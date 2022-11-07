using SmartShopping.Models;

namespace SmartShopping.Services
{
    public interface IShopService
    {
        /*
         * Returns shop created or one exists returns it
         */
        public Task<Shop> AddShopAsync(string name);

        public Task<ICollection<Shop>> GetAllAsync();
    }
}
