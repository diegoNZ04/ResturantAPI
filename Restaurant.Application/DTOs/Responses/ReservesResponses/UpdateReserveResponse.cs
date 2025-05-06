using System.Text.Json.Serialization;
using Restaurant.Application.Converters;

namespace Restaurant.Application.DTOs.Responses.ReservesResponses;

public class UpdateReserveResponse
{
    public int TableNumber { get; set; }
    public int PeopleNumber { get; set; }
    [JsonConverter(typeof(BrasiliaDateTimeConverter))]
    public DateTime ReserveDate { get; set; }
}
