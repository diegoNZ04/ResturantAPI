namespace Infra.Repositories.Interfaces;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task GetUserByIdAsync(int id);
    Task GetUserByEmailAsync(string email);
    Task UpdateUserAsync(User user);
}