using Microsoft.EntityFrameworkCore;
using SmartShopping.Data;
using SmartShopping.Dtos;
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

        public async Task<ShopDto> AddShopAsync(string name)
        {
            Shop? shop = await _databaseContext.Shops.FirstOrDefaultAsync(e => e.Name.Equals(name));

            if (shop is not null)
                return shop.ToDto();

            shop = new Shop()
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            _databaseContext.Shops.Add(shop);
            await _databaseContext.SaveChangesAsync();

            return shop.ToDto();
        }

        public async Task<ICollection<ShopDto>> GetAllAsync()
        {
            return await _databaseContext.Shops.Select(e => e.ToDto()).ToListAsync();
        }
    }
}
