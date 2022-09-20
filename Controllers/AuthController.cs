using Microsoft.AspNetCore.Mvc;
using SmartShopping.Data;
using SmartShopping.Dtos;
using SmartShopping.Services;
using SmartShopping.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SmartShopping.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Role = "user",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            user = _userRepository.Create(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            _userRepository.SaveChanges();

            return Created("success", new
            {
                tokens = new
                {
                    token = accessToken,
                    refreshToken = refreshToken
                },
                user = user
            });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            if (dto is null)
            {
                return BadRequest("Invalid client request");
            }

            var user = _userRepository.GetUserByEmail(dto.Email);

            if (user == null)
                return BadRequest(new { message = "Invalid Credentials" });

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return BadRequest(new { message = "Invalid Credentials" });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            _userRepository.SaveChanges();

            return Ok(new 
            {
                tokens = new 
                {
                    token = accessToken,
                    refreshToken = refreshToken
                },
                user = user
            });
        }

        [HttpGet("user"), Authorize]
        public IActionResult GetUser()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;

            var user = _userRepository.GetUserByEmail(email);
            if (user == null) return BadRequest();

            return Ok(user);
        }
    }
}
