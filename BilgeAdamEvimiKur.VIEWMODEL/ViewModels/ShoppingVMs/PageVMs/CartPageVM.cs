using BilgeAdamEvimiKur.COMMON.Tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ShoppingVMs.PageVMs
{
    public class CartPageVM
    {
        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
