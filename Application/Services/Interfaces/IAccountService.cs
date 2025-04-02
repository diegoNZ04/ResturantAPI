using RestaurantAPI.DTOs;

namespace RestaurantAPI.Services.Interfaces
{
    public interface IAccountService
    {
        string Register(RegisterDTO registerDTO);
        string Login(LoginDTO loginDTO);
    }
}