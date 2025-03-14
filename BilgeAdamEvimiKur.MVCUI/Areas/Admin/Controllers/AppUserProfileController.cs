using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.ProfileDTOs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppProfileVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppProfileVMs.PureVMs.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;


namespace BilgeAdamEvimiKur.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppUserProfileController : Controller
    {
        readonly IMapper _mapper;
        readonly IProfileManager _profileManager;

        public AppUserProfileController(IMapper mapper, IProfileManager profileManager)
        {
            _mapper = mapper;
            _profileManager = profileManager;
        }

        public async Task<IActionResult> GetUserProfiles(int? pageNumber)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            List<AppUserProfileDTO> profiles = await _profileManager.GetUserProfiles();
            List<GetAppUserProfileResModel> gProfile = _mapper.Map<List<GetAppUserProfileResModel>>(profiles);
            IPagedList<GetAppUserProfileResModel> pageListUserProfile = gProfile.ToPagedList(pageNumber ?? 1, 6);
            return View(pageListUserProfile);
        }

        public async Task<IActionResult> UpdateAppUser(int? id)
        {
            if (id == null) return RedirectToAction("GetUserProfiles");
            if (id > 0)
            {
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                AppUserProfileDTO aUserPDTO = await _profileManager.GetUserProfileAsync(id.Value);
                UpdateAppUserProfileReqModel cAUPRM = _mapper.Map<UpdateAppUserProfileReqModel>(aUserPDTO);
                return View(cAUPRM);
            }
            else return RedirectToAction("GetUserProfiles");
        }



        [HttpPost]
        public async Task<IActionResult> UpdateAppUser(UpdateAppUserProfileReqModel model)
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
                    return RedirectToAction("GetUserProfiles");
                }
                else
                {
                    ViewBag.UserName = User.Identity.Name ?? " Guest ";
                    TempData["Result"] = "Hata : Profile güncelleme işlemi başarısız";
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


        public async Task<IActionResult> DeleteAppUser(int? id)
        {
            if (id == null) TempData["Result"] = "Silme işleminde id değeri null olamaz.";
            if (id <= 0) TempData["Result"] = "Silme işleminde id değeri sıfır ve sıfırdan küçük olamaz.";
            else
            {
                try
                {
                    _profileManager.Delete(await _profileManager.FindAsync(id));
                    TempData["Result"] = "Silme işlemi başarılı";
                }
                catch
                {
                    TempData["Result"] = "Hata : Silme işlemi başarısız";
                }
            }
            return RedirectToAction("GetUserProfiles");
        }

        public IActionResult DestroyAppUser(int? id)
        {
            if (id == null) TempData["Result"] = "Destroy işleminde id değeri null olamaz.";
            if (id <= 0) TempData["Result"] = "Destroy işleminde id değeri sıfır ve sıfırdan küçük olamaz.";
            else
            {
                try
                {
                    ProfileDTO destroyDTO = _profileManager.Find(id);
                    TempData["Result"] = _profileManager.Destroy(destroyDTO);
                }
                catch
                {
                    TempData["Result"] = "Hata : Destroy işlemi başarısız";
                }
            }
            return RedirectToAction("GetUserProfiles");
        }
    }
}
