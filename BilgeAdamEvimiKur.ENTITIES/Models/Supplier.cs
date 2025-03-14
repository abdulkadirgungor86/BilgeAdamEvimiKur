using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.ENTITIES.Models
{
    public class Supplier : BaseEntity
    {
        public string SupplierName { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // Relational Properties
        public virtual ICollection<Product> Products { get; set; }
    }
}
