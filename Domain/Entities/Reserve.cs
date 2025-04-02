using RestaurantAPI.Domain.Enums;

namespace RestaurantAPI.Domain.Entities;

public class Reserve
{
    public int Id { get; set; } = default!;
    public int UserId { get; set; } = default!;
    public int TableId { get; set; } = default!;
    public int PeopleNumber { get; set; } = default!;
    public DateTime ReserveDate { get; set; } = default!;
    public ReserveStatus Status { get; set; } = default!;
}