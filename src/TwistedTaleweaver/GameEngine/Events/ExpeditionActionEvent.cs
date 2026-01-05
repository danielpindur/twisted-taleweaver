using TwistedTaleweaver.Core.Kafka.Attributes;
using TwistedTaleweaver.GameEngine.Entities;
using TwistedTaleweaver.GameEngine.Entities.Enums;

namespace TwistedTaleweaver.GameEngine.Events;

public class ExpeditionActionEvent
{
    public Guid EventId { get; set; } = Guid.NewGuid();
    
    [PartitionKey]
    public Guid ExpeditionId { get; set; }
    
    public ExpeditionEventType ExpeditionEventType { get; set; }
    
    public ExpeditionEventSource ExpeditionEventSource { get; set; }
    
    public long? SourceMessageId { get; set; }
    
    public ActionAdjudication? ActionAdjudication { get; set; }
}