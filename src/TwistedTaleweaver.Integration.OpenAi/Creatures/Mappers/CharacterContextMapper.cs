using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;

namespace TwistedTaleweaver.Integration.OpenAi.Creatures.Mappers;

public static class CharacterContextMapper
{
    public static List<CharacterContext> ToCharacterContexts(
        this List<Character> source,
        Dictionary<Guid, List<CreatureTrait>> traitsByCharacterId,
        Dictionary<Guid, CombatCreatureState> stateByCharacterId)
    {
        return source.Select(x => x.ToCharacterContext(traitsByCharacterId, stateByCharacterId)).ToList();
    }
    
    public static CharacterContext ToCharacterContext(
        this Character source,
        Dictionary<Guid, List<CreatureTrait>> traitsByCharacterId,
        Dictionary<Guid, CombatCreatureState> stateByCharacterId)
    {
        var state = stateByCharacterId[source.CreatureId];
        var traits = traitsByCharacterId[source.CreatureId];

        return new CharacterContext()
        {
            CreatureId = source.CreatureId,
            Name = source.Name,
            Description = source.Description,
            CreatureGender = source.Gender,
            NarrativeAlignment = source.Alignment,
            CharacterClass = source.Class,
            CreatureType = source.CreatureType,
            MaxHealth = source.MaxHealth,
            CurrentHealth = state.CurrentHealth,
            Status = state.Status,
            ArmorClass = source.ArmorClass,
            SpeedScores = source.SpeedScores,
            AttributeScores = source.AttributeScores,
            Traits = traits,
        };
    }
}