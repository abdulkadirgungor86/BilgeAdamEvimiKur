using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DTO.DTOs.CategoryDTOs
{
    public class CategoryResponseModelDTO
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }

}
