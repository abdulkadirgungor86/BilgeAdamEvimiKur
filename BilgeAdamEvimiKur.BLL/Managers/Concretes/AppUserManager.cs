using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.COMMON.Tools.Services;
using BilgeAdamEvimiKur.DAL.Repositories.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.AppUserDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.OrderDTOs;
using BilgeAdamEvimiKur.ENTITIES.Enums;
using BilgeAdamEvimiKur.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace BilgeAdamEvimiKur.BLL.Managers.Concretes
{
    public class AppUserManager : BaseManager<AppUser, AppUserDTO>, IAppUserManager
    {
        readonly IMapper _mapper;
        readonly IAppUserRepository _userRep;
        readonly IOrderManager _orderManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;

        public AppUserManager(IAppUserRepository userRep, IMapper mapper, IOrderManager orderManager ,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base( mapper , userRep)
        {
            _userRep = userRep;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _orderManager = orderManager;
    
        }

        public override string Destroy(AppUserDTO item)
        {
            AppUser entity = _mapper.Map<AppUser>(item);
            if (entity.Status == DataStatus.Deleted)
            {
                try
                {
                    AppUser existingItem = _userRep.FirstOrDefault(e=>e.Id == entity.Id);
                    if (existingItem != null)
                    {
                        DestroyDependentOrders(existingItem);
                        _userRep.Destroy(existingItem);
                    }
                    else throw new ArgumentException("Hata : existingItem nesnesi değeri null. Veritabanında bulunamadı. AppUserManager/Destroy/existingItem");
                    return "Destroy işlemi başarılı";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "Veriyi yoketmek için önce silmeniz lazım";
        }

        void DestroyDependentOrders(AppUser existingItem)
        {
            List<OrderDTO> orderDTOs = _orderManager.Where(o => o.AppUserID == existingItem.Id).ToList();

            foreach (OrderDTO order in orderDTOs)
            {
                OrderDTO deleteOrderDTO = _orderManager.Find(order.ID);
                _orderManager.Delete(deleteOrderDTO);
                OrderDTO destroyOrderDTO = _orderManager.Find(order.ID);
                _orderManager.Destroy(destroyOrderDTO);
            }
        }

        public override void Delete(AppUserDTO item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item), "Öğe boş olamaz.");
            AppUser entity = _mapper.Map<AppUser>(item);
            entity.SecurityStamp = Guid.NewGuid().ToString();
            entity.Status = DataStatus.Deleted;
            entity.DeletedDate = DateTime.Now;
            AppUser originalEntity = _iRep.Find(entity.Id);
            _iRep.Entry(originalEntity, entity);
        }

        public async Task DeleteAsync(int id)
        {
            AppUser? user = await _iRep.FindAsync(id);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.Status = DataStatus.Deleted;
            user.DeletedDate = DateTime.Now;
            AppUser originalUser = await _iRep.FindAsync(user.Id);
            _iRep.Entry(originalUser, user);
        }


        public override async Task UpdateAsync(AppUserDTO item)
        {
            AppUser entity = _mapper.Map<AppUser>(item);
            entity.ModifiedDate = DateTime.Now;
            entity.Status = DataStatus.Updated;
            AppUser originalEntity = await _iRep.FindAsync(entity.Id);
            _iRep.Entry(originalEntity, entity);
        }

        public async Task<IdentityResult> UserRegisterAsync(UserRegisterRequestModelDTO userRegisterDTO)
        {
            List<string> keys = new List<string> { "confirmEmailUrl" };
            Dictionary<string, string> configSettings = await JsonService.ReadFromFileAsync(keys);
            string link = configSettings["confirmEmailUrl"];

            AppUser appUser = _mapper.Map<AppUser>(userRegisterDTO);
            appUser.ActivationCode = Guid.NewGuid();
            Microsoft.AspNetCore.Identity.IdentityResult result = await _userManager.CreateAsync(appUser, userRegisterDTO.Password);
            if (result.Succeeded)
            {
                string message = $"\"{appUser.UserName}\" adlı kullanıcı hesabınız oluşturuldu. Lütfen linki tıklayınız:  {link}{appUser.ActivationCode}&id={appUser.Id}";
                await MailService.SendAsync(receiver: userRegisterDTO.Email, body: message);
            }
            return result;
        }

        public async Task<bool> checkMailActiveAsync(string Id)
        {
            try
            {
                List<string> keys = new List<string> { "confirmEmailUrl" };
                Dictionary<string, string> configSettings = await JsonService.ReadFromFileAsync(keys);
                string link = configSettings["confirmEmailUrl"];

                AppUser appUser = await _userManager.FindByIdAsync(Id);
                
                string email = appUser.Email;
                Guid emailChangeGuid = Guid.NewGuid();
                appUser.ActivationCode = emailChangeGuid;
                appUser.SecurityStamp = Guid.NewGuid().ToString();
                AppUser originalEntity = _iRep.Find(Convert.ToInt32(Id));
                _iRep.Entry(originalEntity, appUser);

                string message = $"\"{appUser.UserName}\" adlı kullanıcı emailinizi doğrulamanız gerekmektedir. Lütfen linki tıklayınız:  {link}{emailChangeGuid}&id={Id}";
                await MailService.SendAsync(receiver: email, body: message);
      
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


            public async Task<(bool, string)> ConfirmEmailAsync(ConfirmEmailDTO confirmEmailDTO)
        {
            AppUser? user = await _userManager.FindByIdAsync(confirmEmailDTO.UserId.ToString());
            if (user == null) return (false, "Hata : Kullanici bulunamadi.");
            if (user.ActivationCode != confirmEmailDTO.SpecId) return (false, "Hata : Gecersiz onay kodu");
            if (user.EmailConfirmed) return (false, "Hata : Hesabınız daha önce onaylanmıştır.");
            if (user.Status == DataStatus.Deleted) return (false, "Hata : Hesabınız silinmiş olduğundan emailiniz onaylanmamıştır. Lütfen adminle iletişime geçiniz.");
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            return (true, "Hesabınız onaylanmistir");
        }

        public async Task<(bool,SignInResult, IList<string?>)> SignInAsync(UserSignInRequestModelDTO userSignInDTO)
        {
            AppUser? user = await _userManager.FindByNameAsync(userSignInDTO.UserName);
            bool passwordConfirmed = await _userManager.CheckPasswordAsync(user, userSignInDTO.Password);

            if (user == null) return (passwordConfirmed, SignInResult.Failed, null);

            SignInResult result = await _signInManager.PasswordSignInAsync(user, userSignInDTO.Password, true, true);
            if (result.Succeeded)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                return (passwordConfirmed, result, roles);
            }
            return (passwordConfirmed, result, null);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<List<AppUserDTO>>  GetAllUserAsync()
        {
            List<AppUserDTO> result = new List<AppUserDTO>();
            foreach (AppUserDTO user in (base.GetAll()))
            {
                user.RoleNames = (await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id.ToString()))).ToList();
                result.Add(user);
            }
            return result;
        }


        public async Task<string> SendPassToEmailAsync(SendPassToEmailDTO sendPassToEmailDTO)
        {
            try
            {
                AppUser? user = await _userManager.FindByNameAsync(sendPassToEmailDTO.UserName);
                if (user != null)
                {
                    if (user.Email == sendPassToEmailDTO.Email)
                    {
                        if (user.EmailConfirmed)
                        {
                            if (user.Status == DataStatus.Deleted) return "Hesabınız silinmiştir. Lütfen adminle iletişime geçiniz.";

                            string password = PasswordService.GenerateRandomString(12);
                            user.SecurityStamp = Guid.NewGuid().ToString();
                            await _userManager.UpdateAsync(user);
                            PasswordHasher<AppUser> _passwordHasher = new PasswordHasher<AppUser>();
                            user.PasswordHash = _passwordHasher.HashPassword(user, password);
                            await _userManager.UpdateAsync(user);
                            string message = $"Şifre sıfırlama talabiniz nedeniyle şifreniz değiştirilip emalinize gönderilmiştir. Giriş bilgileriniz aşağıdadır. \r\n UserName : {user.UserName} \r\n Password : {password} ";
                            await MailService.SendAsync(receiver: user.Email, body: message);
                            return "Email adresinize şifre sıfırlama bilgileri gönderilmiştir.";

                        }
                        else return "Email adresiniz onaylanmadığı için şifre gönderilememektedir.";
                    }
                    else return "Kullanıcı ile email bilgileri uyuşmamaktadır.";
                }
                else return "Kullanıcı bulunamadı.";
            } catch (Exception ex)
            {
                return $"Hata : {ex.Message}";
            }
        }

        public async Task<string> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                AppUser? user = await _userManager.FindByIdAsync(changePasswordDTO.Id);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, changePasswordDTO.Password))
                    {
                        if (user.Status == DataStatus.Deleted) return "Hesabınız silinmiştir. Lütfen adminle iletişime geçiniz.";

                        user.SecurityStamp = Guid.NewGuid().ToString();
                        await _userManager.UpdateAsync(user);
                        PasswordHasher<AppUser> _passwordHasher = new PasswordHasher<AppUser>();
                        user.PasswordHash = _passwordHasher.HashPassword(user, changePasswordDTO.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return "Şifre değiştirme işlemi başarılı";
                    }
                    else return "Mevcut şifre yanlış";
                }
                else return "Hata : Kullanıcı bulunamadı";
            } catch (Exception ex)
            {
                return $"Hata : {ex.Message}";
            }
        }
    }
}
