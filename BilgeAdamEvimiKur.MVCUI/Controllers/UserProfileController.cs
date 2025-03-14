using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.AppUserDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.ProfileDTOs;
using BilgeAdamEvimiKur.ENTITIES.Enums;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppProfileVMs.PureVMs.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace BilgeAdamEvimiKur.MVCUI.Controllers
{
    public class UserProfileController : Controller
    {
        readonly IAppUserManager _userManager;
        readonly IMapper _mapper;
        readonly IProfileManager _profileManager;

        public UserProfileController(IAppUserManager userManager, IMapper mapper, IProfileManager profileManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _profileManager = profileManager;
        }

        public async Task<IActionResult> UserProfile()
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            AppUserDTO aUserDTO = await _userManager.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            if (aUserDTO != null)
            {

                ProfileDTO profileDTO = await _profileManager.FirstOrDefaultAsync(x => x.ID == aUserDTO.Id);

                if (profileDTO != null)
                {
                    if (profileDTO.Status == ((DataStatus.Deleted).ToString()))
                    {
                        TempData["Result"] = "Profiliniz silinmiştir! Yeniden profilinizi oluşturabilirsiniz.";
                        AppUserProfileReqModel aUPRM = new();
                        aUPRM.Id = aUserDTO.Id;
                        aUPRM.UserName = aUserDTO.UserName;
                        return View(aUPRM);
                    
                    } else
                    {
                        AppUserProfileReqModel aUPRM = _mapper.Map<AppUserProfileReqModel>(profileDTO);
                        aUPRM.UserName = aUserDTO.UserName;
                        return View(aUPRM);
                    }
                }
                else
                {
                    AppUserProfileReqModel aUPRM = new();
                    aUPRM.Id = aUserDTO.Id;
                    aUPRM.UserName = aUserDTO.UserName;
                    return View(aUPRM);
                }
            }
            TempData["Result"] = "Giriş yapmadan bu sayfaya ulaşamazsınız.";
            return RedirectToAction("RedirectPanel", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(AppUserProfileReqModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.UserName = User.Identity.Name ?? " Guest ";
                    return View(model);
                }

                AppUserProfileDTO aUserProfileDTO = _mapper.Map<AppUserProfileDTO>(model);

                if (_profileManager.UpdateUserProfile(aUserProfileDTO))
                {
                    ViewBag.UserName = User.Identity.Name ?? " Guest ";
                    TempData["Result"] = "Profil güncelleme işlemi başarılı";
                    return View(model);
                }
                else
                {
                    ViewBag.UserName = User.Identity.Name ?? " Guest ";
                    TempData["Result"] = "Hata : Profil güncelleme işlemi başarısız";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                TempData["Result"] = $"Hata : {ex}";
                return View(model);
            }
        }
    }
}
