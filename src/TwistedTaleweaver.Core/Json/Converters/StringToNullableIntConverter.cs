using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwistedTaleweaver.Core.Json.Converters;

/// <summary>
/// Converter for nullable integers that can handle both string and numeric JSON representations.
/// </summary>
public class StringToNullableIntConverter : JsonConverter<int?>
{
    public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Handle nulls
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString();
            if (int.TryParse(str, out var value))
                return value;
            if (string.IsNullOrWhiteSpace(str))
                return null;
            throw new JsonException($"Cannot convert '{str}' to int.");
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetInt32();
        }

        throw new JsonException($"Unexpected token {reader.TokenType} when parsing int.");
    }

    public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString());
        else
            writer.WriteNullValue();
    }
}