using JwtApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtApi.Services
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly List<User> users= new List<User>()
        {
            new User()
            {
                Id = 1,
                Username = "admin1",
                Email = "admin1@admin1.com",
                Password = "admin1@admin1"
            },
            new User()
            {
                Id = 2,
                Username = "admin2",
                Email = "admin2@admin2.com",
                Password = "admin2@admin2"
            },
        };

        public User Authenticate(string email, string password)
        {
            return users.FirstOrDefault(u => u.Email.ToUpper().Equals(email.ToUpper()) && u.Password == password);
        }

        public string GenerateToken(string secret, List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
