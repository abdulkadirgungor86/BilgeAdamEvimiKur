using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VIEWMODEL.ViewModels.OrderVMs.PureVMs.ResponseModels
{
    public class OrderResModel
    {
        public int ID { get; set; }
        public string ShippingAddress { get; set; }
        public decimal Price { get; set; }
        public string? Email { get; set; }
        public string? NameDescription { get; set; }
        public int? AppUserID { get; set; }
        public string? AppUserName { get; set; }
        public string Status {  get; set; }
    }
}
