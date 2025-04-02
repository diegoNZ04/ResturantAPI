namespace RestaurantAPI.Utilities.Interfaces
{
    public interface IPasswordHasher
    {

        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}