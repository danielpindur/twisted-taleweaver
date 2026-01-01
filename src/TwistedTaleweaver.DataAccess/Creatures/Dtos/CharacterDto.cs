using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.DataAccess.Creatures.Dtos;

public class CharacterDto : CreatureDto
{
    public int MaxHealth { get; set; }

    public int ArmorClass { get; set; }

    public int WalkingSpeed { get; init; }

    public int? FlyingSpeed { get; init; }

    public int? SwimmingSpeed { get; init; }

    public int? ClimbingSpeed { get; init; }

    public int? BurrowingSpeed { get; init; }

    public int? TeleportationSpeed { get; init; }

    public CharacterClass Class { get; set; }
}