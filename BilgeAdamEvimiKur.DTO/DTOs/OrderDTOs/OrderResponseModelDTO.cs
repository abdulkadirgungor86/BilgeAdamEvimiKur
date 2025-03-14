using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DTO.DTOs.OrderDTOs
{
    public class OrderResponseModelDTO
    {
        public int ID { get; set; }
        public string ShippingAddress { get; set; }
        public string? Email { get; set; }
        public string? NameDescription { get; set; }
        public decimal Price { get; set; }
        public int? AppUserID { get; set; }
    }

}
