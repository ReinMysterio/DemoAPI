using DataAccess.Context;
using DemoAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoAPI.Services
{
    public class AuthenticateService
    {
        private IConfiguration _configuration;
        private readonly DemoAPIContext _demoAPIContext;

        public AuthenticateService(IConfiguration configuration, DemoAPIContext demoAPIContext)
        {
            _configuration = configuration;
            _demoAPIContext = demoAPIContext;
        }

        public bool IsAuthenticateUser(LoginRequest loginRequest, out string token)
        {
            token = string.Empty;
            var user = _demoAPIContext.Users.FirstOrDefault(u => u.UserName == loginRequest.UserName);
            if (user == null)
            {
                return false;
            }

            token = GenerateToken();
            return BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
        }

        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "Admin"),                
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], 
                             _configuration["Jwt:Audience"],
                             claims,
                             expires: DateTime.Now.AddMinutes(120),
                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
