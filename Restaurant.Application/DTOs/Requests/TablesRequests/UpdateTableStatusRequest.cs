using Restaurant.Domain.Enums;

namespace Restaurant.Application.DTOs.Requests.TablesRequests;

public class UpdateTableStatusRequest
{
    public TableStatus Status { get; set; }
}