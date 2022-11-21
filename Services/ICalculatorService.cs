using SmartShopping.Dtos;

namespace SmartShopping.Services
{
    public interface ICalculatorService
    {
        Task<ShoppingStrategyDto[]> GenerateShoppingStrategies(CartDto[] dtos);
    }
}
