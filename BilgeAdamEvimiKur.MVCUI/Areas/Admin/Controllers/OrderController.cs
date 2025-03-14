using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.OrderDTOs;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.OrderVMs.PureVMs.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

namespace BilgeAdamEvimiKur.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        readonly IMapper _mapper;
        readonly IOrderManager _orderManager;

        public OrderController(IMapper mapper, IOrderManager orderManager)
        {
            _mapper = mapper;
            _orderManager = orderManager;
        }

        public IActionResult GetOrders(int? pageNumber  )
        {
            ViewBag.UserName = User.Identity.Name ?? " Guest ";
            List<OrderResModel> orderRM = _mapper.Map<List<OrderResModel>>(_orderManager.GetAll());
            IPagedList<OrderResModel> pageListOrders = orderRM.ToPagedList(pageNumber ?? 1, 6);
            return View(pageListOrders);
        }

        public async Task<IActionResult> DeleteOrder(int? id)
        {
            if (id == null) TempData["Result"] = "Silme işleminde id değeri null olamaz.";
            if (id <= 0) TempData["Result"] = "Silme işleminde id değeri sıfır ve sıfırdan küçük olamaz.";
            else
            {
                try
                {
                    _orderManager.Delete(await _orderManager.FindAsync(id));
                    TempData["Result"] = "Silme işlemi başarılı";
                }
                catch
                {
                    TempData["Result"] = "Hata : Silme işlemi başarısız";
                }
            }
            return RedirectToAction("GetOrders");
        }

        public IActionResult DestroyOrder(int? id)
        {
            if (id == null) TempData["Result"] = "Destroy işleminde id değeri null olamaz.";
            if (id <= 0) TempData["Result"] = "Destroy işleminde id değeri sıfır ve sıfırdan küçük olamaz.";
            else
            {
                try
                {
                    OrderDTO orderDTO = _orderManager.Find(id);
                    TempData["Result"] = _orderManager.Destroy(orderDTO);
                }
                catch
                {
                    TempData["Result"] = "Hata : Destroy işlemi başarısız";
                }
            }
            return RedirectToAction("GetOrders");
        }
    }
}
