using SmartShopping.Models;

namespace SmartShopping.Dtos
{
    public static class DtoExtensions
    {
        public static PriceRecordDto ToDto(this PriceRecord record)
        {
            return new PriceRecordDto(record);
        }

        public static ShopDto ToDto(this Shop shop)
        {
            return new ShopDto(shop);
        }
    }
}
