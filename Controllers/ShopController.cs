using Microsoft.AspNetCore.Mvc;
using SmartShopping.Dtos;
using SmartShopping.Services;

namespace SmartShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            ShopDto shop = await _shopService.AddShopAsync(name);
            return Ok(shop);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ICollection<ShopDto> shops = await _shopService.GetAllAsync();
            return Ok(shops);
        }
    }
}
