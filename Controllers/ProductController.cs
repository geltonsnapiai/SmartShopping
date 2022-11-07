using Microsoft.AspNetCore.Mvc;
using SmartShopping.Dtos;
using SmartShopping.Models;
using SmartShopping.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<AuthController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }


        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] List<ProductDto> products)
        {
            foreach (var p in products)
            {
                if (string.IsNullOrEmpty(p.Name) || string.IsNullOrEmpty(p.Shop) 
                        || double.IsNaN(p.Price))
                {
                    continue;
                }

                await _productService.AddRecordAsync(p);
            }
            
            return Ok();
        }

        /*
         * Get products:
         *      if shop parameter specified
         */
        [HttpGet]
        public async Task<IActionResult> Get(string shop)
        {
            try
            {
                if (!string.IsNullOrEmpty(shop))
                {
                    ICollection<ShopProductData> products = await _productService.GetProductDataByShopNameAsync(shop);

                    return Ok(products);
                }

                return BadRequest("Specify shop name");
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}