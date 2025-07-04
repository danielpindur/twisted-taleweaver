using TwistedTaleweaver.DataAccess.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.DataAccess.Expeditions.Entities;

public class Expedition
{
    public Guid ExpeditionId { get; set; }
    
    public Guid StreamId { get; set; }
    
    public Guid BroadcasterUserId { get; set; }
    
    public required string BroadcasterExternalUserId { get; set; }
    
    public ExpeditionStatus Status { get; set; }
    
    public Guid CreatedByUserId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? StartedAt { get; set; }
    
    public DateTimeOffset? CompletedAt { get; set; }
    
    public DateTimeOffset? FailedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }
}