using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.DTOs.Requests.AuthRequests;
using Restaurant.Application.Services.Interfaces;

namespace Restaurant.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        var response = await _authService.RegisterUserAsync(request);
        return CreatedAtAction(nameof(RegisterUser), new { id = response.Id }, response);
    }
    [HttpPost("login-user")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
    {
        var response = await _authService.LoginUserAsync(request);
        return Ok(response);
    }
}