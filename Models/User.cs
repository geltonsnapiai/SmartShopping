using SmartShopping.Data;
using System.Text.Json.Serialization;

namespace SmartShopping.Models
{
    public class User : IEntity
    {
        [JsonIgnore] public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        [JsonIgnore] public string PasswordHash { get; set; }
        [JsonIgnore] public string? RefreshToken { get; set; }
        [JsonIgnore] public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
