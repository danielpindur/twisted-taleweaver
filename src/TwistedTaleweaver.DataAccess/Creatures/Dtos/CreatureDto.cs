using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.DataAccess.Creatures.Dtos;

public class CreatureDto
{
    public Guid CreatureId { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public CreatureType CreatureType { get; set; }

    public NarrativeAlignment Alignment { get; set; }

    public CreatureGender Gender { get; set; }

    public int Strength { get; set; }

    public int Dexterity { get; set; }

    public int Constitution { get; set; }

    public int Intelligence { get; set; }

    public int Wisdom { get; set; }

    public int Charisma { get; set; }
}