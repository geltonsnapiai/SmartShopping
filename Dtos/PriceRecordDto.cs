using SmartShopping.Models;

namespace SmartShopping.Dtos
{
    public readonly struct PriceRecordDto
    {
        public string Shop { get; }
        public float Price { get; }
        public DateTime Date { get; }

        public PriceRecordDto(PriceRecord record)
        {
            Shop = record.Shop.Name;
            Price = record.Price;
            Date = record.CheckDate;
        }

        public PriceRecordDto(string shop, float price, DateTime date)
        {
            Shop = shop;
            Price = price;
            Date = date;
        }
    }
}
