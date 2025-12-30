namespace TwistedTaleweaver.Api.Messages.Entities.Enums;

/// <summary>
/// Defines different types of messages
/// </summary>
public enum MessageType
{
    /// <summary>
    /// Narrative messages sent by Twisted Taleweaver
    /// </summary>
    Narrative = 1,

    /// <summary>
    /// Either messages sent by a characters or action taken by monsters
    /// </summary>
    Action,

    /// <summary>
    /// System messages - expedition starting, roll results, etc.
    /// </summary>
    System
}