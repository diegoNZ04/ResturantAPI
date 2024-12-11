using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Entities;
using RestaurantAPI.Interfaces;
using RestaurantAPI.Utilities;

namespace RestaurantAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationContext _context;
        private readonly PasswordHasher _passwordHasher;

        public AccountService(ApplicationContext context, PasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public string Register(RegisterDTO registerDTO)
        {
            if (_context.Users.Any(u => u.Email == registerDTO.Email))
            {
                throw new ArgumentException("Email já registrado.");
            }

            var newUser = new User
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                PasswordHash = registerDTO.Password,
                Role = registerDTO.Role
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser.PasswordHash);

            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return "Usuário registrado com sucesso!";

        }
        public string Login(LoginDTO loginDTO)
        {
            // Busca o usuário pelo e-mail
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDTO.Email);
            if (user == null || !_passwordHasher.VerifyHashedPassword(user.PasswordHash, loginDTO.Password))
                throw new UnauthorizedAccessException("E-mail ou senha inválidos.");

            // Gera o token JWT
            return JwtTokenGenerator.GenerateToken(user.Id, user.Email);
        }

    }
}