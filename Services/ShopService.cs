using Microsoft.EntityFrameworkCore;
using SmartShopping.Data;
using SmartShopping.Models;

namespace SmartShopping.Services
{
    public class ShopService : IShopService
    {
        private readonly DatabaseContext _databaseContext;

        public ShopService(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        public async Task<Shop> AddShopAsync(string name)
        {
            Shop? shop = await _databaseContext.Shops.FirstOrDefaultAsync(e => e.Name.Equals(name));

            if (shop is not null)
                return shop;

            shop = new Shop()
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            _databaseContext.Shops.Add(shop);
            await _databaseContext.SaveChangesAsync();

            return shop;
        }

        public async Task<List<Shop>> GetAllAsync()
        {
            return await _databaseContext.Shops.ToListAsync();
        }
    }
}
