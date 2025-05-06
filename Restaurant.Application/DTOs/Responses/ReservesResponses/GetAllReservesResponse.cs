using System.Text.Json.Serialization;
using Restaurant.Application.Converters;
using Restaurant.Domain.Enums;

namespace Restaurant.Application.DTOs.Responses.ReservesResponses;

public class GetAllReservesResponse
{
    public int Id { get; set; }
    public int TableNumber { get; set; }
    public int PeopleNumber { get; set; }
    [JsonConverter(typeof(BrasiliaDateTimeConverter))]
    public DateTime ReserveDate { get; set; }
    public string Status { get; set; }
    public int UserId { get; set; }
}
