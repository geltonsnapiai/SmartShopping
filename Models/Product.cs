namespace SmartShopping.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductTag> Tags { get; set; }
        public virtual ICollection<PriceRecord> PriceRecords { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }

        public Product()
        {
            Name = "";
            Tags = new List<ProductTag>();
            PriceRecords = new List<PriceRecord>();
            Shops = new List<Shop>();
        }
    }
}
