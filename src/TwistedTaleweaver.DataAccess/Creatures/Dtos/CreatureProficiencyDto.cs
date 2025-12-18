using TwistedTaleweaver.Domain.Creatures.Enums;

namespace TwistedTaleweaver.DataAccess.Creatures.Dtos;

public class CreatureProficiencyDto
{
    public Guid CreatureId { get; set; }
    
    public ProficiencyType Type { get; set; }
    
    public required string Target { get; set; }
}