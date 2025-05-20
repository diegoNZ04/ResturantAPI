using AutoMapper;
using FluentValidation;
using Restaurant.Application.DTOs.Requests.ReservesRequests;
using Restaurant.Application.DTOs.Responses.ReservesResponses;
using Restaurant.Application.Exceptions;
using Restaurant.Application.Services.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Infra.Repositories.Interfaces;

namespace Restaurant.Application.Services;

public class ReserveService : IReserveService
{
    private readonly IReserveRepository _reserveRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateReserveRequest> _createReserveValidator;
    private readonly IValidator<UpdateReserveRequest> _updateReserveValidator;
    public ReserveService(
        IReserveRepository reserveRepository,
        ITableRepository tableRepository,
        IUserRepository userRepository,
        IMapper mapper,
        IValidator<CreateReserveRequest> createReserveValidator,
        IValidator<UpdateReserveRequest> updateReserveValidator)
    {
        _reserveRepository = reserveRepository;
        _tableRepository = tableRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _createReserveValidator = createReserveValidator;
        _updateReserveValidator = updateReserveValidator;
    }

    public async Task<CreateReserveResponse> CreateReserveAsync(CreateReserveRequest request)
    {
        var validationResult = await _createReserveValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var table = await _tableRepository.GetTableByNumberAsync(request.TableNumber)
            ?? throw new EntityNotFoundException("Table", request.TableNumber);

        var user = await _userRepository.GetUserByIdAsync(request.UserId)
            ?? throw new EntityNotFoundException("User", request.UserId);

        var overlappingReservation = await _reserveRepository.HasActiveReservationAsync(request.TableNumber, request.ReserveDate);
        if (overlappingReservation)
            throw new BusinessRuleException("This table is already reserved for the time indicated.");

        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        if (request.ReserveDate <= now)
            throw new BusinessRuleException("The reservation date must be in the future.");

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
        var reserve = await _reserveRepository.GetReserveByIdAsync(reserveId)
            ?? throw new EntityNotFoundException("Table", reserveId);

        var table = await _tableRepository.GetTableByNumberAsync(reserve.TableNumber);
        if (table != null && table.Status == TableStatus.reserved)
        {
            table.Status = TableStatus.available;
            await _tableRepository.UpdateTableAsync(table);
        }

        await _reserveRepository.DeleteReserveAsync(reserveId);
    }

    public async Task<IEnumerable<GetAllReservesResponse>> GetAllReservesAsync(int page, int pageSize)
    {
        var reserves = await _reserveRepository.GetAllReservesAsync(page, pageSize);

        var response = _mapper.Map<IEnumerable<GetAllReservesResponse>>(reserves);

        return response;
    }

    public async Task<GetReserveByIdResponse> GetReserveByIdAsync(int reserveId)
    {
        var reserve = await _reserveRepository.GetReserveByIdAsync(reserveId)
            ?? throw new EntityNotFoundException("Reserve", reserveId);

        var response = _mapper.Map<GetReserveByIdResponse>(reserve);

        return response;
    }

    public async Task<UpdateReserveResponse> UpdateReserveAsync(int reserveId, UpdateReserveRequest request)
    {
        var reserve = await _reserveRepository.GetReserveByIdAsync(reserveId)
            ?? throw new EntityNotFoundException("Reserve", reserveId);

        var validationResult = await _updateReserveValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var updatedReserve = _mapper.Map(request, reserve);

        await _reserveRepository.UpdateReserveAsync(updatedReserve);

        var response = _mapper.Map<UpdateReserveResponse>(reserve);

        return response;
    }

    public async Task<UpdateReserveStatusResponse> UpdateReserveStatusAsync(int reserveId, UpdateReserveStatusRequest request)
    {
        var reserve = await _reserveRepository.GetReserveByIdAsync(reserveId)
            ?? throw new EntityNotFoundException("Reserve", reserveId);

        var updatedStatus = _mapper.Map(request, reserve);

        await _reserveRepository.UpdateReserveAsync(updatedStatus);

        var response = _mapper.Map<UpdateReserveStatusResponse>(reserve);

        return response;
    }
}