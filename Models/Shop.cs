using System.Text.Json.Serialization;

namespace SmartShopping.Models
{
    public class Shop
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore] public ICollection<Product> Products { get; set; } 
    }
}
