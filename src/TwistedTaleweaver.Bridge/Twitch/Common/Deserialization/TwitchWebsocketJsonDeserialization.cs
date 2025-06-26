using System.Text.Json;
using TwistedTaleweaver.Core.Json.Converters;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Deserialization;

internal static class TwitchWebsocketJsonDeserialization
{
    public static JsonSerializerOptions Options => new JsonSerializerOptions()
    {
        Converters = { new EnumMemberJsonConverterFactory(), new StringToNullableIntConverter() }
    };
}