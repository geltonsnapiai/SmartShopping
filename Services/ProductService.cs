using Microsoft.EntityFrameworkCore;
using SmartShopping.Data;
using SmartShopping.Dtos;
using SmartShopping.Models;

namespace SmartShopping.Services
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _databaseContext;

        public ProductService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AddRecordAsync(ProductDto dto)
        {
            // check if product exists
            Product product = await _databaseContext.Products.FirstOrDefaultAsync(e => e.SimplifiedName.Equals(dto.Name));

            // if doesn't - create one
            if (product is null)
            {
                product = new Product()
                {
                    Id = Guid.NewGuid()
                };

                product.SetName(dto.Name);

                _databaseContext.Products.Add(product);
            }
            // update shop shop
            Shop shop = await _databaseContext.Shops.FirstOrDefaultAsync(e => e.Name.Equals(dto.Shop));

            if (shop is null)
                throw new Exception("No record of shop '" + dto.Shop + "' exits");

            if (!product.Shops.Contains(shop))
            {
                product.Shops.Add(shop);
            }

            // update tags if needed
            foreach (var tagName in dto.Tags)
            {
                if (!product.Tags.Any(t => t.SimplifiedName.Equals(tagName))) {
                    ProductTag tag = await _databaseContext.ProductTags.FirstOrDefaultAsync(e => e.SimplifiedName.Equals(tagName));
                    if (tag is null)
                    {
                        tag = new ProductTag
                        {
                            Id = Guid.NewGuid()
                        };

                        tag.SetName(tagName);
                        _databaseContext.ProductTags.Add(tag);
                    }
                    
                    product.Tags.Add(tag);
                }
            }

            // add price record
            PriceRecord priceRecord = new PriceRecord
            {
                Id = Guid.NewGuid(),
                Product = product,
                Shop = shop,
                Price = dto.Price,
                CheckDate = dto.DateOfPurchase,
                UploadDate = DateTime.UtcNow
            };

            _databaseContext.PriceRecords.Add(priceRecord);

            // save changes
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<ICollection<ProductDataDto>> GetAllProductsAsync()
        {
            var products = await _databaseContext.Products.ToListAsync();
            return products.Select(product => new ProductDataDto(
                product.Id,
                product.DisplayName,
                product.Tags.Select(t => t.DisplayName).ToArray(),
                product.Shops.Select(s => {
                    return new PriceRecordDto(product.PriceRecords.Where(r => r.Shop.Equals(s))
                        .MaxBy(r => r.CheckDate));
                }).ToArray()
            ))
                .ToList();
        }

        public async Task<ICollection<PriceRecordDto>> GetProductPriceRecordsAsync(Guid id)
        {
            Product? product = await _databaseContext.Products.FirstOrDefaultAsync(product => product.Id.Equals(id));
            
            if (product is null)
                throw new Exception("Product with id: " + id.ToString() + " does not exist.");

            return product.PriceRecords.Select(e => e.ToDto()).ToList();
        }

        public Task<ICollection<PriceRecordDto>> GetShopProductPriceRecordsAsync(Guid id, string shop)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ProductDataDto>> GetShopProductsAsync(string shopName)
        {
            Shop shop = await _databaseContext.Shops.FirstOrDefaultAsync(e => e.Name.Equals(shopName));

            if (shop is null)
                throw new Exception("No record of shop '" + shopName + "' exits");

            List<ProductDataDto> productData = new List<ProductDataDto>();
            foreach (var product in shop.Products)
            {
                PriceRecord? record = product.PriceRecords.MaxBy(record => record.CheckDate);

                productData.Add(new ProductDataDto(
                    product.Id,
                    product.DisplayName,
                    product.Tags.Select(t => t.DisplayName).ToArray(),
                    new PriceRecordDto[] { new PriceRecordDto(record) }
                ));
            }

            return productData;
        }

        public async Task<ICollection<ProductDataDto>> SearchProductsAsync(string searchQuery)
        {
            string simplifiedSearchQuery = Helpers.Simplify(searchQuery);

            var products = await _databaseContext.Products.Where(p => p.SimplifiedName.Contains(simplifiedSearchQuery)).ToListAsync();

            return products.Select(product => new ProductDataDto(
                product.Id,
                product.DisplayName,
                product.Tags.Select(t => t.DisplayName).ToArray(),
                product.Shops.Select(s => {
                    return new PriceRecordDto(product.PriceRecords.Where(r => r.Shop.Equals(s))
                        .MaxBy(r => r.CheckDate));
                }).ToArray()
            ))
                .ToList();
        }

        public async Task<ICollection<ProductDataDto>> SearchShopProductsAsync(string shopName, string searchQuery)
        {
            string simplifiedSearchQuery = Helpers.Simplify(searchQuery);

            Shop shop = await _databaseContext.Shops.FirstOrDefaultAsync(e => e.Name.Equals(shopName));

            if (shop is null)
                throw new Exception("No record of shop '" + shopName + "' exits");

            var products = await _databaseContext.Products.Where(p => p.DisplayName.Contains(simplifiedSearchQuery))
                .Where(p => p.Shops.Contains(shop))
                .ToListAsync();

            return products.Select(product => new ProductDataDto(
                product.Id,
                product.DisplayName,
                product.Tags.Select(t => t.DisplayName).ToArray(),
                product.Shops.Select(s => {
                    return new PriceRecordDto(product.PriceRecords.Where(r => r.Shop.Equals(s))
                        .MaxBy(r => r.CheckDate));
                }).ToArray()
            ))
                .ToList();
        }
    }
}
