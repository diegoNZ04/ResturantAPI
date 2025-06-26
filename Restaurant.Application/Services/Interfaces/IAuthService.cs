using Restaurant.Application.DTOs.Requests.AuthRequests;
using Restaurant.Application.DTOs.Responses.AuthResponses;

namespace Restaurant.Application.Services.Interfaces;

public interface IAuthService
{
    Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request);
    Task<LoginUserResponse> LoginUserAsync(LoginUserRequest request);
}