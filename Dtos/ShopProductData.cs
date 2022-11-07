namespace SmartShopping.Dtos
{
    public class ShopProductData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string[] Tags { get; set; }
        public float Price { get; set; }
        public DateTime TimeUpdated { get; set; }
    }
}
