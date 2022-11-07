using Microsoft.AspNetCore.Mvc;
using SmartShopping.Dtos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ILogger<AuthController> _logger;

        public ProductController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }


        // GET: /<controller>/
        [HttpPost("submit")]
        public IActionResult Submit([FromBody] List<ProductDto> products)
        {
            foreach (var p in products)
            {
                if (string.IsNullOrEmpty(p.ProductName) || string.IsNullOrEmpty(p.Shop) 
                        || double.IsNaN(p.Price) || !p.DateOfPurchase.HasValue)
                    return BadRequest("All fields should be filled");
            }
            
            return Ok();
        }
    }
}