using TwistedTaleweaver.Api.Expeditions.Entities.Enums;
using TwistedTaleweaver.Core.Kafka.Attributes;

namespace TwistedTaleweaver.Api.Expeditions.Events;

public class ExpeditionStateUpdatedEvent
{
    [PartitionKey]
    public Guid ExpeditionId { get; set; }
    
    public ExpeditionStatus Status { get; set; }

    public Guid? ActiveUserId { get; set; }
}