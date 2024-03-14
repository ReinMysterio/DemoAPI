using DemoAPI.Models;
using DemoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController] 
    public class AuthenticateController : ControllerBase
    {
        private readonly AuthenticateService _authenticateService;
        public AuthenticateController(AuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult IsAuthenticateUser([FromBody] LoginRequest loginRequest)
        {
            var isAuthenticate = _authenticateService.IsAuthenticateUser(loginRequest, out TokenResponse tokenResponse);
            if (isAuthenticate)
            {
                return Ok(tokenResponse);
            }
            return Unauthorized(new { Message = "Invalid username or password" });
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            try
            {
                var tokenResponse = _authenticateService.GenerateRefreshToken(refreshTokenRequest);                

                return Ok(tokenResponse);
            }
            catch (SecurityTokenException)
            {
                return Unauthorized(new { Message = "Invalid refresh token" });
            }
        }

        [HttpGet("TestAuthorize")]
        public IActionResult TestAuthorize()
        {
            return Ok("TestAuthorize");
        }
    }
}
