using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.ResponseModels;
using BilgeAdamEvimiKur.DTO.DTOs.CategoryDTOs;
using X.PagedList.Extensions;
using X.PagedList;

namespace BilgeAdamEvimiKur.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        readonly IMapper _mapper;
        readonly ICategoryManager _categoryManager;

        public CategoryController(IMapper mapper, ICategoryManager catManager)
        {
            _mapper = mapper;
            _categoryManager = catManager;
        }

        public IActionResult GetCategories(int? pageNumber)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            List<CategoryResModel> catResModels = _mapper.Map<List<CategoryResModel>>(_categoryManager.GetAll());
            IPagedList<CategoryResModel> pageListCategories=catResModels.ToPagedList(pageNumber ?? 1, 6);
            return View(pageListCategories);
        }

        public IActionResult CreateCategory()
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryReqModel model)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            if (!ModelState.IsValid) return View(model);
            TempData["Result"] = await _categoryManager.AddAsync(_mapper.Map<CategoryDTO>(model));
            return View(model);
        }

        public async Task<IActionResult> UpdateCategory(int? id)
        {
            if (id == null) return RedirectToAction("GetCategories");
            if (id > 0)
            {
                UpdateCategoryReqModel uCRM = _mapper.Map<UpdateCategoryReqModel>(await _categoryManager.FindAsync(id));
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View(uCRM);
            }
            else return RedirectToAction("GetCategories");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryReqModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View(model);
            }
            try
            {
                CategoryDTO cDTO = _mapper.Map<CategoryDTO>(model);
                await _categoryManager.UpdateAsync(cDTO);
                TempData["Result"] = "Kategori güncelleme işlemi başarılı";
            }
            catch
            {
                TempData["Result"] = "Hata : Kategori güncelleme işlemi başarısız";
            }
            return RedirectToAction("GetCategories");
        }

        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null) TempData["Result"] = "Silme işleminde id değeri null olamaz.";
            if (id <= 0) TempData["Result"] = "Silme işleminde id değeri sıfır ve sıfırdan küçük olamaz.";
            else
            {
                try
                {
                    _categoryManager.Delete(await _categoryManager.FindAsync(id));
                    TempData["Result"] = "Silme işlemi başarılı";
                }
                catch
                {
                    TempData["Result"] = "Hata : Silme işlemi başarısız";
                }
            }
            return RedirectToAction("GetCategories");
        }

        public IActionResult DestroyCategory(int? id)
        {
            if (id == null) TempData["Result"] = "Destroy işleminde id değeri null olamaz.";
            if (id <= 0) TempData["Result"] = "Destroy işleminde id değeri sıfır ve sıfırdan küçük olamaz.";
            else
            {
                try
                {
                    CategoryDTO categoryDTO = _categoryManager.Find(id);
                    TempData["Result"] = _categoryManager.Destroy(categoryDTO);
                }
                catch
                {
                    TempData["Result"] = "Hata : Destroy işlemi başarısız";
                }
            }
            return RedirectToAction("GetCategories");
        }
    }
 }

