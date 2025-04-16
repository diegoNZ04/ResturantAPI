using Restaurant.Domain.Enums;

namespace Restaurant.Application.DTOs.Requests.ReservesRequests;

public class CreateReserveRequest
{
    public int TableNumber { get; set; }
    public int PeopleNumber { get; set; }
    public DateTime ReserveDate { get; set; }
    public int UserId { get; set; }
}