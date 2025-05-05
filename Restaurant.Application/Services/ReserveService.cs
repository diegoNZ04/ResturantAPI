using AutoMapper;
using Restaurant.Application.DTOs.Requests.ReservesRequests;
using Restaurant.Application.DTOs.Responses.ReservesResponses;
using Restaurant.Application.Services.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Infra.Repositories.Interfaces;

namespace Restaurant.Application.Services;

public class ReserveService : IReserveService
{
    private readonly IReserveRepository _reserveRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;
    public ReserveService(IReserveRepository reserveRepository, ITableRepository tableRepository, IMapper mapper)
    {
        _reserveRepository = reserveRepository;
        _tableRepository = tableRepository;
        _mapper = mapper;
    }

    public async Task<CreateReserveResponse> CreateReserveAsync(CreateReserveRequest request)
    {
        var table = await _tableRepository.GetTableByNumberAsync(request.TableNumber);

        if (table == null)
            throw new Exception("Mesa não encontrada.");

        if (table.Status != TableStatus.available)
            throw new Exception("Mesa não está disponível para reserva.");

        if (request.PeopleNumber > table.Capacity)
            throw new Exception("Número de pessoas excede a capacidade da mesa.");

        var reserve = _mapper.Map<Reserve>(request);

        await _reserveRepository.AddReserveAsync(reserve);

        table.Status = TableStatus.reserved;
        await _tableRepository.UpdateTableAsync(table);

        var response = _mapper.Map<CreateReserveResponse>(reserve);
        response.Status = ReserveStatus.active.ToString();

        return response;
    }

    public async Task DeleteReserveAsync(int reserveId)
    {
        var reserve = await _reserveRepository.GetReserveByIdAsync(reserveId);

        if (reserve != null)
            await _reserveRepository.DeleteReserveAsync(reserve.Id);

        throw new Exception("Reserve not found");
    }

    public async Task<IEnumerable<GetAllReservesResponse>> GetAllReservesAsync(int page, int pageSize)
    {
        var reserves = await _reserveRepository.GetAllReservesAsync(page, pageSize);

        var response = _mapper.Map<IEnumerable<GetAllReservesResponse>>(reserves);

        return response;
    }

    public async Task<GetReserveByIdResponse> GetReserveByIdAsync(int reserveId)
    {
        var reserve = await _reserveRepository.GetReserveByIdAsync(reserveId);

        if (reserve == null)
            throw new Exception("Reserve not found");

        var response = _mapper.Map<GetReserveByIdResponse>(reserve);

        return response;
    }

    public async Task<UpdateReserveResponse> UpdateReserveAsync(int reserveId, UpdateReserveRequest request)
    {
        var reserve = await _reserveRepository.GetReserveByIdAsync(reserveId);

        if (reserve == null)
            throw new Exception("Reserve not found");

        var updatedReserve = _mapper.Map(request, reserve);
        await _reserveRepository.UpdateReserveAsync(updatedReserve);

        var response = _mapper.Map<UpdateReserveResponse>(reserve);

        return response;
    }

    public async Task<UpdateReserveStatusResponse> UpdateReserveStatusAsync(int reserveId, UpdateReserveStatusRequest request)
    {
        var reserve = await _reserveRepository.GetReserveByIdAsync(reserveId);

        if (reserve == null)
            throw new Exception("Reserve not found");

        var updatedStatus = _mapper.Map(request, reserve);
        await _reserveRepository.UpdateReserveAsync(updatedStatus);

        var response = _mapper.Map<UpdateReserveStatusResponse>(reserve);

        return response;
    }
}