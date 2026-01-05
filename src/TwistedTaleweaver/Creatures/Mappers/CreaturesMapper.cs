using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.DataAccess.Creatures.Dtos;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;

namespace TwistedTaleweaver.Creatures.Mappers;

public static class CreaturesMapper
{
    public static List<CreatureTrait> ToEntities(this List<CreatureTraitDto> source)
    {
        return source.Select(ToEntity).ToList();
    }
    
    public static CreatureTrait ToEntity(this CreatureTraitDto source)
    {
        return new CreatureTrait()
        {
            Name = source.TraitName,
            Effect = source.Effect,
            ConditionForPerform = source.ConditionDescription
        };
    }
    
    public static SpeedScores ToEntity(this GeneratedSpeedScores source)
    {
        return new SpeedScores()
        {
            Walking = source.Walking,
            Climbing = source.Climbing.HasValue ? source.Climbing.ActualValue : null,
            Swimming = source.Swimming.HasValue ? source.Swimming.ActualValue : null,
            Burrowing = source.Burrowing.HasValue ? source.Burrowing.ActualValue : null,
            Flying = source.Flying.HasValue ? source.Flying.ActualValue : null,
            Teleportation = source.Teleportation.HasValue ? source.Teleportation.ActualValue : null
        };
    }
    
    public static IEnumerable<Creature> ToEntities(this IEnumerable<CreatureDto> source)
    {
        return source.Select(ToEntity);
    }
    
    public static Creature ToEntity(this CreatureDto source)
    {
        return new Creature()
        {
            CreatureId = source.CreatureId,
            Name = source.Name,
            Description = source.Description,
            Gender = source.Gender,
            CreatureType = source.CreatureType,
            Alignment = source.Alignment,
            AttributeScores = new AttributeScores()
            {
                Strength = source.Strength,
                Dexterity = source.Dexterity,
                Constitution = source.Constitution,
                Intelligence = source.Intelligence,
                Wisdom = source.Wisdom,
                Charisma = source.Charisma,
            }
        };
    }
}