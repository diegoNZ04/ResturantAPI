using Restaurant.Domain.Entities;

namespace Restaurant.Infra.Repositories.Interfaces;

public interface ITableRepository
{
    Task AddTableAsync(Table table);
    Task<Table> GetTableByIdAsync(int id);
    Task<IEnumerable<Table>> GetAllTablesAsync(int page, int pageSize);
    Task UpdateTableAsync(Table table);
    Task DeleteTableAsync(int id);
}