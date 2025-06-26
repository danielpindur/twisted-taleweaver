using System.Text.Json;
using TwistedTaleweaver.Bridge.Twitch.Common.Deserialization;
using TwistedTaleweaver.Bridge.Twitch.Common.Models;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Processing;

internal class TwitchNotification(TwitchEventSubNotification eventSubNotification)
{
    public string MessageId { get; } = eventSubNotification.Metadata.MessageId;

    public DateTime MessageTimestamp { get; } = eventSubNotification.Metadata.MessageTimestamp;

    public string Message { get; } = eventSubNotification.Payload.Event!.Value.ToString();

    public T Deserialize<T>() where T : ITwitchEvent
    {
        return eventSubNotification.Payload.Event!.Value.Deserialize<T>(TwitchWebsocketJsonDeserialization.Options) ??
               throw new InvalidOperationException($"Failed to deserialize event of type {typeof(T).Name}");
    }
}