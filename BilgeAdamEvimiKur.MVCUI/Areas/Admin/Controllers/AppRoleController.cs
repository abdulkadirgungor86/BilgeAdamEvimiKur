using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.BLL.Managers.Concretes;
using BilgeAdamEvimiKur.DTO.DTOs.AppRoleDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.AppUserDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppRoleVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.AppRoleVMs.PureVMs.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X.PagedList;
using X.PagedList.Extensions;

namespace BilgeAdamEvimiKur.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppRoleController : Controller
    {
        readonly IMapper _mapper;
        readonly IAppRoleManager _userRoleManager;
        readonly IAppUserManager _userManager;
        readonly UserManager<AppUser> _userManagerAppUser;

        public AppRoleController(IMapper mapper, IAppRoleManager appRoleManager, IAppUserManager userManager, UserManager<AppUser> userManagerAppUser)
        {
            _mapper = mapper;
            _userRoleManager = appRoleManager;
            _userManager = userManager;
            _userManagerAppUser = userManagerAppUser;
        }

        public IActionResult GetAppRoles(int? pageNumber)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            List<AppRoleResModel> appRoleResModels = _mapper.Map<List<AppRoleResModel>>(_userRoleManager.GetAll());
            IPagedList<AppRoleResModel> pageListRoles =  appRoleResModels.ToPagedList(pageNumber ?? 1, 6);
            return View(pageListRoles);
        }

        public IActionResult CreateAppRole()
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppRole(CreateAppRoleReqModel model)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            if (!ModelState.IsValid) return View(model);
            TempData["Result"] = await _userRoleManager.AddAsync(_mapper.Map<AppRoleDTO>(model));
            return View(model);
        }

        public async Task<IActionResult> UpdateAppRole(int? id)
        {
            if (id == null) return RedirectToAction("GetAppRoles");
            if (id > 0)
            {
                UpdateAppRoleReqModel uARRM = _mapper.Map<UpdateAppRoleReqModel>(await _userRoleManager.FindAsync(id));
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View(uARRM);

            }
            else return RedirectToAction("GetAppRoles");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAppRole(UpdateAppRoleReqModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View(model);
            }
            try
            {
                AppRoleDTO aDTO = _mapper.Map<AppRoleDTO>(model);
                await _userRoleManager.UpdateAsync(aDTO);
                TempData["Result"] = "Kullanıcı rol güncelleme işlemi başarılı";
            }
            catch
            {
                TempData["Result"] = "Hata : Kullanıcı rol güncelleme işlemi başarısız";
            }
            return RedirectToAction("GetAppRoles");
        }

        public async Task<IActionResult> DeleteAppRole(int? id)
        {
            if (id == null) TempData["Result"] = "Silme işleminde id değeri null olamaz.";
            if (id <= 0) TempData["Result"] = "Silme işleminde id değeri sıfır ve sıfırdan küçük olamaz.";
            else
            {
                try
                {
                    string deletedRoleName = (await _userRoleManager.FirstOrDefaultAsync(x => x.Id == id)).Name;

                    List<AppUserDTO> usersWithDeletedRole = (await _userManager.GetAllUserAsync())
                        .Where(user => user.RoleNames.Contains(deletedRoleName)).ToList();

                    foreach (AppUserDTO userDTO in usersWithDeletedRole)
                    {
                        AppUser user = await _userManagerAppUser.FindByIdAsync(userDTO.Id.ToString());
                        await _userManagerAppUser.RemoveFromRoleAsync(user, deletedRoleName);
                    }

                    _userRoleManager.Delete(await _userRoleManager.FindAsync(id));
                    TempData["Result"] = "Silme işlemi başarılı";
                }
                catch
                {
                    TempData["Result"] = "Hata : Silme işlemi başarısız";
                }
            }
            return RedirectToAction("GetAppRoles");
        }

        public IActionResult DestroyAppRole(int? id)
        {
            if (id == null) TempData["Result"] = "Destroy işleminde id değeri null olamaz.";
            if (id <= 0) TempData["Result"] = "Destroy işleminde id değeri sıfır ve sıfırdan küçük olamaz.";
            else
            {
                try
                {
                    AppRoleDTO aDTO = _userRoleManager.Find(id);
                    TempData["Result"] = _userRoleManager.Destroy(aDTO);
                }
                catch
                {
                    TempData["Result"] = "Hata : Destroy işlemi başarısız";
                }
            }
            return RedirectToAction("GetAppRoles");
        }


    }
}
