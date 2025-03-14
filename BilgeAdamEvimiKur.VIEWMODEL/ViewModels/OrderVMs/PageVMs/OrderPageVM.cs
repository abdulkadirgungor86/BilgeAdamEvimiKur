using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.OrderVMs.PureVMs.ResponseModels;
using BilgeAdamEvimiKur.VIEWMODEL.ViewModels.PaymentVMs.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VIEWMODEL.ViewModels.OrderVMs.PageVMs
{
    public class OrderPageVM
    {
        public OrderReqModel Order { get; set; }
        public PaymentReqModel Payment { get; set; }
    }

}
