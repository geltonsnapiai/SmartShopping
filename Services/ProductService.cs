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
            Product product = await _databaseContext.Products.FirstOrDefaultAsync(e => e.Name.Equals(dto.Name));

            // if doesn't - create one
            if (product is null)
            {
                product = new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name
                };

                _databaseContext.Products.Add(product);
            }
            // update shop shop
            Shop shop = await _databaseContext.Shops.FirstOrDefaultAsync(e => e.Name.Equals(dto.Shop));

            if (shop is null)
                throw new Exception("No record of shop '" + dto.Shop + "' exits");

            if (!product.Shops.Contains(shop))
            {
                product.Shops.Add(shop);
                //shop.Products.Add(product);
            }

            // update tags if needed
            foreach (var tagTitle in dto.Tags)
            {
                if (!product.Tags.Any(t => t.Title.Equals(tagTitle))) {
                    ProductTag tag = await _databaseContext.ProductTags.FirstOrDefaultAsync(e => e.Title.Equals(tagTitle));
                    if (tag is null)
                    {
                        tag = new ProductTag
                        {
                            Id = Guid.NewGuid(),
                            Title = tagTitle
                        };
                        _databaseContext.ProductTags.Add(tag);
                    }
                    
                    product.Tags.Add(tag);
                    //tag.Products.Add(product);
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

        public async Task<ICollection<ShopProductData>> GetProductDataByShopNameAsync(string name)
        {
            ICollection<Product> products = await GetProductsByShopNameAsync(name);
            List<ShopProductData> productData = new List<ShopProductData>();
            foreach (var product in products)
            {
                List<string> tags = new List<string>();
                foreach (var tag in product.Tags)
                    tags.Add(tag.Title);

                PriceRecord? record = product.PriceRecords.MaxBy(record => record.CheckDate);

                productData.Add(new ShopProductData
                {
                    Id = product.Id,
                    Name = product.Name,
                    Tags = tags.ToArray(),
                    Price = record is null ? float.NaN : record.Price,
                    TimeUpdated = record is null ? DateTime.MinValue : record.CheckDate
                });
            }

            return productData;
        }

        public async Task<ICollection<Product>> GetProductsByShopNameAsync(string name)
        {
            Shop shop = await _databaseContext.Shops.FirstOrDefaultAsync(e => e.Name.Equals(name));

            if (shop is null)
                throw new Exception("No record of shop '" + name + "' exits");

            return shop.Products;
        }
    }
}
