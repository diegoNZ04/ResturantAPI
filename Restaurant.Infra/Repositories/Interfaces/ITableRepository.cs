namespace Infra.Repositories.Interfaces;

public interface ITableRepository
{
    Task AddReserveAsync(Table table);
    Task GetTableByIdAsync(int id);
    Task<IEnumerable<Table>> GetAllTablesAsync();
    Task UpdateTableAsync(Table table);
    Task DeleteTableAsync(int id);
}