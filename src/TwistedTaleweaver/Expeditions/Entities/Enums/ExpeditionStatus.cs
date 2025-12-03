namespace TwistedTaleweaver.Expeditions.Entities.Enums;

/// <summary>
/// Represents the status of an expedition
/// </summary>
public enum ExpeditionStatus
{
    /// <summary>
    /// Expedition was created - can be joined by participants
    /// </summary>
    Created = 1,
    
    /// <summary>
    /// Expedition has started - is in progress
    /// </summary>
    Started,
    
    /// <summary>
    /// Expedition has been completed
    /// </summary>
    Completed
}