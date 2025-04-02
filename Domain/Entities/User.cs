using RestaurantAPI.Domain.Enums;

namespace RestaurantAPI.Domain.Entities;

public class User
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public Role Role { get; set; } = default!;
}