using SmartShopping.Models;
using System.Security.Claims;

namespace SmartShopping.Services
{
    public interface ITokenService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims);
        public string GenerateRefreshToken();
        public (string AccessToken, string RefreshToken) GenerateTokens(User user);
        public string? GetEmailFromAccessToken(string token);
    }
}
