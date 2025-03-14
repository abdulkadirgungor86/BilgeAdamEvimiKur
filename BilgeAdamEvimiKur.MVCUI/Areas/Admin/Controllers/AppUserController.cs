using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.ENTITIES.Enums;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppRoleVMs.PureVMs.ResponseModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PageVMs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppUserVMs.PureVMs.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

namespace BilgeAdamEvimiKur.MVCUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppUserController : Controller
    {
        readonly IMapper _mapper;
        readonly IAppUserManager _userManager;
        readonly UserManager<AppUser> _userManagerAppUser;
        readonly IAppRoleManager _appRoleManager;

        public AppUserController(IMapper mapper, IAppUserManager appUserManager, IAppRoleManager appRoleManager, UserManager<AppUser> userManagerAppUser)
        {
            _mapper = mapper;
            _userManager = appUserManager;
            _appRoleManager = appRoleManager;
            _userManagerAppUser = userManagerAppUser;
        }

        public async Task<IActionResult> GetUsers(int? pageNumber)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            List<GetUserResModel> lGetURM = _mapper.Map<List<GetUserResModel>>( (await _userManager.GetAllUserAsync()) );
            IPagedList<GetUserResModel> pageListUsers = lGetURM.ToPagedList(pageNumber ?? 1, 6);
            return View(pageListUsers);
        }

        public async Task<IActionResult> UpdateUser(int? id)
        {
            if (id == null) return RedirectToAction("GetUsers");
            if (id > 0)
            {
                UpdateUserPageModel updateUserPageModel = new UpdateUserPageModel();
                updateUserPageModel.UpdateUserModel = _mapper.Map<UpdateUserModel>(await _userManager.FindAsync(id));
                updateUserPageModel.UpdateUserModel.RoleNames = (await _userManagerAppUser.GetRolesAsync(await _userManagerAppUser.FindByIdAsync(id.ToString()))).ToList();
                updateUserPageModel.AppRoleResModels = _mapper.Map<List<AppRoleResModel>>(_appRoleManager.GetActives());

                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View(updateUserPageModel);
            }
            else return RedirectToAction("GetUsers");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserPageModel? model, string? action)
        {
            if ((model != null) & (action != null))
            {
                if (action == "updateUser")
                {
                    bool isResult= false;
                    bool isEmailText=false;
                    string result = "";
                    AppUser user = await _userManagerAppUser.FindByIdAsync(model.UpdateUserModel.Id.ToString());
                    user.SecurityStamp = Guid.NewGuid().ToString();
                   
                    if (!string.IsNullOrEmpty( model.UpdateUserModel.UserName) && (user.UserName != model.UpdateUserModel.UserName) )
                    {
                        model.UpdateUserModel.UserName = model.UpdateUserModel.UserName.Trim();
                        if (!string.IsNullOrEmpty(model.UpdateUserModel.UserName)) user.UserName = model.UpdateUserModel.UserName;

                        result += "UserName ";
                        isResult = true;
                    }

                    if  (!(string.IsNullOrEmpty(model.UpdateUserModel.Email))&&(user.Email != model.UpdateUserModel.Email))
                    {
                        user.Email = model.UpdateUserModel.Email;
                        user.EmailConfirmed = false;
                        await _userManager.checkMailActiveAsync(user.Id.ToString());
                        isEmailText = true;
                        result += "Email ";
                        isResult = true;
                    }

                    if (!string.IsNullOrEmpty(model.UpdateUserModel.NewPassword))
                    {
                        PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
                        user.PasswordHash = passwordHasher.HashPassword(user, model.UpdateUserModel.NewPassword);
                        user.ModifiedDate = DateTime.Now;
                        user.Status = DataStatus.Updated;
                        await _userManagerAppUser.UpdateAsync(user);
                        result += "Password ";
                        isResult = true;
                    }

                    if (await _userManagerAppUser.IsLockedOutAsync(user))
                    {
                        await _userManagerAppUser.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                        result += "Locked ";
                        isResult = true;
                    }

                    if(isResult)
                    {
                        user.ModifiedDate = DateTime.Now;
                        user.Status = DataStatus.Updated;
                        IdentityResult updateResult = await _userManagerAppUser.UpdateAsync(user);
                        if (!updateResult.Succeeded)
                        {
                            isEmailText = false;
                            isResult = false;
                            result = "Güncelleme başarısız oldu.";
                        } else
                        {
                            result += "alan(lar)ı güncellendi.";
                        }
                    }
                    else result = "Herhangi bir güncelleme olmadı.";

                    if (isEmailText) result += " Email adresiniz değiştiği için e-posta adresinize doğrulama linki gönderilmiştir.";
                    TempData["Result"] = result;

                    return RedirectToAction("GetUsers");
                }
                else if (action == "addRole")
                {
                    try
                    {
                        if (!(string.IsNullOrEmpty(model.UpdateUserModel.NewRoleName)))
                        {
                            AppUser user = await _userManagerAppUser.FindByIdAsync(model.UpdateUserModel.Id.ToString());
                            IdentityResult res = await _userManagerAppUser.AddToRoleAsync(user, model.UpdateUserModel.NewRoleName);

                            if(res.Succeeded)
                            {
                                user.ModifiedDate = DateTime.Now;
                                user.Status = DataStatus.Updated;
                                IdentityResult updateResult = await _userManagerAppUser.UpdateAsync(user);
                            }
                            TempData["Result"] = $"Update : \"{user.UserName}\" adlı kullanıcıya \"{model.UpdateUserModel.NewRoleName}\" rolü eklendi.";
                        }
                    } catch (Exception ex)
                    {
                        TempData["Result"] = $"Hata : {ex.Message}";
                    }
                    return RedirectToAction("UpdateUser", new { id = model.UpdateUserModel.Id });
                }
                else if (action == "removeRole")
                {
                    try
                    {
                        if (!(string.IsNullOrEmpty(model.UpdateUserModel.NewRoleName)))
                        {
                            AppUser user = await _userManagerAppUser.FindByIdAsync(model.UpdateUserModel.Id.ToString());
                             IdentityResult res =  await _userManagerAppUser.RemoveFromRoleAsync(user, model.UpdateUserModel.NewRoleName);

                            if(res.Succeeded)
                            {
                                user.ModifiedDate = DateTime.Now;
                                user.Status = DataStatus.Updated;
                                IdentityResult updateResult = await _userManagerAppUser.UpdateAsync(user);
                            }
                            TempData["Result"] = $"Update : \"{user.UserName}\" adlı kullanıcının \"{model.UpdateUserModel.NewRoleName}\" rolu çıkarıldı.";
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["Result"] = $"Hata : {ex.Message}";
                    }
                    return RedirectToAction("UpdateUser", new { id = model.UpdateUserModel.Id });
                }
            }
            return RedirectToAction("GetUsers");
        }

        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null) TempData["Result"] = "Silme işleminde id değeri null olamaz.";
            if (id <= 0) TempData["Result"] = "Silme işleminde id değeri sıfır ve sıfırdan küçük olamaz.";
            else
            {
                try
                {
                    await _userManager.DeleteAsync(id.Value);
                    TempData["Result"] = "Silme işlemi başarılı";
                }
                catch
                {
                    TempData["Result"] = "Hata : Silme işlemi başarısız";
                }
            }
            return RedirectToAction("GetUsers");
        }

        public IActionResult DestroyUser(int? id)
        {
            if (id == null) TempData["Result"] = "Destroy işleminde id değeri null olamaz.";
            if (id <= 0) TempData["Result"] = "Destroy işleminde id değeri sıfır ve sıfırdan küçük olamaz.";
            else
            {
                try
                {
                    TempData["Result"] =  _userManager.Destroy(_userManager.Find(id));
                }
                catch (Exception ex)
                {
                    TempData["Result"] = $"Hata : {ex.Message}";
                }
            }
            return RedirectToAction("GetUsers");
        }

    }
}
