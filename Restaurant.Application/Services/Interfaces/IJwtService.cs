using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}