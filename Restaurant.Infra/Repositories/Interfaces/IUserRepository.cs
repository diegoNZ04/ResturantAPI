using Restaurant.Domain.Entities;

namespace Restaurant.Infra.Repositories.Interfaces;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByEmailAsync(string email);
    Task UpdateUserAsync(User user);
}