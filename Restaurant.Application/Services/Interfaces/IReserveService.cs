using RestaurantAPI.DTOs;

namespace RestaurantAPI.Services.Interfaces
{
    public interface IReserveService
    {
        string CreateReserve(ReserveDTO reserveDTO);
        List<ReserveDetailsDTO> ListUserReserves(int userId);
        string CancelReserve(int reserveId);
    }
}