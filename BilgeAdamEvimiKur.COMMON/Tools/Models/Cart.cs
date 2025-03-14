using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.COMMON.Tools.Models
{
    [Serializable]
    public class Cart
    {
        [JsonProperty("MyCart")]
        public Dictionary<int, CartItem> MyCart { get; set; }

        [JsonProperty("CartItems")]
        public List<CartItem> CartItems { get; set; }

        [JsonProperty("TotalPrice")]
        public decimal TotalPrice { get; set; }
    }
}
