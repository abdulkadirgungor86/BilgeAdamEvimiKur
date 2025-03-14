using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DTO.DTOs.ShoppingDTOs
{
    [Serializable]
    public class CartDTO
    {
        [JsonProperty("MyCart")]
        public Dictionary<int, CartItemDTO> MyCart { get; set; }
        [JsonProperty("CartItems")]
        public List<CartItemDTO> CartItems { get; set; }
        [JsonProperty("TotalPrice")]
        public decimal TotalPrice { get; set; }
    }
}
