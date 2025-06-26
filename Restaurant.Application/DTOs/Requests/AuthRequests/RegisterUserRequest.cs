using Restaurant.Domain.Enums;

namespace Restaurant.Application.DTOs.Requests.AuthRequests;

public class RegisterUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; }
}