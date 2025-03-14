using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.BLL.Managers.Concretes;
using BilgeAdamEvimiKur.DTO.DTOs.CategoryDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.SupplierDTOs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.ResponseModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.Supplier.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.Supplier.PureVMs.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

namespace BilgeAdamEvimiKur.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SupplierController : Controller
    {
        readonly IMapper _mapper;
        readonly ISupplierManager _supplierManager;

        public SupplierController(IMapper mapper, ISupplierManager supplierManager)
        {
            _mapper = mapper;
            _supplierManager = supplierManager;
        }

        public IActionResult GetSuppliers(int? pageNumber)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            List<SupplierResModel> supResModels = _mapper.Map<List<SupplierResModel>>(_supplierManager.GetAll());
            IPagedList<SupplierResModel> pageListSuppliers = supResModels.ToPagedList(pageNumber ?? 1, 6);
            return View(pageListSuppliers);
        }

        public IActionResult CreateSupplier()
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateSupplier(CreateSupplierReqModel model)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            if (!ModelState.IsValid) return View(model);
            TempData["Result"] = await _supplierManager.AddAsync(_mapper.Map<SupplierDTO>(model));
            return RedirectToAction("GetSuppliers");
        }

        public async Task<IActionResult> UpdateSupplier(int? id)
        {
            if (id == null) return RedirectToAction("GetSuppliers");
            if (id > 0)
            {
                UpdateSupplierReqModel uSRM = _mapper.Map<UpdateSupplierReqModel>(await _supplierManager.FindAsync(id));
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View(uSRM);
            }
            else return RedirectToAction("GetSuppliers");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateSupplier(UpdateSupplierReqModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View(model);
            }
            try
            {
                SupplierDTO cDTO = _mapper.Map<SupplierDTO>(model);
                await _supplierManager.UpdateAsync(cDTO);
                TempData["Result"] = "Tedarikçi güncelleme işlemi başarılı";
            }
            catch
            {
                TempData["Result"] = "Hata : Tedarikçi güncelleme işlemi başarısız";
            }
            return RedirectToAction("GetSuppliers");
        }

        public async Task<IActionResult> DeleteSupplier(int? id)
        {
            if (id == null) TempData["Result"] = "Silme işleminde id değeri null olamaz.";
            if (id <= 0) TempData["Result"] = "Silme işleminde id değeri sıfır ve sıfırdan küçük olamaz.";
            else
            {
                try
                {
                    _supplierManager.Delete(await _supplierManager.FindAsync(id));
                    TempData["Result"] = "Silme işlemi başarılı";
                }
                catch
                {
                    TempData["Result"] = "Hata : Silme işlemi başarısız";
                }
            }
            return RedirectToAction("GetSuppliers");
        }

        public IActionResult DestroySupplier(int? id)
        {
            if (id == null) TempData["Result"] = "Destroy işleminde id değeri null olamaz.";
            if (id <= 0) TempData["Result"] = "Destroy işleminde id değeri sıfır ve sıfırdan küçük olamaz.";
            else
            {
                try
                {
                    SupplierDTO supplierDTO = _supplierManager.Find(id);
                    TempData["Result"] = _supplierManager.Destroy(supplierDTO);
                }
                catch
                {
                    TempData["Result"] = "Hata : Destroy işlemi başarısız";
                }
            }
            return RedirectToAction("GetSuppliers");
        }

    }
}