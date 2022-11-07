using Microsoft.AspNetCore.Mvc;
using SmartShopping.Dtos;
using SmartShopping.Services;
using SmartShopping.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace SmartShopping.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IValidationService _validationService;

        public AuthController(ILogger<AuthController> logger, IUserService userService, ITokenService tokenService, IValidationService validationService)
        {
            _logger = logger;
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService)); ;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (dto is null)
                return BadRequest("Invalid client request");

            var (ok, invalidField, errorMessage) = await _validationService.ValidateRegistrationAsync(dto);
            if (!ok) 
                return ValidationProblem(detail:errorMessage, type : invalidField);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Role = "user",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            (string accessToken, string refreshToken) = _tokenService.GenerateTokens(user);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            
            await _userService.CreateUserAsync(user);

            return Created("success", new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (dto is null || dto.Email.IsNullOrEmpty() || dto.Password.IsNullOrEmpty())
                return BadRequest("Invalid client request");

            var user = await _userService.GetUserByEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokens = _tokenService.GenerateTokens(user);

            user.RefreshToken = tokens.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userService.UpdateUserAsync(user);

            return Ok(new
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenDto dto)
        {
            if (dto is null || dto.AccessToken.IsNullOrEmpty() || dto.RefreshToken.IsNullOrEmpty())
                return BadRequest("Invalid client request");

            string accessToken = dto.AccessToken;
            string refreshToken = dto.RefreshToken;

            var email = _tokenService.GetEmailFromAccessToken(accessToken);
            if (email == null) return BadRequest("Invalid client request");
            
            var user = await _userService.GetUserByEmailAsync(email);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var tokens = _tokenService.GenerateTokens(user);

            user.RefreshToken = tokens.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userService.UpdateUserAsync(user);

            return Ok(new
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            });
        }

        [HttpPost("revoke"), Authorize]
        public async Task<IActionResult> Revoke()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email is null) return BadRequest();

            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null) return BadRequest();

            user.RefreshToken = null;
            await _userService.UpdateUserAsync(user);

            return NoContent();
        }

        [HttpGet("user"), Authorize]
        public async Task<IActionResult> GetUser()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email is null) return BadRequest();

            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null) return BadRequest();

            return Ok(user);
        }

        [HttpDelete("deleteuser"), Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email is null) return BadRequest();

            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null) return BadRequest();

            await _userService.DeleteUserAsync(user);

            return Ok("success");
        }
    }
}
