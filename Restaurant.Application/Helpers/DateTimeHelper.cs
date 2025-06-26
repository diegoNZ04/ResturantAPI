using System.Runtime.InteropServices;

namespace Restaurant.Application.Helpers;

public static class DateTimeHelper
{
    private static readonly TimeZoneInfo BrasiliaTZ =
        TimeZoneInfo.FindSystemTimeZoneById(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        ? "E. South America Standard Time"
        : "America/Sao_Paulo");

    public static DateTime ToBrasiliaTime(DateTime utcDateTime)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, BrasiliaTZ);
    }
}