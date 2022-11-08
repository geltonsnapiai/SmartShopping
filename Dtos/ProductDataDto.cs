using SmartShopping.Models;

namespace SmartShopping.Dtos
{
    public readonly struct ProductDataDto
    {
        public Guid Id { get; }
        public string Name { get; }
        public string[] Tags { get; }
        public PriceRecordDto[] Prices { get; }
        
        public ProductDataDto(Guid id, string name, string[] tags, PriceRecordDto[] prices)
        {
            Id = id;
            Name = name;
            Tags = tags;
            Prices = prices;
        }
    }
}
