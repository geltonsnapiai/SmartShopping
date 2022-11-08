namespace SmartShopping.Dtos
{
    public class ProductData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string[] Tags { get; set; }
        public string[] Shops { get; set; } 
        public float Price { get; set; }
        public DateTime TimeUpdated { get; set; }
    }
}
