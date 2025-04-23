using Restaurant.Domain.Enums;

namespace Restaurant.Application.DTOs.Requests.TablesRequests;

public class UpdateTableRequest
{
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
}
