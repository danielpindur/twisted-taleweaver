using System.Runtime.Serialization;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Models.Enums;

/// <summary>
/// Represent the status of a Twitch EventSub WebSocket session.
/// </summary>
internal enum SessionStatus
{
    /// <summary>
    /// The session is active
    /// </summary>
    [EnumMember(Value = "enabled")]
    Enabled = 1,

    /// <summary>
    /// The session is pending reconnection
    /// </summary>
    [EnumMember(Value = "reconnecting")]
    Reconnecting,

    /// <summary>
    /// The session has successfully established a connection.
    /// </summary>
    [EnumMember(Value = "connected")]
    Connected
}