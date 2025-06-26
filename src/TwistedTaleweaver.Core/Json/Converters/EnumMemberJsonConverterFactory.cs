using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwistedTaleweaver.Core.Json.Converters;

/// <summary>
/// Converter factory for enums that uses the EnumMember attribute for serialization.
/// It supports both nullable and non-nullable enums.
/// </summary>
public class EnumMemberJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        var t = Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert;
        return t.IsEnum;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var isNullable = Nullable.GetUnderlyingType(typeToConvert) != null;
        var enumType = Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert;

        var converterType = isNullable
            ? typeof(NullableEnumMemberJsonConverter<>).MakeGenericType(enumType)
            : typeof(EnumMemberJsonConverter<>).MakeGenericType(enumType);

        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}