using SmartShopping.Dtos;
using SmartShopping.Models;

namespace SmartShopping.Services
{
    public interface IUserService
    {
        public Task<(string AccessToken, string RefreshToken)> RegisterUserAsync(RegisterDto registerData);
        public Task<(string AccessToken, string RefreshToken)> LoginUserAsync(LoginDto loginData);
        public Task<(string AccessToken, string RefreshToken)> RefreshTokensAsync(TokenDto oldTokens);
        public Task RevokeTokensByUserIdAsync(Guid id);
        public Task DeleteUserByIdAsync(Guid id);
        public Task<User> GetUserByIdAsync(Guid id);
    }

    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message)
        {

        }
    }
}
