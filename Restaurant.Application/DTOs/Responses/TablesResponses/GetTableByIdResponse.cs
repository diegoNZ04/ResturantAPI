namespace Restaurant.Application.DTOs.Responses.TablesResponses;

public class GetTableByIdResponse
{
    public int Capacity { get; set; }
    public int TableNumber { get; set; }
    public string Status { get; set; }
}