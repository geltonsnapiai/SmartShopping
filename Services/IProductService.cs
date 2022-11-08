using SmartShopping.Dtos;
using SmartShopping.Models;
using System.Collections;

namespace SmartShopping.Services
{
    public interface IProductService
    {
        public Task AddRecordAsync(ProductDto product);

        public Task<ICollection<ProductDataDto>> GetAllProductsAsync();
        
        public Task<ICollection<ProductDataDto>> GetShopProductsAsync(string shopName);

        public Task<ICollection<ProductDataDto>> SearchProductsAsync(string searchQuery);

        public Task<ICollection<ProductDataDto>> SearchShopProductsAsync(string shop, string searchQuery);

        public Task<ICollection<PriceRecordDto>> GetProductPriceRecordsAsync(Guid id);

        public Task<ICollection<PriceRecordDto>> GetShopProductPriceRecordsAsync(Guid id, string shop);
    }
}
