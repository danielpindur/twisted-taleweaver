using TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Chat;

namespace TwistedTaleweaver.Twitch.ChatMessages.Extensions;

internal static class ChatMessagePayloadExtensions
{
    public static bool IsExpeditionStartCommand(this ChatMessagePayload payload)
    {
        return payload.Message.StartsWith("!expedition");
    }
}