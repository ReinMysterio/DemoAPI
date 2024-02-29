using DemoAPI.Models;
using DemoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class AuthenticateController : ControllerBase
    {
        private readonly AuthenticateService _authenticateService;
        public AuthenticateController(AuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost("Login")]
        public IActionResult IsAuthenticateUser([FromBody] LoginRequest loginRequest)
        {
            var isAuthenticate = _authenticateService.IsAuthenticateUser(loginRequest, out string token);
            if (isAuthenticate)
            {
                return Ok(new { Token = token });
            }
            return Unauthorized(new { Message = "Invalid username or password" });
        }
    }
}
