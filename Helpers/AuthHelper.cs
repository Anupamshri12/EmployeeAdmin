using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeAdmin.Models;

namespace EmployeeAdmin.Helpers
{
    public class AuthHelper
    {
        private readonly IConfiguration _configuration;
        public AuthHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJWTToken(SystemUser User)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier ,User.Id.ToString()),
                new Claim(ClaimTypes.Name ,User.Name)
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"])
                        ),
                     SecurityAlgorithms.HmacSha256Signature

                    )
                  
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
