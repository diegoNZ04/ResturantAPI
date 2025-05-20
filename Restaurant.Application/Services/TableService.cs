using AutoMapper;
using FluentValidation;
using Restaurant.Application.DTOs.Requests.TablesRequests;
using Restaurant.Application.DTOs.Responses.TablesResponses;
using Restaurant.Application.Exceptions;
using Restaurant.Application.Services.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Infra.Repositories.Interfaces;

namespace Restaurant.Application.Services;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateTableRequest> _createTableValidator;
    private readonly IValidator<UpdateTableRequest> _updateTableValidator;
    public TableService(
        ITableRepository tableRepository,
        IMapper mapper,
        IValidator<CreateTableRequest> createTableValidator,
        IValidator<UpdateTableRequest> updateTableValidator)
    {
        _tableRepository = tableRepository;
        _mapper = mapper;
        _createTableValidator = createTableValidator;
        _updateTableValidator = updateTableValidator;
    }
    public async Task<CreateTableResponse> CreateTableAsync(CreateTableRequest request)
    {
        var validationResult = await _createTableValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var table = _mapper.Map<Table>(request);

        await _tableRepository.AddTableAsync(table);

        var response = _mapper.Map<CreateTableResponse>(table);
        response.Status = TableStatus.available.ToString();

        return response;
    }

    public async Task DeleteTableAsync(int tableId)
    {
        var table = await _tableRepository.GetTableByIdAsync(tableId)
            ?? throw new EntityNotFoundException("Table", tableId);

        await _tableRepository.DeleteTableAsync(tableId);
    }

    public async Task<IEnumerable<GetAllTablesResponse>> GetAllTablesAsync(int page, int pageSize)
    {
        var tables = await _tableRepository.GetAllTablesAsync(page, pageSize);

        var response = _mapper.Map<IEnumerable<GetAllTablesResponse>>(tables);

        return response;
    }

    public async Task<GetTableByIdResponse> GetTableByIdAsync(int tableId)
    {
        var table = await _tableRepository.GetTableByIdAsync(tableId)
            ?? throw new EntityNotFoundException("Table", tableId);

        var response = _mapper.Map<GetTableByIdResponse>(table);
        return response;
    }

    public async Task<UpdateTableResponse> UpdateTableAsync(int tableId, UpdateTableRequest request)
    {
        var table = await _tableRepository.GetTableByIdAsync(tableId)
            ?? throw new EntityNotFoundException("Table", tableId);

        var validationResult = await _updateTableValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var updatedTable = _mapper.Map(request, table);

        await _tableRepository.UpdateTableAsync(updatedTable);

        var response = _mapper.Map<UpdateTableResponse>(table);
        return response;
    }

    public async Task<UpdateTableStatusResponse> UpdateTableStatusAsync(int tableId, UpdateTableStatusRequest request)
    {
        var table = await _tableRepository.GetTableByIdAsync(tableId)
            ?? throw new EntityNotFoundException("Table", tableId);

        var updatedStatus = _mapper.Map(request, table);
        await _tableRepository.UpdateTableAsync(updatedStatus);

        var response = _mapper.Map<UpdateTableStatusResponse>(table);

        return response;
    }
}