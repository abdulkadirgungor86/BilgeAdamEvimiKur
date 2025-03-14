using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.ENTITIES.Models
{
    public class Order : BasePriceSpec
    {
        public string ShippingAddress { get; set; }
        public string? Email { get; set; }
        public string? NameDescripton { get; set; }
        public int? AppUserID { get; set; }

        // Relational Properties
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
