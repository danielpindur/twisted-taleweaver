using System.Runtime.Serialization;

namespace TwistedTaleweaver.Bridge.Twitch.Common.Models.Enums;

/// <summary>
/// Represents the status of a Twitch EventSub subscription.
/// </summary>
internal enum SubscriptionStatus
{
    /// <summary>
    /// The subscription is enabled and active.
    /// </summary>
    [EnumMember(Value = "enabled")]
    Enabled,

    /// <summary>
    /// The user mentioned in the subscription no longer exists
    /// </summary>
    [EnumMember(Value = "user_removed")]
    UserRemoved,

    /// <summary>
    /// The user revoked the authorization token that the subscription relied on
    /// </summary>
    [EnumMember(Value = "authorization_revoked")]
    AuthorizationRevoked,

    /// <summary>
    /// The subscribed to subscription type and version is no longer supported
    /// </summary>
    [EnumMember(Value = "version_removed")]
    VersionRemoved
}