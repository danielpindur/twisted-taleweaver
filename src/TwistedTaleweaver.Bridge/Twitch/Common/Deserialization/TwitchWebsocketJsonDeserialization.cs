using System.Text.Json;
using TwistedTaleweaver.Core.Json.Converters;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Deserialization;

internal static class TwitchWebsocketJsonDeserialization
{
    private static readonly Lazy<JsonSerializerOptions> _options = new(() => new JsonSerializerOptions()
    {
        Converters = { new EnumMemberJsonConverterFactory(), new StringToNullableIntConverter() }
    });
    
    public static JsonSerializerOptions Options => _options.Value;
}