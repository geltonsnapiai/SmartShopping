using System.Text.Json.Serialization;

namespace SmartShopping.Models
{
    public class Shop
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore] public virtual ICollection<Product> Products { get; set; } 

        public Shop()
        {
            Products = new List<Product>();
        }
    }
}
