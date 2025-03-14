using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.AppUserDTOs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.RequestModels;
using Microsoft.AspNetCore.Mvc;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.VerifyMailVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.ENTITIES.Enums;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using System.Security.Claims;

namespace BilgeAdamEvimiKur.MVCUI.Controllers
{
    public class UserController : Controller
    {

        readonly IAppUserManager _userManager;
        readonly IMapper _mapper;

        public UserController(IMapper mapper, IAppUserManager appUserManager)
        {
            _mapper = mapper;
            _userManager = appUserManager;
        }

        public IActionResult Register()
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterReqModel model)
        {
            if (!string.IsNullOrEmpty(model.UserName)) model.UserName = model.UserName.Trim();

            if (!ModelState.IsValid)
            {
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View(model);
            }
            UserRegisterRequestModelDTO userDTO = _mapper.Map<UserRegisterRequestModelDTO>(model);
            if ((await _userManager.UserRegisterAsync(userDTO)).Succeeded)
            {
                TempData["Result"] = "Lütfen emailinize gelen linki tıklayarak hesabınızı onaylayınız.";
                return RedirectToAction("RedirectPanel", "Home");
            }
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            TempData["Result"] = "Hata : Hesabınız oluşturulamadı. Kullanıcı adını değiştirip tekrar deneyiniz.";
            return View();
        }


        public IActionResult SignIn(string returnUrl)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            if (returnUrl == null || returnUrl == string.Empty) return View();
            TempData["Result"] = $"'{returnUrl}' sayfasına yetkisiz erişmeye çalıştınız. Lütfen giriş yapınız.";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInReqModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View(model);
            }
            UserSignInRequestModelDTO signInDTO = _mapper.Map<UserSignInRequestModelDTO>(model);

            AppUserDTO aUserDTO = await _userManager.FirstOrDefaultAsync(x => x.UserName == signInDTO.UserName);
            if ((aUserDTO != null) && (aUserDTO.Status == "Deleted"))
            {
                TempData["Result"] = $"\"{aUserDTO.UserName}\" adlı hesabınız silinmiştir. Lütfen adminle iletişime geçiniz.";
                return RedirectToAction("SignIn");
            }

            (bool passwordConfirmed, SignInResult result, IList<string>? roles) = await _userManager.SignInAsync(signInDTO);

            if (result.Succeeded)
            {
                if (roles.Contains("Admin")) return RedirectToAction("Index", "Home", new { Area = "Admin" });
                else if (roles.Contains("Member")) return RedirectToAction("Privacy","Home");
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsNotAllowed)
            {
                if (passwordConfirmed)
                {
                    TempData["Result"] = "Lütfen emailinize gelen linki tıklayarak hesabınızı onaylayınız.";
                    return RedirectToAction("RedirectPanel", "Home");
                }
            }
            else if (result.IsLockedOut)
            {
                TempData["Result"] = $"\"{aUserDTO.UserName}\" adlı hesabınız kilitlidir. Lütfen adminle iletişime geçiniz.";
                return RedirectToAction("SignIn");
            }

            TempData["Result"] = "Kullanıcı adınız veya şifreniz yanlış";
            return RedirectToAction("SignIn");
        }

        public async Task<IActionResult> LogOut()
        {
            await _userManager.SignOutAsync();
            if (User.Identity.Name == null)
            {
                TempData["Result"] = "Giriş yapmadan çıkış yapamazsınız.";
                return RedirectToAction("RedirectPanel", "Home");
            }
            TempData["Result"] = "Hesabınızdan başarıyla çıkış yaptınız.";
            return RedirectToAction("RedirectPanel", "Home");
        }

        public async Task<IActionResult> ConfirmEmail(Guid? specId, int? id)
        {
            if (specId == null)
            {
                TempData["Result"] = "Guid değeri boş geçilemez.";
                return RedirectToAction("RedirectPanel", "Home");
            }
            if ((id == null)&&(id>0))
            {
                TempData["Result"] = "id değeri boş yada geçersiz.";
                return RedirectToAction("RedirectPanel", "Home");
            }
            ConfirmEmailDTO cEmailDTO = new ConfirmEmailDTO() { SpecId = specId.Value, UserId = id.Value };
            (bool isSucces, string message) = await _userManager.ConfirmEmailAsync(cEmailDTO);
            if (isSucces)
            {
                TempData["Result"] = "Hesabınız onaylanmıştır. Giriş yapabilirsiniz.";
                return RedirectToAction("SignIn");
            }
            TempData["Result"] = message;
            return RedirectToAction("RedirectPanel", "Home");
        }

        public IActionResult VerifyMail()
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyMail(VerifyMailReqModel model)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            if (!ModelState.IsValid) return View(model);
            int userSum = 0;
            int deleteUserSum = 0;
            List<AppUserDTO> listAUDTO = _userManager.Where(x => x.Email == model.Email && x.EmailConfirmed == false);
            if (listAUDTO.Count == 0) TempData["Result"] = "Kullanıcı bulunamadı ya da email hesabınız onaylıdır.";
            else
            {
                foreach (AppUserDTO userDTO in listAUDTO)
                {
                    if(userDTO.Status != (DataStatus.Deleted).ToString())
                    {
                        userSum += 1;
                        await _userManager.checkMailActiveAsync(userDTO.Id.ToString());
                    }
                    else deleteUserSum += 1;
                }
                if (userSum > 0) TempData["Result"] = $"\"{model.Email}\" email adresine sahip {userSum} sayıda onaylanmamış hesap vardır. Bu hesap(lar) için doğrulama linki gönderilmiştir. <br />";
                if (deleteUserSum > 0) TempData["Result"] += $"\"{model.Email}\" email adresine sahip {deleteUserSum} sayıda silinmiş hesap vardır. Adminle iletişime geçiniz.";
            }
            return View();
        }

        public IActionResult SendPassToEmail()
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendPassToEmail(SendPassToEmailReqModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View(model);
            }

            SendPassToEmailDTO sendPassToEmailDTO = _mapper.Map<SendPassToEmailDTO>(model);
            TempData["Result"] = await _userManager.SendPassToEmailAsync(sendPassToEmailDTO);
            return RedirectToAction("SendPassToEmail");
              
        }

        public IActionResult ChangePassword()
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";

            if (User.Identity.Name == null)
            {
                TempData["Result"] = "Giriş yapmadan bu sayfaya ulaşamazsınız.";
                return RedirectToAction("RedirectPanel", "Home");
            }

            ChangePasswordReqModel model = new();
            model.Id = (User.FindFirst(ClaimTypes.NameIdentifier)).Value;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordReqModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View(model);
            }

            ChangePasswordDTO changePasswordDTO = _mapper.Map<ChangePasswordDTO>(model);
            TempData["Result"] = await _userManager.ChangePasswordAsync(changePasswordDTO);

            return RedirectToAction("ChangePassword");
        }


    }
}
