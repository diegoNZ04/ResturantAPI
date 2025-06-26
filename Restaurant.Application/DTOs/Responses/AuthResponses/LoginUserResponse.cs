namespace Restaurant.Application.DTOs.Responses.AuthResponses;

public class LoginUserResponse
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}