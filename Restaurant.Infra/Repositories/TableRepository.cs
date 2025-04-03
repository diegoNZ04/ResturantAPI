using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Infra.Data;
using Restaurant.Infra.Repositories.Interfaces;

namespace Restaurant.Infra.Repositories;

public class TableRepository : ITableRepository
{
    private readonly ApplicationContext _context;
    public TableRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task AddTableAsync(Table table)
    {
        _context.Tables.Add(table);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTableAsync(int id)
    {
        var table = await _context.Tables.FindAsync(id);

        _context.Tables.Remove(table);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Table>> GetAllTablesAsync(int page, int pageSize)
    {
        var tables = await _context.Tables.AsNoTracking()
             .Skip((page - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();

        return tables;
    }

    public async Task<Table> GetTableByIdAsync(int id)
    {
        return await _context.Tables.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task UpdateTableAsync(Table table)
    {
        _context.Tables.Update(table);
        await _context.SaveChangesAsync();
    }
}