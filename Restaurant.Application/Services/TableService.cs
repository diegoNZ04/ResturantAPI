using AutoMapper;
using Restaurant.Application.DTOs.Requests.TablesRequests;
using Restaurant.Application.DTOs.Responses.TablesResponses;
using Restaurant.Application.Services.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Infra.Repositories.Interfaces;

namespace Restaurant.Application.Services;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;
    public TableService(ITableRepository tableRepository, IMapper mapper)
    {
        _tableRepository = tableRepository;
        _mapper = mapper;
    }
    public async Task<CreateTableResponse> CreateTableAsync(CreateTableRequest request)
    {
        var table = _mapper.Map<Table>(request);

        await _tableRepository.AddTableAsync(table);

        var response = _mapper.Map<CreateTableResponse>(table);
        response.Status = TableStatus.available.ToString();

        return response;
    }

    public async Task DeleteTableAsync(int tableId)
    {
        var table = await _tableRepository.GetTableByIdAsync(tableId);

        if (table != null)
            await _tableRepository.DeleteTableAsync(table.Id);

        throw new Exception("Table not found");
    }

    public async Task<IEnumerable<GetAllTablesResponse>> GetAllTablesAsync(int page, int pageSize)
    {
        var tables = await _tableRepository.GetAllTablesAsync(page, pageSize);

        var response = _mapper.Map<IEnumerable<GetAllTablesResponse>>(tables);

        return response;
    }

    public async Task<GetTableByIdResponse> GetTableByIdAsync(int tableId)
    {
        var table = await _tableRepository.GetTableByIdAsync(tableId);

        if (table == null)
            throw new Exception("Table not found");

        var response = _mapper.Map<GetTableByIdResponse>(table);
        return response;
    }

    public async Task<UpdateTableResponse> UpdateTableAsync(int tableId, UpdateTableRequest request)
    {
        var table = await _tableRepository.GetTableByIdAsync(tableId);

        if (table == null)
            throw new Exception("Table not found");

        var updatedTable = _mapper.Map(request, table);

        await _tableRepository.UpdateTableAsync(updatedTable);

        var response = _mapper.Map<UpdateTableResponse>(table);
        return response;
    }
}