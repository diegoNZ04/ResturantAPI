using Restaurant.Domain.Entities;

namespace Restaurant.Infra.Repositories.Interfaces;

public interface IReserveRepository
{
    Task AddReserveAsync(Reserve reserve);
    Task<Reserve> GetReserveByIdAsync(int id);
    Task<IEnumerable<Reserve>> GetAllReservesAsync(int page, int pageSize);
    Task UpdateReserveAsync(Reserve reserve);
    Task DeleteReserveAsync(int id);
    Task<bool> HasActiveReservationAsync(int tableNumber, DateTime reserveDate);
}
