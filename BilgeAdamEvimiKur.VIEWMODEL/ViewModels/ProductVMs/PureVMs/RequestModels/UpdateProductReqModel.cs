using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.VIEWMODEL.ViewModels.ProductVMs.PureVMs.RequestModels
{
    public class UpdateProductReqModel
    {
        public int ID { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? UnitsInStock { get; set; }
        public int? CategoryID { get; set; }
        public string ImagePath { get; set; }
        public int SupplierID { get; set; }
    }
}
