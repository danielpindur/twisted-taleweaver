using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.DataAccess.Creatures.Dtos;

public class CharacterDto : CombatCreatureDto
{
    public CharacterClass Class { get; set; }
}