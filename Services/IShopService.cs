using SmartShopping.Dtos;

namespace SmartShopping.Services
{
    public interface IShopService
    {
        /*
         * Returns shop created or one exists returns it
         */
        public Task<ShopDto> AddShopAsync(string name);

        public Task<ICollection<ShopDto>> GetAllAsync();
    }
}
