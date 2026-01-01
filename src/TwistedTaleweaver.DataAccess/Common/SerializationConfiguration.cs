using System.Text.Json;

namespace TwistedTaleweaver.DataAccess.Common;

internal static class SerializationConfiguration
{
    public static JsonSerializerOptions SerializationOptions => new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}