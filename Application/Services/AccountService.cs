using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Entities;
using RestaurantAPI.Services.Interfaces;
using RestaurantAPI.Utilities.Interfaces;

namespace RestaurantAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AccountService(ApplicationContext context, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
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
            var user = _context.Users.FirstOrDefault(u => u.Id == loginDTO.Id);
            if (user == null || !_passwordHasher.VerifyHashedPassword(user.PasswordHash, loginDTO.Password))
                throw new UnauthorizedAccessException("E-mail ou senha inválidos.");

            return _jwtTokenGenerator.GenerateToken(user);
        }

    }
}