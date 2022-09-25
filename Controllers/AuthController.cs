﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Register(RegisterDto dto)
        {
            if (dto is null)
                return BadRequest("Invalid client request");

            string? errorMessage;
            if (!_validationService.ValidateUsername(dto.Name, out errorMessage)) return ValidationProblem(errorMessage);
            if (!_validationService.ValidateEmail(dto.Email, out errorMessage)) return ValidationProblem(errorMessage);
            if (!_validationService.ValidatePassword(dto.Password, out errorMessage)) return ValidationProblem(errorMessage);

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
            
            _userService.CreateUser(user);

            return Created("success", new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            if (dto is null || dto.Email.IsNullOrEmpty() || dto.Password.IsNullOrEmpty())
                return BadRequest("Invalid client request");

            var user = _userService.GetUserByEmail(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return BadRequest(new { message = "Invalid credentials" });

            var tokens = _tokenService.GenerateTokens(user);

            user.RefreshToken = tokens.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            _userService.UpdateUser(user);

            return Ok(new
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(TokenDto dto)
        {
            if (dto is null || dto.AccessToken.IsNullOrEmpty() || dto.RefreshToken.IsNullOrEmpty())
                return BadRequest("Invalid client request");

            string accessToken = dto.AccessToken;
            string refreshToken = dto.RefreshToken;

            var email = _tokenService.GetEmailFromAccessToken(accessToken);
            if (email == null) return BadRequest("Invalid client request");
            
            var user = _userService.GetUserByEmail(email);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var tokens = _tokenService.GenerateTokens(user);

            user.RefreshToken = tokens.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            _userService.UpdateUser(user);

            return Ok(new
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            });
        }

        [HttpPost("revoke"), Authorize]
        public IActionResult Revoke()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email is null) return BadRequest();

            var user = _userService.GetUserByEmail(email);
            if (user == null) return BadRequest();

            user.RefreshToken = null;
            _userService.UpdateUser(user);

            return NoContent();
        }

        [HttpGet("user"), Authorize]
        public IActionResult GetUser()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email is null) return BadRequest();

            var user = _userService.GetUserByEmail(email);
            if (user == null) return BadRequest();

            return Ok(user);
        }
    }
}
