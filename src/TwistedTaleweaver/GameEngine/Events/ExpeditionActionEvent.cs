using TwistedTaleweaver.Core.Kafka.Attributes;

namespace TwistedTaleweaver.GameEngine.Events;

public enum ExpeditionEventType
{
    /// <summary>
    /// Expedition was started -> generate narrative
    /// </summary>
    ExpeditionStarted = 1,
    
    /// <summary>
    /// Narrative was generated -> we need next actor to take it's turn
    /// </summary>
    NarrativeGenerated,
    
    /// <summary>
    /// An action has been read from the character message
    /// </summary>
    CharacterActionRequested
}

public enum ExpeditionEventSource
{
    System = 1,
    CharacterAction
}

public class ExpeditionActionEvent
{
    public Guid EventId { get; set; } = Guid.NewGuid();
    
    [PartitionKey]
    public Guid ExpeditionId { get; set; }
    
    public ExpeditionEventType ExpeditionEventType { get; set; }
    
    public ExpeditionEventSource ExpeditionEventSource { get; set; }
}