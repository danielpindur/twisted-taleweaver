using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.Api.Creatures.Entities;

public class CreatureStateUpdate
{
    public Guid CreatureId { get; set; }
    
    public int? HealthUpdate { get; set; }
    
    public CreatureStatus Status { get; set; }
}