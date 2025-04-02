using RestaurantAPI.Entities;

namespace RestaurantAPI.Utilities.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}