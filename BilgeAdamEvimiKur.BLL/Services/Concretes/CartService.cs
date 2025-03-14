using AutoMapper;
using BilgeAdamEvimiKur.BLL.Managers.Abstracts;
using BilgeAdamEvimiKur.BLL.Managers.Concretes;
using BilgeAdamEvimiKur.BLL.Services.Abstracts;
using BilgeAdamEvimiKur.COMMON.Tools.Models;
using BilgeAdamEvimiKur.DTO.DTOs.ShoppingDTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.BLL.Services.Concretes
{
    public class CartService : ICartService
    {
        readonly IMapper _mapper;
        readonly IProductManager _pManager;
        readonly ISessionService _sService;

        public CartService(ISessionService sessionService, IMapper mapper, IProductManager productManager)
        {
            _sService = sessionService;
            _mapper = mapper;
            _pManager = productManager;
        }

        void FinalizeCart(string key, Cart cart)
        {
            cart.CartItems = cart.MyCart.Values.ToList();
            cart.TotalPrice = cart.MyCart.Values.Sum(x => x.SubTotal);
            SetCartForSession(key, cart);
        }

        public CartDTO GetCartFromSession(string key)
        {
            return _mapper.Map<CartDTO>(_sService.GetObject<Cart>(key));
        }


        public void SetCartForSession(string key, Cart cart)
        {
            cart.CartItems = cart.MyCart.Values.ToList();
            cart.TotalPrice = cart.MyCart.Values.Sum(x => x.SubTotal);
            _sService.SetObject(key, cart);
        }


        public async Task AddToCartAsync(string key, int id)
        {
            CartItem cItem = _mapper.Map<CartItem>(await _pManager.FindAsync(id));
            CartDTO cDTO = GetCartFromSession(key) ?? new CartDTO { MyCart = new Dictionary<int, CartItemDTO>() };
            Cart cart = _mapper.Map<Cart>(cDTO);
            if (!cart.MyCart.ContainsKey(cItem.ID)) cart.MyCart.Add(cItem.ID, cItem);
            cart.MyCart[cItem.ID].Amount++;
            FinalizeCart(key, cart);
        }

        public int GetAmountFromCart(string key,int id)
        {
            CartDTO cDTO = GetCartFromSession(key);
            Cart cart = _mapper.Map<Cart>(cDTO);
            if (cart != null && cart.MyCart.ContainsKey(id))
            {
                return cart.MyCart[id].Amount;
            }
            return 0;
        }

        public void SetAmountFromCart(string key, int id, int amount)
        {
            CartDTO cDTO = GetCartFromSession(key);
            Cart cart = _mapper.Map<Cart>(cDTO);
            if(amount == 0) cart.MyCart.Remove(id);
            else cart.MyCart[id].Amount = amount;
            FinalizeCart(key, cart);
        }

        public void DescreaseFromCart(string key, int id)
        {
            CartDTO cDTO = GetCartFromSession(key);
            Cart cart = _mapper.Map<Cart>(cDTO);
            cart.MyCart[id].Amount--;
            if (cart.MyCart[id].Amount == 0) cart.MyCart.Remove(id);
            FinalizeCart(key, cart);
        }

        public void RemoveFromCart(string key, int id)
        {
            CartDTO cDTO = GetCartFromSession(key);
            Cart cart = _mapper.Map<Cart>(cDTO);
            cart.MyCart.Remove(id);
            FinalizeCart(key, cart);
        }

        public void ClearAllFromCart(string key)
        {
            CartDTO cDTO = GetCartFromSession(key);
            Cart cart = _mapper.Map<Cart>(cDTO);
            cart.MyCart.Clear();
            FinalizeCart(key, cart);
        }
    }
}
