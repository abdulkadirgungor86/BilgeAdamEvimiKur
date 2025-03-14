using BilgeAdamEvimiKur.DTO.DTOs.OrderDTOs;
using BilgeAdamEvimiKur.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Managers.Abstracts
{
    public interface IOrderManager : IManager<Order, OrderDTO>
    {
        Task<bool> ConfirmeOrderAsync(OrderRequestPageVMDTO orderRequestPageVMDTO, string userID);
        decimal GetTotalPrice();
    }

}
