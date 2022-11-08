using System.Text.Json.Serialization;

namespace SmartShopping.Models
{
    public class Shop : IEquatable<Shop>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore] public virtual ICollection<Product> Products { get; set; } 

        public Shop()
        {
            Products = new List<Product>();
        }

        public bool Equals(Shop? other)
        {
            if (ReferenceEquals(null, other)) return false;
            return other.Id == Id;
        }
    }
}
