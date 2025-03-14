using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.BLL.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PureVMs.ResponseModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.CategoryVMs.PureVMs.ResponseModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ShoppingVMs.PageVMs;
using X.PagedList.Extensions;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.OrderVMs.PageVMs;
using BilgeAdamEvimiKur.DTO.DTOs.OrderDTOs;
using System.Security.Claims;
using BilgeAdamEvimiKur.DTO.DTOs.ProductDTOs;
using BilgeAdamEvimiKur.COMMON.Tools.Models;

namespace BilgeAdamEvimiKur.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        readonly IMapper _mapper;
        readonly ICartService _cartService;
        readonly IProductManager _productManager;
        readonly ICategoryManager _categoryManager;
        readonly IOrderManager _orderManager;
        readonly IAppUserManager _userManager;

        public ShoppingController(IMapper mapper, ICartService cartService, IProductManager productManager, ICategoryManager categoryManager, IOrderManager orderManager, IAppUserManager userManager)
        {
            _mapper = mapper;
            _cartService = cartService;
            _productManager = productManager;
            _categoryManager = categoryManager;
            _orderManager = orderManager;
            _userManager = userManager;
        }


        public IActionResult Index(int? pageNumber, int? categoryPageNumber, int? categoryID)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            List<ProductResModel> products = _mapper.Map < List < ProductResModel >>
            (categoryID == null ? _productManager.GetActives() : _productManager.Where(x => x.CategoryID == categoryID && x.Status != ENTITIES.Enums.DataStatus.Deleted));
            List<CategoryResModel> categories = _mapper.Map<List<CategoryResModel>>(_categoryManager.GetActives());

            ShoppingPageVM shoppingPageVM = new ShoppingPageVM()
            {
                Categories = categories.ToPagedList(categoryPageNumber ?? 1, 8),
                Products = products.ToPagedList(pageNumber ?? 1 , 8)
            };

            TempData["CategoryID"] = categoryID;
            TempData["PageNumber"] = pageNumber;
            return View(shoppingPageVM);
        }

        public IActionResult CartPage()
        {
            CartPageVM cartPageVM = _mapper.Map<CartPageVM>(_cartService.GetCartFromSession("scart"));
            if(cartPageVM == null || cartPageVM.CartItems.Count == 0 || cartPageVM.TotalPrice <= 0 )
            {
                TempData["Result"] = "Alışveriş sepetiniz boş.";
                return RedirectToAction("Index");
            }

            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            bool isExceed = false;
            TempData["Result"] = "";

            foreach (CartItem cItem in cartPageVM.CartItems)
            {
                ProductDTO productDTO = _productManager.Find(cItem.ID);
                if(productDTO.UnitsInStock < cItem.Amount)
                {
                    isExceed = true;
                    _cartService.SetAmountFromCart("scart", cItem.ID, productDTO.UnitsInStock);
                    TempData["Result"] += $" \"{cItem.ProductName}\" "; 
                }
            }

            if (isExceed)
            {
                cartPageVM = _mapper.Map<CartPageVM>(_cartService.GetCartFromSession("scart"));
                if (cartPageVM == null || cartPageVM.CartItems.Count == 0 || cartPageVM.TotalPrice <= 0)
                {
                    TempData["Result"] = "Alışveriş sepetiniz boş.";
                    return RedirectToAction("Index");
                }
                
                TempData["Result"] += "ürün(ü/leri) stok değeri aştığı için, alış adetleri stok miktarına eşitlendi.";
            }
            
            return View(cartPageVM);
        }

        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id != null && id.Value > 0)
            {
                ProductDTO productDTO = _productManager.Find(id.Value);
                if ((productDTO.UnitsInStock > 0) && (productDTO.UnitsInStock > _cartService.GetAmountFromCart("scart", id.Value)))
                {
                    await _cartService.AddToCartAsync("scart", id.Value);
                    TempData["Result"] = "Ürününüz sepete eklendi.";
                }
                else TempData["Result"] = $"\"{productDTO.ProductName}\" ürününün stoğu bitti!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult DecreaseFromCart(int? id) 
        {
            if (id != null && id.Value > 0) _cartService.DescreaseFromCart("scart", id.Value);
            return RedirectToAction("CartPage");
        }

        public IActionResult DeleteFromCart(int? id)
        {
            if (id != null && id.Value > 0)  _cartService.RemoveFromCart("scart", id.Value);
            return RedirectToAction("CartPage");
        }

        public IActionResult ConfirmOrder()
        {
            if (_orderManager.GetTotalPrice() > 0)
            {
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View();
            }
            TempData["Result"] = "Hiçbir ürün almadan ödeme sayfasına geçemezsiniz.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(OrderPageVM orderPageVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserName = User.Identity.Name ?? " Guest ";
                return View(orderPageVM);
            }

            OrderRequestPageVMDTO orderRPVMDTO = _mapper.Map<OrderRequestPageVMDTO>(orderPageVM);

            bool res = false;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            try
            {
                res = await _orderManager.ConfirmeOrderAsync(orderRPVMDTO, userId);
            } catch (Exception ex)
            {
                TempData["Result"] = $"Hata : {ex.Message}";
                return RedirectToAction("Index");
            }

            if (res) TempData["Result"] = "Şiparişiniz başarıyla alınmıştır.";
            else TempData["Result"] = "Siparis işlemi sırasında bir hata oluştu.";
            return RedirectToAction("Index");
        }

    }
}
