using SmartShopping.Data;

namespace SmartShopping.Models
{
    public class Product : INamedEntity
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string SimplifiedName { get; set; }
        public virtual ICollection<ProductTag> Tags { get; set; }
        public virtual ICollection<PriceRecord> PriceRecords { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }

        public Product()
        {
            DisplayName = "";
            SimplifiedName = "";
            Tags = new List<ProductTag>();
            PriceRecords = new List<PriceRecord>();
            Shops = new List<Shop>();
        }
    }
}
