using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.COMMON.Tools.Models
{
    [Serializable]
    public class CartItem
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [JsonProperty("Amount")]
        public int Amount { get; set; }

        [JsonProperty("UnitPrice")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("SubTotal")]
        public decimal SubTotal => Amount * UnitPrice;

        [JsonProperty("ImagePath")]
        public string ImagePath { get; set; }

        [JsonProperty("CategoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("CategoryID")]
        public int? CategoryID { get; set; }
    }
}
