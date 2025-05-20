using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Infra.Data;
using Restaurant.Infra.Repositories.Interfaces;

namespace Restaurant.Infra.Repositories;

public class ReserveRepository : IReserveRepository
{
    private readonly ApplicationContext _context;
    public ReserveRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task AddReserveAsync(Reserve reserve)
    {
        _context.Reserves.Add(reserve);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReserveAsync(int id)
    {
        var reserve = await _context.Reserves.FindAsync(id);

        _context.Reserves.Remove(reserve);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Reserve>> GetAllReservesAsync(int page, int pageSize)
    {
        var reserves = await _context.Reserves.AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return reserves;
    }

    public async Task<Reserve> GetReserveByIdAsync(int id)
    {
        return await _context.Reserves
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> HasActiveReservationAsync(int tableNumber, DateTime reserveDate)
    {
        return await _context.Reserves.AnyAsync(r =>
            r.TableNumber == tableNumber &&
            r.ReserveDate.Date == reserveDate.Date &&
            r.Status == ReserveStatus.active);
    }

    public async Task UpdateReserveAsync(Reserve reserve)
    {
        _context.Reserves.Update(reserve);
        await _context.SaveChangesAsync();
    }
}