using BilgeAdamEvimiKur.DTO.DTOs.BaseEntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DTO.DTOs.OrderDetailDTOs
{
    public class OrderDetailDTO : BaseEntityDTO
    {
        public int OrderID { get; set; }
        public string ShippingAddress { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
