using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.ENTITIES.Models
{
    public abstract class BasePriceSpec : BaseEntity
    {
        public decimal Price { get; set; }
    }
}
