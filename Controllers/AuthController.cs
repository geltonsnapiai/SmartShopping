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
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var (ok, invalidField, errorMessage) = await _validationService.ValidateRegistrationAsync(dto);
            if (!ok)
                return ValidationProblem(detail: errorMessage, type: invalidField);

            try
            {
                (string accessToken, string refreshToken) = await _userService.RegisterUserAsync(dto);
                return Created("api/auth/user", new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }
            catch (AuthenticationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                return BadRequest("Invalid client request");

            try
            {
                (string accessToken, string refreshToken) = await _userService.LoginUserAsync(dto);
                return Ok(new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            } catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto dto)
        {
            if (string.IsNullOrEmpty(dto.AccessToken) || string.IsNullOrEmpty(dto.RefreshToken))
                return BadRequest("Invalid client request");

            try
            {
                (string accessToken, string refreshToken) = await _userService.RefreshTokensAsync(dto);
                return Ok(new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("revoke"), Authorize]
        public async Task<IActionResult> Revoke()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id is null) 
                return BadRequest("Token does not contain id claim");
            await _userService.RevokeTokensByUserIdAsync(Guid.Parse(id));
            
            return NoContent();
        }

        [HttpGet("user"), Authorize]
        public async Task<IActionResult> GetUser()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id is null)
                return BadRequest("Token does not contain id claim");

            var user = await _userService.GetUserByIdAsync(Guid.Parse(id));

            return Ok(user);
        }

        [HttpDelete("deleteUser"), Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id is null)
                return BadRequest("Token does not contain id claim");

            await _userService.DeleteUserByIdAsync(Guid.Parse(id));

            return Ok("success");
        }
    }
}
