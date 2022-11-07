using System;

namespace SmartShopping.Dtos
{
    public class ProductDto
    {
        public string? ProductName { get; set; }
        public string[] Tags { get; set; }
        public double Price { get; set; }
        public string? Shop { get; set; }
        public DateTime? DateOfPurchase { get; set; }
    }
 }