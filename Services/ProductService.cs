using Microsoft.EntityFrameworkCore;
using SmartShopping.Dtos;
using SmartShopping.Models;
using SmartShopping.Repositories;

namespace SmartShopping.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
            _repository.Autosave = false;
        }

        public async Task AddRecordAsync(ProductDto dto)
        {
            // TODO: simplify DTO name
            // check if product exists
            Product product = await _repository.ReadCreateNamedAsync<Product>(dto.Name);

            //// if doesn't - create one
            //if (product is null)
            //{
            //    product = new Product()
            //    {
            //        Id = Guid.NewGuid()
            //    };

            //    product.SetName(dto.Name);

            //    _databaseContext.Products.Add(product);
            //}
            // update shop shop
            Shop? shop = await _repository.Set<Shop>().FirstOrDefaultAsync(e => e.Name.Equals(dto.Shop));

            if (shop is null)
                throw new Exception("No record of shop '" + dto.Shop + "' exits");

            if (!product.Shops.Contains(shop))
            {
                product.Shops.Add(shop);
            }

            dto.GenerateTagsFromName();

            // update tags if needed
            foreach (var tagName in dto.Tags)
            {
                var tagNames = Helpers.ProcessName(tagName);

                if (!product.Tags.Any(t => t.SimplifiedName.Equals(tagNames.Simplified))) 
                {
                    ProductTag tag = await _repository.ReadCreateNamedAsync<ProductTag>(tagName);
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


            await _repository.CreateAsync<PriceRecord>(priceRecord);

            // save changes
            await _repository.SaveChangesAsync();
        }

        public async Task<ICollection<ProductDataDto>> GetAllProductsAsync()
        {
            var products = await _repository.Set<Product>().ToListAsync();
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
            Product? product = await _repository.Set<Product>().FirstOrDefaultAsync(product => product.Id.Equals(id));
            
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
            Shop? shop = await _repository.Set<Shop>().FirstOrDefaultAsync(e => e.Name.Equals(shopName));

            if (shop is null)
                throw new Exception("No record of shop '" + shopName + "' exits");

            List<ProductDataDto> productData = new List<ProductDataDto>();
            foreach (var product in shop.Products)
            {
                PriceRecord? record = product.PriceRecords.MaxBy(record => record.CheckDate);

                if (record is null)
                    continue;

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
            (_, string simplifiedSearchQuery) = Helpers.ProcessName(searchQuery);

            var products = await _repository.Set<Product>().Where(p => p.SimplifiedName.Contains(simplifiedSearchQuery)).ToListAsync();

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
            (_, string simplifiedSearchQuery) = Helpers.ProcessName(searchQuery);

            Shop? shop = await _repository.Set<Shop>().FirstOrDefaultAsync(e => e.Name.Equals(shopName));

            if (shop is null)
                throw new Exception("No record of shop '" + shopName + "' exits");

            var products = await _repository.Set<Product>().Where(p => p.DisplayName.Contains(simplifiedSearchQuery))
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
