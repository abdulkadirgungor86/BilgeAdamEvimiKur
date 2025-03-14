using BilgeAdamEvimiKur.COMMON.Tools.Models;
using BilgeAdamEvimiKur.DTO.DTOs.ShoppingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Services.Abstracts
{
    public interface ICartService
    {
        void ClearAllFromCart(string key);
        CartDTO GetCartFromSession(string key);
        void SetCartForSession(string key, Cart cart);
        int GetAmountFromCart(string key, int id);
        void SetAmountFromCart(string key, int id, int amount);
        Task AddToCartAsync(string key, int id);
        void DescreaseFromCart(string ket, int id);
        void RemoveFromCart(string key, int id);
    }
}
