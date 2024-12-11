using RestaurantAPI.DTOs;

namespace RestaurantAPI.Interfaces
{
    public interface IAccountService
    {
        string Register(RegisterDTO registerDTO);
        string Login(LoginDTO loginDTO);
    }
}