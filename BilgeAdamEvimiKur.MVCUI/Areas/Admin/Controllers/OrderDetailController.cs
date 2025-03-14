using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.BLL.Managers.Concretes;
using BilgeAdamEvimiKur.ENTITIES.Models;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.OrderDetailVMs.PureVms.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

namespace BilgeAdamEvimiKur.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderDetailController : Controller
    {
        readonly IMapper _mapper;
        readonly IOrderDetailManager _orderDetailManager;


        public OrderDetailController(IMapper mapper, IOrderDetailManager orderDetailManager)
        {
            _mapper = mapper;
            _orderDetailManager = orderDetailManager;
        }


        public IActionResult GetOrderDetails(int? pageNumber)
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            List<OrderDetailResModel> oDRM = _mapper.Map<List<OrderDetailResModel>>(_orderDetailManager.GetAll());
            IPagedList<OrderDetailResModel> pageListOrderDetail = oDRM.ToPagedList(pageNumber ?? 1, 6);
            return View(pageListOrderDetail);
        }


        public async Task<IActionResult> DeleteOrderDetail(int? orderID, int? productID)
        {
            if (orderID == null)
            {
                TempData["Result"] = "Silme işleminde orderID değeri null olamaz.";
                return RedirectToAction("GetOrderDetails");
            }
            if (productID == null)
            {
                TempData["Result"] = "Silme işleminde productID değeri null olamaz.";
                return RedirectToAction("GetOrderDetails");
            }
            if (orderID <= 0)
            {
                TempData["Result"] = TempData["Result"] = "Silme işleminde orderID değeri sıfır ve sıfırdan küçük olamaz."; ;
                return RedirectToAction("GetOrderDetails");
            }
            if (productID <= 0)
            {
                TempData["Result"] = TempData["Result"] = "Silme işleminde productID değeri sıfır ve sıfırdan küçük olamaz.";
                return RedirectToAction("GetOrderDetails");
            }
            try
            {
                _orderDetailManager.Delete(await _orderDetailManager.FindAsync(orderID, productID));
                TempData["Result"] = "Silme işlemi başarılı";
            }
            catch
            {
                TempData["Result"] = "Hata : Silme işlemi başarısız";
            }
            return RedirectToAction("GetOrderDetails");
        }

        public async Task<IActionResult> DestroyOrderDetail(int? orderID, int? productID)
        {
            if (orderID == null)
            {
                TempData["Result"] = "Destroy işleminde orderID değeri null olamaz.";
                return RedirectToAction("GetOrderDetails");
            }
            if (productID == null)
            {
                TempData["Result"] = "Destroy işleminde productID değeri null olamaz.";
                return RedirectToAction("GetOrderDetails");
            }
            if (orderID <= 0)
            {
                TempData["Result"] = TempData["Result"] = "Destroy işleminde orderID değeri sıfır ve sıfırdan küçük olamaz."; ;
                return RedirectToAction("GetOrderDetails");
            }
            if (productID <= 0)
            {
                TempData["Result"] = TempData["Result"] = "Destroy işleminde productID değeri sıfır ve sıfırdan küçük olamaz.";
                return RedirectToAction("GetOrderDetails");
            }
            try
            {
                TempData["Result"] = _orderDetailManager.Destroy(await _orderDetailManager.FindAsync(orderID, productID));
            }
            catch
            {
                TempData["Result"] = "Hata : Destroy işlemi başarısız";
            }
            return RedirectToAction("GetOrderDetails");
        }
    }
}
