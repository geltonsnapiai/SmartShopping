using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartShopping.Dtos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        // GET: /<controller>/
        [HttpPost("submit")]
        public IActionResult Submit([FromBody] ProductList products)
        {
            foreach (var p in products.Products)
            {
                if (String.IsNullOrEmpty(p.ProductName) || String.IsNullOrEmpty(p.Shop) || Double.IsNaN(p.Price) || !p.DateOfPurchase.HasValue)
                    return BadRequest("All fields should be filled");
            }

            return Ok("Objects are created.");

        }
    }
}