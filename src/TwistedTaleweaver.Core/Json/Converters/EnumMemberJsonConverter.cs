using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwistedTaleweaver.Core.Json.Converters;

/// <summary>
/// Json converter for enums that uses the EnumMember attribute for serialization.
/// </summary>
internal class EnumMemberJsonConverter<T> : JsonConverter<T> where T : struct, Enum
{
    private static readonly Type EnumType = typeof(T);

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("The JSON value could not be converted to non-nullable enum.");
        }

        var value = reader.GetString();

        foreach (var field in EnumType.GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var enumMemberAttr = field.GetCustomAttribute<EnumMemberAttribute>();
            if (enumMemberAttr is not null && enumMemberAttr.Value == value)
            {
                return (T)field.GetValue(null)!;
            }
        }

        if (Enum.TryParse(EnumType, value, ignoreCase: true, out var result))
        {
            return (T)result;
        }

        throw new JsonException($"Unknown value '{value}' for enum '{EnumType.Name}'");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var enumValueString = value.ToString();
        var field = EnumType.GetField(enumValueString!);
        var enumMemberAttr = field?.GetCustomAttribute<EnumMemberAttribute>();

        if (enumMemberAttr is not null)
        {
            writer.WriteStringValue(enumMemberAttr.Value);
        }
        else
        {
            writer.WriteStringValue(enumValueString);
        }
    }
}