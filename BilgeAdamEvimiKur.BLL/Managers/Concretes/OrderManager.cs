using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.BLL.Services.Abstracts;
using BilgeAdamEvimiKur.COMMON.Tools.Services;
using BilgeAdamEvimiKur.DAL.Repositories.Abstracts;
using BilgeAdamEvimiKur.DTO.DTOs.OrderDetailDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.OrderDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.ProductDTOs;
using BilgeAdamEvimiKur.DTO.DTOs.ShoppingDTOs;
using BilgeAdamEvimiKur.ENTITIES.Enums;
using BilgeAdamEvimiKur.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Managers.Concretes
{
    public class OrderManager : BaseManager<Order, OrderDTO>, IOrderManager
    {
        readonly IOrderRepository _ordRep;
        readonly ICartService _cartService;
        readonly IApiService _apiService;
        readonly UserManager<AppUser> _userManager;
        readonly IOrderDetailManager _orderDetailManager;
        readonly IMapper _mapper;
        readonly IProductManager _productManager;

        public OrderManager(IOrderRepository ordRep, IMapper mapper, IApiService apiService, UserManager<AppUser> userManager, ICartService cartService, IOrderDetailManager orderDetailManager, IProductManager productManager) : base(mapper, ordRep)
        {
            _ordRep = ordRep;
            _userManager = userManager;
            _orderDetailManager = orderDetailManager;
            _cartService = cartService;
            _apiService = apiService;
            _mapper = mapper;
            _productManager = productManager;
        }

        public override string Destroy(OrderDTO item)
        {
            Order entity = _mapper.Map<Order>(item);
            if (entity.Status == DataStatus.Deleted)
            {
                try
                {
                    Order order = _ordRep.FirstOrDefault(e => e.ID == item.ID);
                    if (order != null)
                    {
                        _ordRep.Destroy(order);
                        return "Destroy işlemi başarılı";
                    }
                    return "Hata :  product nesnesi  null geldi.  OrderManager/Destroy/order";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "Veriyi yok etmek için önce silmeniz lazım";
        }

        public async Task<bool> ConfirmeOrderAsync(OrderRequestPageVMDTO oVMDTO, string userID)
        {
            List<string> keys = new List<string> { "creditCardApiUrl" };
            Dictionary<string, string> configSettings = await JsonService.ReadFromFileAsync(keys);
            string link = configSettings["creditCardApiUrl"];

            CartDTO cDTO = _cartService.GetCartFromSession("scart");
            if (cDTO == null)  return false;
            if (cDTO.TotalPrice <= 0) return false;
            
            // Satış öncesi stok kontrolü başlangıç
            foreach (CartItemDTO item in cDTO.CartItems)
            {
                ProductDTO productDTO = _productManager.Find(item.ID);
                if (productDTO.UnitsInStock < item.Amount) return false;
            }
            // Satış öncesi stok kontrolü bitiş

            oVMDTO.Order.Price = oVMDTO.Payment.ShoppingPrice = cDTO.TotalPrice;
            (bool isSuccess, string responseBody) = await _apiService.MakePostRequestAsync($"{link}", oVMDTO.Payment);

            if (isSuccess)
            {
                if (!string.IsNullOrEmpty(userID))
                {
                    AppUser appUser = await _userManager.FindByIdAsync(userID);
                    oVMDTO.Order.AppUserID = appUser.Id;
                }

                OrderDTO addedOrder = await AddAndGetAsync(_mapper.Map<OrderDTO>(oVMDTO.Order));

                foreach (CartItemDTO item in cDTO.CartItems)
                {

                    // Satış sonrası stok kontrolü ve ayarlamaları başlangıç
                    ProductDTO productDTO = _productManager.Find(item.ID);
                    if (productDTO.UnitsInStock < item.Amount)
                    {
                        // throw new Exception($"Hata : \"{item.ProductName}\" adlı ürün stok miktarından fazla şatış adedine sahip."); //***Hata fırlatmasın ve kalan işlemleri tamamlasın. Burada hata fırlatmaktan ziyade iyi bir log yönetimi lazım***
                         productDTO.UnitsInStock = 0;
                    }
                    else productDTO.UnitsInStock -= item.Amount;
                    await _productManager.UpdateAsync(productDTO);
                    // Satış sonrası stok kontrolü ve ayarlamaları bitiş

                    OrderDetailDTO odDTO = _mapper.Map<OrderDetailDTO>(item);
                    odDTO.OrderID = addedOrder.ID;
                    
                    await _orderDetailManager.AddAsync(odDTO);
                }
                _cartService.ClearAllFromCart("scart");
                return true;
            }
            return false;
        }

        public decimal GetTotalPrice()
        {
            CartDTO cDTO = _cartService.GetCartFromSession("scart");
            if (cDTO != null) if (cDTO.TotalPrice >= 0) return cDTO.TotalPrice;
            return 0;
        }


    }
}
