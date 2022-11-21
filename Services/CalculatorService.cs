using Microsoft.EntityFrameworkCore;
using SmartShopping.Dtos;
using SmartShopping.Models;
using SmartShopping.Repositories;

namespace SmartShopping.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IRepository _repository;

        public CalculatorService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<ShoppingStrategyDto[]> GenerateShoppingStrategies(CartDto[] dtos)
        {
            var strategies = await _repository.Set<Shop>()
                .Select(e => new ShoppingStrategyDto { Shop = e.Name })
                .ToArrayAsync();

            foreach (var item in dtos)
            {
                Product? product = await _repository.ReadAsync<Product>(item.Id);
                
                if (product is null)
                {
                    var unavailableProductDto = new ShoppingStrategyDto.ProductInfoDto
                    {
                        UnavailableStatus = "There is no record of this item.",
                        Name = item.Name,
                        Amount = item.Amount,
                    };

                    foreach (var strategy in strategies)
                    {
                        strategy.UnavailableProducts.Add(unavailableProductDto);
                    }

                    continue;
                }

                foreach (var strategy in strategies)
                {
                    var shopRecords =
                        from record in product.PriceRecords
                        where record.Shop.Name == strategy.Shop
                        select record;

                    if (shopRecords is null)
                    {
                        strategy.UnavailableProducts.Add(new ShoppingStrategyDto.ProductInfoDto
                        {
                            UnavailableStatus = "There is no record of this item for this shop.",
                            Name = item.Name,
                            Amount = item.Amount,
                        });

                        continue;
                    }

                    var priceRecord = shopRecords.MaxBy(e => e.CheckDate);

                    if (priceRecord is null)
                    {
                        strategy.UnavailableProducts.Add(new ShoppingStrategyDto.ProductInfoDto
                        {
                            UnavailableStatus = "There is no record of this item for this shop.",
                            Name = item.Name,
                            Amount = item.Amount,
                        });

                        continue;
                    }

                    strategy.Products.Add(new ShoppingStrategyDto.ProductInfoDto
                    {
                        Name = item.Name,
                        Amount = item.Amount,
                        UnitPrice = priceRecord.Price,
                        TotalPrice = priceRecord.Price * item.Amount
                    });

                    strategy.Price += priceRecord.Price * item.Amount;
                }
            }

            return strategies;
        }
    }
}
