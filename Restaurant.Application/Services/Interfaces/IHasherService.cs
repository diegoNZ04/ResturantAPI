namespace Restaurant.Application.Services.Interfaces;

public interface IHasherService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}