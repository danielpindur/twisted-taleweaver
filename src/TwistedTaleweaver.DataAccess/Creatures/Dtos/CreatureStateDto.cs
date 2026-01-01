using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.DataAccess.Creatures.Dtos;

public class CreatureStateDto
{
    public Guid ExpeditionId { get; set; }
    
    public Guid CreatureId { get; set; }
    
    public CreatureStatus? Status { get; set; }
    
    public int CurrentHealth { get; set;}
}