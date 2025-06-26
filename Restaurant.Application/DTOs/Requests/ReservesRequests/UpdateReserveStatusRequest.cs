using Restaurant.Domain.Enums;

namespace Restaurant.Application.DTOs.Requests.ReservesRequests;

public class UpdateReserveStatusRequest
{
    public ReserveStatus Status { get; set; }
}