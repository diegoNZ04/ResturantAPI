using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Restaurant.Application.Converters;

public class BrasiliaDateTimeConverter : JsonConverter<DateTime>
{
    private static readonly TimeZoneInfo BrasiliaTZ =
        TimeZoneInfo.FindSystemTimeZoneById(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        ? "E. South America Standard Time"
        : "America/Sao_Paulo");
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = DateTime.Parse(reader.GetString());
        return TimeZoneInfo.ConvertTimeToUtc(value, BrasiliaTZ);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var localTime = TimeZoneInfo.ConvertTime(value, BrasiliaTZ);
        writer.WriteStringValue(localTime.ToString("dd/MM/yyyy HH:mm"));
    }
}