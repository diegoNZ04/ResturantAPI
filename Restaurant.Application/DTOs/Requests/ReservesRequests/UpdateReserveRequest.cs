using Restaurant.Domain.Enums;

namespace Restaurant.Application.DTOs.Requests.ReservesRequests;

public class UpdateReserveRequest
{
    public int TablesNumber { get; set; }
    public int PeopleNumber { get; set; }
    public DateTime ReserveDate { get; set; }
    public ReserveStatus Status { get; set; }
}
