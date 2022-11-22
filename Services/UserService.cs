using Microsoft.EntityFrameworkCore;
using SmartShopping.Dtos;
using SmartShopping.Models;
using SmartShopping.Repositories;

namespace SmartShopping.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IValidationService _validationService;

        public UserService(IRepository repository, ITokenService tokenService, IValidationService validationService)
        {
            _repository = repository;
            _tokenService = tokenService;
            _validationService = validationService;

            repository.Autosave = true;
        }

        public async Task DeleteUserByIdAsync(Guid id)
        {
            User? user = await _repository.ReadAsync<User>(id);
            if (user is not null)
                await _repository.DeleteAsync(user);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            User? user = await _repository.ReadAsync<User>(id);

            if (user is null)
                throw new AuthenticationException("There is no such user.");

            return user;
        }

        public async Task<(string AccessToken, string RefreshToken)> LoginUserAsync(LoginDto loginData)
        {
            var user = await _repository.Set<User>().FirstOrDefaultAsync(e => e.Email.Equals(loginData.Email));

            if (user is null || !BCrypt.Net.BCrypt.Verify(loginData.Password, user.PasswordHash))
                throw new AuthenticationException("Email or password is incorrect");

            var tokens = _tokenService.GenerateTokens(user);

            user.RefreshToken = tokens.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _repository.UpdateAsync(user);

            return tokens;
        }

        public async Task<(string AccessToken, string RefreshToken)> RefreshTokensAsync(TokenDto oldTokens)
        {
            string accessToken = oldTokens.AccessToken;
            string refreshToken = oldTokens.RefreshToken;

            var id = _tokenService.GetIdFromAccessToken(accessToken);

            User? user = await _repository.ReadAsync<User>(id);

            if (user is null)
                throw new AuthenticationException("There is no such user.");

            if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new AuthenticationException("Login expired");

            var tokens = _tokenService.GenerateTokens(user);

            user.RefreshToken = tokens.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _repository.UpdateAsync(user);

            return tokens;
        }

        public async Task<(string AccessToken, string RefreshToken)> RegisterUserAsync(RegisterDto registerData)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = registerData.Name,
                Email = registerData.Email,
                Role = "user",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerData.Password)
            };

            (string accessToken, string refreshToken) = _tokenService.GenerateTokens(user);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _repository.CreateAsync(user);

            return (accessToken, refreshToken);
        }

        public async Task RevokeTokensByUserIdAsync(Guid id)
        {
            User? user = await _repository.ReadAsync<User>(id);
            user.RefreshToken = null;
            await _repository.UpdateAsync(user);
        }
    }
}
