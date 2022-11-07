namespace SmartShopping.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductTag> Tags { get; set; }
        public ICollection<PriceRecord> PriceRecords { get; set; }
        public ICollection<Shop> Shops { get; set; }
    }
}
