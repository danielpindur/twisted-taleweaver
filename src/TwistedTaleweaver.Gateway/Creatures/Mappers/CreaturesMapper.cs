using TwistedTaleweaver.Api.Creatures.Entities;
using CreatureTraitContract = TwistedTaleweaver.Gateway.Creatures.Contracts.CreatureTrait;
using CharacterContract = TwistedTaleweaver.Gateway.Creatures.Contracts.Character;
using SpeedScoresContract =  TwistedTaleweaver.Gateway.Creatures.Contracts.SpeedScores;
using AttributeScoresContract =  TwistedTaleweaver.Gateway.Creatures.Contracts.AttributeScores;

namespace TwistedTaleweaver.Gateway.Creatures.Mappers;

internal static class CreaturesMapper
{
    public static CreatureTraitContract ToContract(this CreatureTrait source)
    {
        return new CreatureTraitContract()
        {
            Name = source.Name,
            Effect = source.Effect,
            ConditionForPerform = source.ConditionForPerform,
        };
    }

    public static List<CreatureTraitContract> ToContracts(this List<CreatureTrait> source)
    {
        return source.Select(ToContract).ToList();
    }
    
    public static Dictionary<Guid, List<CreatureTraitContract>> ToContracts(
        this Dictionary<Guid, List<CreatureTrait>> source)
    {
        var result = new Dictionary<Guid, List<CreatureTraitContract>>();

        foreach (var kvp in source)
        {
            if (!result.ContainsKey(kvp.Key))
            {
                result.Add(kvp.Key, kvp.Value.ToContracts());
            }
        }
        
        return result;
    }

    public static CharacterContract ToContract(this Character source)
    {
        return new CharacterContract()
        {
            Id = source.CharacterId,
            Name = source.Name,
            Gender = source.Gender,
            CreatureType = source.CreatureType,
            Alignment = source.Alignment,
            Class = source.Class,
            MaxHealth = source.MaxHealth,
            ArmorClass = source.ArmorClass,
            SpeedScores = new SpeedScoresContract()
            {
                Walking = source.SpeedScores.Walking,
                Climbing = source.SpeedScores.Climbing,
                Swimming = source.SpeedScores.Swimming,
                Burrowing = source.SpeedScores.Burrowing,
                Flying = source.SpeedScores.Flying,
                Teleportation = source.SpeedScores.Teleportation
            },
            AttributeScores = new AttributeScoresContract()
            {
                Strength = source.AttributeScores.Strength,
                Intelligence = source.AttributeScores.Intelligence,
                Charisma = source.AttributeScores.Charisma,
                Constitution = source.AttributeScores.Constitution,
                Dexterity = source.AttributeScores.Dexterity,
                Wisdom = source.AttributeScores.Wisdom,
            }
        };
    }
}