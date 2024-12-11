using RestaurantAPI.DTOs;

namespace RestaurantAPI.Interfaces
{
    public interface ITableService
    {
        List<TableDTO> GetAllTables();
        string CreateTable(TableDTO tableDTO);
        void UpdateTable(int tableId, TableDTO tableDTO);
        void DeleteTableById(int tableId);
    }
}