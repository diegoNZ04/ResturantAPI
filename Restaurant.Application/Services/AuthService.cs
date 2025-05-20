using AutoMapper;
using Restaurant.Application.DTOs.Requests.AuthRequests;
using Restaurant.Application.DTOs.Responses.AuthResponses;
using Restaurant.Application.Services.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Infra.Repositories.Interfaces;

namespace Restaurant.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IHasherService _hasherService;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    public AuthService(
        IUserRepository userRepository,
        IHasherService hasherService,
        IJwtService jwtService,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _hasherService = hasherService;
        _jwtService = jwtService;
        _mapper = mapper;
    }
    public async Task<LoginUserResponse> LoginUserAsync(LoginUserRequest request)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);

        if (user == null || !_hasherService.VerifyPassword(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var token = _jwtService.GenerateToken(user);

        var response = _mapper.Map<LoginUserResponse>(user);
        response.Token = token;
        response.Role = user.Role.ToString();

        return response;
    }

    public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request)
    {
        var hashedPassword = _hasherService.HashPassword(request.Password);

        var user = _mapper.Map<User>(request);
        user.PasswordHash = hashedPassword;

        await _userRepository.AddUserAsync(user);

        var response = _mapper.Map<RegisterUserResponse>(user);

        return response;
    }
}