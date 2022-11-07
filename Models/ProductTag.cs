namespace SmartShopping.Models
{
    public class ProductTag
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public ProductTag()
        {
            Products = new List<Product>();
        }
    }
}
