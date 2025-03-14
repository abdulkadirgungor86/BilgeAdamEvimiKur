using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.ProductDTOs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.ResponseModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PageVMs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PureVMs.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PureVMs.RequestModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.Supplier.PureVMs.ResponseModels;
using BilgeAdamEvimiKur.ENTITIES.Models;

namespace BilgeAdamEvimiKur.MVCUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        readonly IMapper _mapper;
        readonly IProductManager _productManager;
        readonly ICategoryManager _catManager;
        readonly ISupplierManager _supplierManager;


        public ProductController(IMapper mapper, IProductManager productManager, ICategoryManager catManager, ISupplierManager supplierManager)
        {
            _mapper = mapper;
            _catManager = catManager;
            _productManager = productManager;
            _supplierManager = supplierManager;
        }

        public IActionResult GetProducts(int? pageNumber)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            List<ProductResModel> productResModel = _mapper.Map<List<ProductResModel>>(_productManager.GetAll());
            GetProductPageVM getProductPageVM = new GetProductPageVM()
            {
                Products = productResModel.ToPagedList(pageNumber ?? 1, 4)
            };
            return View(getProductPageVM);
        }

        public IActionResult CreateProduct()
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";

            CreateProductPageVM createProductPageVM = new()
            {
                Categories = _mapper.Map<List<CategoryResModel>>(_catManager.GetAll()),
                Suppliers = _mapper.Map<List<SupplierResModel>>(_supplierManager.GetAll())
            };
            return View(createProductPageVM);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductPageVM model, IFormFile? formFile)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";

            ModelState.Remove("Categories");
            ModelState.Remove("Suppliers");
            if (!ModelState.IsValid) // Validation kontrol
            {
                model.Categories = _mapper.Map<List<CategoryResModel>>(_catManager.GetAll());
                model.Suppliers = _mapper.Map<List<SupplierResModel>>(_supplierManager.GetAll());
                return View(model);
            }

            if ((model.Product.CategoryID == null) || (model.Product.CategoryID <= 0)) // CategoryId kontrol
            {
                model.Categories = _mapper.Map<List<CategoryResModel>>(_catManager.GetAll());
                model.Suppliers = _mapper.Map<List<SupplierResModel>>(_supplierManager.GetAll());
                TempData["Result"] = "Lütfen ürün kategorisini seçiniz!";
                return View(model);
            }
            if ((model.Product.SupplierID == null) || (model.Product.SupplierID <= 0)) //Supplier kontrol
            {
                model.Categories = _mapper.Map<List<CategoryResModel>>(_catManager.GetAll());
                model.Suppliers = _mapper.Map<List<SupplierResModel>>(_supplierManager.GetAll());
                TempData["Result"] = "Lütfen tedarikçi seçiniz!";
                return View(model);
            }
            if (formFile == null) // resim kontrol
            {
                model.Categories = _mapper.Map<List<CategoryResModel>>(_catManager.GetAll());
                model.Suppliers = _mapper.Map<List<SupplierResModel>>(_supplierManager.GetAll());
                TempData["Result"] = "Lütfen ürün resmi seçiniz!";
                return View(model);
            }

            TempData["Result"] = await _productManager.CreateProductAsync(formFile, _mapper.Map<ProductDTO>(model.Product));
            return RedirectToAction("CreateProduct");
        }

        public async Task<IActionResult> UpdateProduct(int? id)
        {
            if ((id == null) || (id <= 0))
            {
                TempData["Result"] = "Güncellenecek değer bulunamadı.";
                return RedirectToAction("GetCategories");
            }
            UpdateProductPageVM updateProductPageVM = new()
            {
                Product = _mapper.Map<UpdateProductReqModel>(await _productManager.FindAsync(id)),
                Categories = _mapper.Map<List<CategoryResModel>>(_catManager.GetAll()),
                Suppliers = _mapper.Map<List<SupplierResModel>>(_supplierManager.GetAll())
            };
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            return View(updateProductPageVM);

        }


        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductPageVM model, IFormFile? formFile)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";

            ModelState.Remove("Categories");
            ModelState.Remove("Suppliers");
            if (!ModelState.IsValid) // Validation kontrol
            {
                model.Categories = _mapper.Map<List<CategoryResModel>>(_catManager.GetAll());
                model.Suppliers = _mapper.Map<List<SupplierResModel>>(_supplierManager.GetAll());
                return View(model);
            }

            if ((model.Product.CategoryID == null) || (model.Product.CategoryID <= 0)) // CategoryId kontrol
            {
                model.Categories = _mapper.Map<List<CategoryResModel>>(_catManager.GetAll());
                model.Suppliers = _mapper.Map<List<SupplierResModel>>(_supplierManager.GetAll());
                TempData["Result"] = "Lütfen ürün kategorisini seçiniz!";
                return View(model);
            }
            if ((model.Product.SupplierID == null) || (model.Product.SupplierID <= 0)) //Supplier kontrol
            {
                model.Categories = _mapper.Map<List<CategoryResModel>>(_catManager.GetAll());
                model.Suppliers = _mapper.Map<List<SupplierResModel>>(_supplierManager.GetAll());
                TempData["Result"] = "Lütfen ürün kategorisini seçiniz!";
                return View(model);
            }
            if (formFile == null) // resim kontrol
            {
                try
                {
                    await _productManager.UpdateAsync(_mapper.Map<ProductDTO>(model.Product));
                    TempData["Result"] = "Güncelleme işlemi başarılı";
                    return RedirectToAction("GetProducts");
                } catch (Exception ex)
                {
                    TempData["Result"] = $"Hata meydana geldi. {ex.Message}";
                    model.Categories = _mapper.Map<List<CategoryResModel>>(_catManager.GetAll());
                    model.Suppliers = _mapper.Map<List<SupplierResModel>>(_supplierManager.GetAll());
                    return View(model);
                }
            }
            try
            {
                await _productManager.UpdateProductAsync(formFile, _mapper.Map<ProductDTO>(model.Product));
                TempData["Result"] = "Güncelleme işlemi başarılı";
                return RedirectToAction("GetProducts");
            } catch (Exception ex)
            {
                TempData["Result"] = $"Hata meydana geldi. {ex.Message}";
                model.Categories = _mapper.Map<List<CategoryResModel>>(_catManager.GetAll());
                model.Suppliers = _mapper.Map<List<SupplierResModel>>(_supplierManager.GetAll());
                return View(model);
            }
        

        }

        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if ((id == null) || (id <= 0))
            {
                TempData["Result"] = "Id değeri 0'dan büyük olmalıdır.";
                return RedirectToAction("GetProducts");
            }

            try
            {
                _productManager.Delete(await _productManager.FindAsync(id));
                TempData["Result"] = "Silme işlemi başarılı";
            }
            catch (Exception ex)
            {
                TempData["Result"] = $"Hata meydana geldi. {ex.Message}";
            }
            return RedirectToAction("GetProducts");
        }

        public async Task<IActionResult> DestroyProduct(int? id)
        {
            if ((id == null) || (id <= 0))
            {
                TempData["Result"] = "Id değeri 0'dan büyük olmalıdır.";
                return RedirectToAction("GetProducts");
            }

            try
            {
                ProductDTO productDto = await _productManager.FindAsync(id);
                TempData["Result"] = _productManager.Destroy(productDto);
            }
            catch (Exception ex)
            {
                TempData["Result"] = $"Hata meydana geldi. {ex.Message}";
            }
            return RedirectToAction("GetProducts");
        }
    }
}
