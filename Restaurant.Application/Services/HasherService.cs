using Restaurant.Application.Services.Interfaces;

namespace Restaurant.Application.Services;

public class HasherService : IHasherService
{
    private const int WorkFactor = 12;
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: WorkFactor);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}