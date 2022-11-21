using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartShopping.Dtos;
using SmartShopping.Services;

namespace SmartShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : Controller
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> GenerateShoppingStrategies([FromBody] CartDto[] dtos)
        {
            var strategies = await _calculatorService.GenerateShoppingStrategies(dtos);
            return Ok(strategies);
        }
    }
}
