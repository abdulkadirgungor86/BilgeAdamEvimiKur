using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VIEWMODEL.ViewModels.OrderDetailVMs.PureVms.ResponseModels
{
    public class OrderDetailResModel
    {
        public int OrderID { get; set; }
        public string ShippingAddress { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

    }
}
