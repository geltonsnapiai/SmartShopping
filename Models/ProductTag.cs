using SmartShopping.Data;

namespace SmartShopping.Models
{
    public class ProductTag : INamedEntity
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string SimplifiedName { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public ProductTag()
        {
            Products = new List<Product>();
        }
    }
}
