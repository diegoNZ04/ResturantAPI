using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Entities;
using RestaurantAPI.Utilities.Interfaces;

namespace RestaurantAPI.Utilities
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private const string SecretKey = "r6fwIFjUkoDEHUgYSmLYhv3os055N3MEMbMPHUEjxKY=";
        private const int ExpirationMinutes = 60;

        public string GenerateToken(User user)
        {


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "RestaurantAPI",
                audience: "RestaurantAPIClients",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(ExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}