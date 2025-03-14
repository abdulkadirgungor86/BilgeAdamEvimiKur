using BilgeAdamEvimiKur.DTO.DTOs.BaseEntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DTO.DTOs.ProductDTOs
{
    public class ProductDTO : BaseEntityDTO
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public string ImagePath { get; set; }
        public string  SupplierName { get; set; }
        public int SupplierID { get; set; }

    }
}
