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
        public IActionResult Submit ([FromBody] ProductList products)
        {
            foreach(var p in products.Products)
            {
                if(p.ProductName == null || p.Shop == null || p.Price == null || p.DateOfPurchase == null)
                {
                    return BadRequest("All fields should be filled");
                }
                else
                {
                    return Ok($"Product/Products is/are createds");
                }
            }
            return BadRequest();
            
        }
    }
}

