namespace SmartShopping.Models
{
    public class ProductTag
    {
        public Guid Id { get; set; }
        public ICollection<Product> Products;
        public string Title;
    }
}
