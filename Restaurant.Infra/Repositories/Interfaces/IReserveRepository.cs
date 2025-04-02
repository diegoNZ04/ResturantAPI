namespace Infra.Repositories;

public interface IReserveRepository
{
    Task AddReserveAsync(Reserve reserve);
    Task GetReserveByIdAsync(int id);
    Task<IEnumerable<Reserve>> GetAllReservesAsync();
    Task UpdateReserveAsync(Reserve reserve);
    Task DeleteReserveAsync(int id);
}
