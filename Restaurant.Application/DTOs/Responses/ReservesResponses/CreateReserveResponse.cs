using Restaurant.Domain.Enums;

namespace Restaurant.Application.DTOs.Responses.ReservesResponses;

public class CreateReserveResponse
{
    public int Id { get; set; }
    public int TablesNumber { get; set; }
    public int PeopleNumber { get; set; }
    public DateTime ReserveDate { get; set; }
    public string Status { get; set; }
}
