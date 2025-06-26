namespace Restaurant.Application.DTOs.Requests.ReservesRequests;

public class UpdateReserveRequest
{
    public int TableNumber { get; set; }
    public int PeopleNumber { get; set; }
    public DateTime ReserveDate { get; set; }
}
