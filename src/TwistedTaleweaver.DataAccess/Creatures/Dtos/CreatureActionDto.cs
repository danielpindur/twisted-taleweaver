namespace TwistedTaleweaver.DataAccess.Creatures.Dtos;

public class CreatureActionDto
{
    public Guid CreatureId { get; set; }

    public required string ActionName { get; set; }

    public required string Description { get; set; }

    public bool IsCombatAction { get; set; }

    public bool IsLegendaryAction { get; set; }

    public int Cost { get; set; }

    public int? Recharge { get; set; }
    
    public int? Range { get; set; }
}