using DataAccess.Context;
using DataAccess.Entities;
using DemoAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JwtConstants = DemoAPI.Constants.JwtConstants;

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

        public bool IsAuthenticateUser(LoginRequest loginRequest, out TokenResponse tokenResponse)
        {
            tokenResponse = null;
            var user = _demoAPIContext.Users.FirstOrDefault(u => u.UserName == loginRequest.UserName);
            if (user == null)
            {
                return false;
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
            if (!isPasswordValid)
            {
                return false;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, $"{(int)user.RoleType}"),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Email", user.Email)
            };

            tokenResponse = UpdateToken(user, claims);

            return true;
        }

        private TokenResponse UpdateToken(User user, IEnumerable<Claim> claims)
        {
            TokenResponse tokenResponse = new()
            {
                Token = GenerateToken(claims, minutes: 60),
                RefreshToken = GenerateRandomString(),
            };

            user.RefreshToken = tokenResponse.RefreshToken;
            _demoAPIContext.SaveChanges();
            return tokenResponse;
        }

        private string GenerateToken(IEnumerable<Claim> claims, int minutes = 120)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[JwtConstants.Secret]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration[JwtConstants.Issuer],
                             _configuration[JwtConstants.Audience],
                             claims,
                             expires: DateTime.Now.AddMinutes(minutes),
                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRandomString()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public TokenResponse GenerateRefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            var principal = GetPrincipalFromExpiredToken(refreshTokenRequest.Token);
            var userName = principal.Identity.Name;
            var user = _demoAPIContext.Users.SingleOrDefault(u => u.UserName == userName);

            if (user.RefreshToken != refreshTokenRequest.RefreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            return UpdateToken(user, principal.Claims);                       
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false,
                ValidIssuer = _configuration[JwtConstants.Issuer],
                ValidAudience = _configuration[JwtConstants.Audience],
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[JwtConstants.Secret]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;            
        }
    }
}
