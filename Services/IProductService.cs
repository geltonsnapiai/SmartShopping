using SmartShopping.Dtos;
using SmartShopping.Models;

namespace SmartShopping.Services
{
    public interface IProductService
    {
        public Task AddRecordAsync(ProductDto product);

        public Task<ICollection<ProductData>> GetAllProductsAsync();
        
        public Task<ICollection<ProductData>> GetShopProductsAsync(string shopName);

        public Task<ICollection<ProductData>> SearchProductsAsync(string searchQuery);

        public Task<ICollection<ProductData>> SearchShopProductsAsync(string shop, string searchQuery);
    }
}
