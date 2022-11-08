using SmartShopping.Models;
using System.Runtime.CompilerServices;

namespace SmartShopping.Dtos
{
    public readonly struct ShopDto
    {
        public Guid Id { get; } 
        public string Name { get; } 

        public ShopDto(Shop shop)
        {
            Id = shop.Id;
            Name = shop.Name;
        }
    }
}
