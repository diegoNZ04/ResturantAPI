namespace Restaurant.Application.DTOs.Requests.TablesRequests;

public class CreateTableRequest
{
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public int ReserveId { get; set; }
}