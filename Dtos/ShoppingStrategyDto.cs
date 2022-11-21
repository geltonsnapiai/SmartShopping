namespace SmartShopping.Dtos
{
    public class ShoppingStrategyDto
    {
        public struct ProductInfoDto
        {
            public string Name { get; set; }
            public float UnitPrice { get; set; }
            public float TotalPrice { get; set; }
            public int Amount { get; set; }
            public string? UnavailableStatus { get; set; }
        }

        public string Shop { get; set; }
        public float Price { get; set; }
        public List<ProductInfoDto> Products { get; set; }
        public List<ProductInfoDto> UnavailableProducts { get; set; }

        public ShoppingStrategyDto()
        {
            Price = 0;
            Products = new List<ProductInfoDto>();
            UnavailableProducts = new List<ProductInfoDto>();
        }
    }
}
