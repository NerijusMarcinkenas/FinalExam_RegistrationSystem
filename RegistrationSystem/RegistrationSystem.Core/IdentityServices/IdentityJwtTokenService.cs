using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RegistrationSystem.Core.Models;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Enums;

namespace RegistrationSystem.Core.IdentityServices
{
    internal sealed class IdentityJwtTokenService : IIdentityJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public IdentityJwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetJwtToken(User user)
        {
            var userRole = Enum.GetName(typeof(Roles), user.Role);
            if (userRole == null)
            {
                userRole = String.Empty;
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Role", userRole)
            };

            var secret = _configuration.GetSection("Jwt:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
