using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartShopping.Data;
using SmartShopping.Dtos;
using SmartShopping.Services;
using System.Security.Claims;

namespace SmartShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public TokenController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(TokenDto tokens)
        {
            if (tokens is null)
                return BadRequest("Invalid client request");

            string accessToken = tokens.AccessToken;
            string refreshToken = tokens.RefreshToken;

            if (accessToken is null || refreshToken is null)
                return BadRequest("Invalid client request");

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var email = principal.FindFirst(ClaimTypes.Email).Value; //this is mapped to the Name claim by default

            var user = _userRepository.GetUserByEmail(email);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            _userRepository.SaveChanges();

            return Ok(new {
                token = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost("revoke"), Authorize]
        public IActionResult Revoke()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;

            var user = _userRepository.GetUserByEmail(email);
            if (user == null) return BadRequest();

            user.RefreshToken = null;

            _userRepository.SaveChanges();

            return NoContent();
        }
    }
}