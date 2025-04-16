using Restaurant.Application.DTOs.Requests.ReservesRequests;
using Restaurant.Application.DTOs.Responses.ReservesResponses;

namespace Restaurant.Application.Services.Interfaces;

public interface IReserveService
{
    Task<CreateReserveResponse> CreateReserveAsync(CreateReserveRequest request);
    Task DeleteReserveAsync(int reserveId);
    Task<IEnumerable<GetAllReservesResponse>> GetAllReservesAsync(int page, int pageSize);
    Task<GetReserveByIdResponse> GetReserveByIdAsync(int reserveId);
    Task<UpdateReserveResponse> UpdateReserveAsync(int reserveId, UpdateReserveRequest request);
}
