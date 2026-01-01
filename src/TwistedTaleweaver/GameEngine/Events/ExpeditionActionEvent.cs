using TwistedTaleweaver.Core.Kafka.Attributes;

namespace TwistedTaleweaver.GameEngine.Events;

public enum ExpeditionEventType
{
    ExpeditionStarted = 1,
    NarrativeGenerated
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