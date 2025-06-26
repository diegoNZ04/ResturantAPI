namespace Restaurant.Application.DTOs.Responses.AuthResponses;

public class RegisterUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}