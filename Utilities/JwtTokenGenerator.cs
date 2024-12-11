using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Data;

namespace RestaurantAPI.Utilities
{
    public class JwtTokenGenerator
    {
        private readonly ApplicationContext _context;
        private readonly PasswordHasher _passwordHasher;

        public JwtTokenGenerator(ApplicationContext context, PasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        private const string SecretKey = "r6fwIFjUkoDEHUgYSmLYhv3os055N3MEMbMPHUEjxKY=";
        private const int ExpirationMinutes = 60;

        public static string GenerateToken(int userId, string userEmail, string role = "User")
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define as claims do token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userEmail),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique ID do token
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
            };

            // Configura o token
            var token = new JwtSecurityToken(
                issuer: "RestaurantAPI", // Nome da aplicação ou domínio
                audience: "RestaurantAPIClients", // Público-alvo do token
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(ExpirationMinutes),
                signingCredentials: credentials
            );

            // Retorna o token serializado
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}