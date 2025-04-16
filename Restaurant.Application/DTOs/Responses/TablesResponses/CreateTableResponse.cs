using Restaurant.Domain.Enums;

namespace Restaurant.Application.DTOs.Responses.TablesResponses;

public class CreateTableResponse
{

    public int Id { get; set; }
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public string Status { get; set; }
}