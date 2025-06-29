using System.Runtime.Serialization;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Models.Enums;

/// <summary>
/// Represents the type of message received from the Twitch EventSub WebSocket connection.
/// </summary>
internal enum MessageType
{
    /// <summary>
    /// First message after client connects to the server
    /// </summary>
    [EnumMember(Value = "session_welcome")]
    Welcome = 1,

    /// <summary>
    /// Message to indicate that the WebSocket connection is healthy
    /// </summary>
    [EnumMember(Value = "session_keepalive")]
    Keepalive,

    /// <summary>
    /// Message sent when an event that you subscribe to occurs
    /// </summary>
    [EnumMember(Value = "notification")]
    Notification,

    /// <summary>
    /// Message server sends if the server must drop the connection
    /// </summary>
    [EnumMember(Value = "session_reconnect")]
    Reconnect,

    /// <summary>
    /// Message sent if the user no longer exists or they revoked the authorization token that the subscription relied on
    /// </summary>
    [EnumMember(Value = "revocation")]
    Revocation
}