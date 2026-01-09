using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;

namespace TwistedTaleweaver.Integration.OpenAi.Creatures.Mappers;

public static class MonsterContextMapper
{
    public static List<MonsterContext> ToMonsterContexts(
        this List<Monster> source,
        Dictionary<Guid, List<CreatureTrait>> traitsByCharacterId,
        Dictionary<Guid, CombatCreatureState> stateByCharacterId)
    {
        return source.Select(x => x.ToMonsterContext(traitsByCharacterId, stateByCharacterId)).ToList();
    }
    
    public static MonsterContext ToMonsterContext(
        this Monster source,
        Dictionary<Guid, List<CreatureTrait>> traitsByCharacterId,
        Dictionary<Guid, CombatCreatureState> stateByCharacterId)
    {
        var state = stateByCharacterId[source.CreatureId];
        var traits = traitsByCharacterId[source.CreatureId];

        return new MonsterContext()
        {
            CreatureId = source.CreatureId,
            Name = source.Name,
            Description = source.Description,
            CreatureGender = source.Gender,
            NarrativeAlignment = source.Alignment,
            ChallengeRating = source.ChallengeRating,
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