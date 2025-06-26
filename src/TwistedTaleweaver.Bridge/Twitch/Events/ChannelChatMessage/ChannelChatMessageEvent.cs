using System.Text.Json.Serialization;
using TwistedTaleweaver.Bridge.Twitch.Common.Models;

namespace TwistedTaleweaver.Bridge.Twitch.Events.ChannelChatMessage;

/// <summary>
/// Represents a Twitch chat message event in a channel.
/// </summary>
internal sealed class ChannelChatMessageEvent : ITwitchEvent
{
    /// <summary>
    /// Unique identifier for the broadcaster whose channel the chat message was sent in.
    /// </summary>
    [JsonPropertyName("broadcaster_user_id")]
    public required string BroadcasterUserId { get; set; }

    /// <summary>
    /// Login name of the broadcaster whose channel the chat message was sent in.
    /// </summary>
    [JsonPropertyName("broadcaster_user_login")]
    public required string BroadcasterUserLogin { get; set; }

    /// <summary>
    /// Display name of the broadcaster whose channel the chat message was sent in.
    /// </summary>
    [JsonPropertyName("broadcaster_user_name")]
    public required string BroadcasterUserName { get; set; }

    /// <summary>
    /// Unique identifier for the user who sent the chat message.
    /// </summary>
    [JsonPropertyName("chatter_user_id")]
    public required string ChatterUserId { get; set; }

    /// <summary>
    /// Login name of the user who sent the chat message.
    /// </summary>
    [JsonPropertyName("chatter_user_login")]
    public required string ChatterUserLogin { get; set; }

    /// <summary>
    /// Display name of the user who sent the chat message.
    /// </summary>
    [JsonPropertyName("chatter_user_name")]
    public required string ChatterUserName { get; set; }

    /// <summary>
    /// Unique identifier for the chat message.
    /// </summary>
    [JsonPropertyName("message_id")]
    public required string ChatMessageId { get; set; }

    /// <summary>
    /// <ref name="ChatMessageContent">ChatMessageContent</ref> contains the text and fragments of the chat message.
    /// </summary>
    [JsonPropertyName("message")]
    public required ChatMessageContent Message { get; set; }

    /// <summary>
    /// Represents the content of a chat message.
    /// </summary>
    public class ChatMessageContent
    {
        /// <summary>
        /// The text of the chat message.
        /// </summary>
        [JsonPropertyName("text")]
        public required string Text { get; set; }
    }
}