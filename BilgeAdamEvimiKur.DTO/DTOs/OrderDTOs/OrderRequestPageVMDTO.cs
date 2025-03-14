using BilgeAdamEvimiKur.DTO.DTOs.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DTO.DTOs.OrderDTOs
{
    public class OrderRequestPageVMDTO
    {
        public OrderResponseModelDTO Order { get; set; }
        public PaymentRequestModelDTO Payment { get; set; }
    }

}
