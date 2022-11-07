using SmartShopping.Dtos;
using SmartShopping.Models;

namespace SmartShopping.Services
{
    public interface IProductService
    {
        public Task AddRecordAsync(ProductDto product);

        public Task<ICollection<Product>> GetProductsByShopNameAsync(string name);

        public Task<ICollection<ShopProductData>> GetProductDataByShopNameAsync(string name);
    }
}
