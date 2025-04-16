using Restaurant.Application.DTOs.Requests.TablesRequests;
using Restaurant.Application.DTOs.Responses.TablesResponses;

namespace Restaurant.Application.Services.Interfaces;

public interface ITableService
{
    Task<CreateTableResponse> CreateTableAsync(CreateTableRequest request);
    Task DeleteTableAsync(int tableId);
    Task<IEnumerable<GetAllTablesResponse>> GetAllTablesAsync(int page, int pageSize);
    Task<GetTableByIdResponse> GetTableByIdAsync(int tableId);
    Task<UpdateTableResponse> UpdateTableAsync(int tableId, UpdateTableRequest request);
}
