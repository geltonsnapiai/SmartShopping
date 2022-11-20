using Microsoft.EntityFrameworkCore;
using SmartShopping.Data;
using SmartShopping.Dtos;
using SmartShopping.Models;
using SmartShopping.Repositories;

namespace SmartShopping.Services
{
    public class ShopService : IShopService
    {
        private readonly IRepository _repository;

        public ShopService(IRepository repository)
        {
            _repository = repository;
            _repository.Autosave = true;
        }

        public async Task<ShopDto> AddShopAsync(string name)
        {
            var shop = await _repository.CreateAsync<Shop>(new Shop
            {
                Name = name,
            });

            return shop.ToDto();
        }

        public async Task<ICollection<ShopDto>> GetAllAsync()
        {
            return await _repository.Set<Shop>().Select(e => e.ToDto()).ToListAsync();
        }
    }
}
