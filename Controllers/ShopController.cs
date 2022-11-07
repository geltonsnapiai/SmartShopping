using Microsoft.AspNetCore.Mvc;
using SmartShopping.Models;
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
            Shop shop = await _shopService.AddShopAsync(name);
            return Ok(shop);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ICollection<Shop> shops = await _shopService.GetAllAsync();
            return Ok(shops);
        }
    }
}
