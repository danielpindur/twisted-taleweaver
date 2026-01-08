using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.DataAccess.Creatures.Dtos;

namespace TwistedTaleweaver.Creatures.Mappers;

public static class MonstersMapper
{
    public static List<Monster> ToEntities(this List<MonsterDto>? source)
    {
        if (source is null)
        {
            return new List<Monster>();
        }

        return source.Select(ToEntity).ToList();
    }

    public static Monster ToEntity(this MonsterDto source)
    {
        return new Monster()
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
            },
            MaxHealth = source.MaxHealth,
            ArmorClass = source.ArmorClass,
            SpeedScores = new SpeedScores()
            {
                Walking = source.WalkingSpeed,
                Climbing = source.ClimbingSpeed,
                Swimming = source.SwimmingSpeed,
                Burrowing = source.BurrowingSpeed,
                Flying = source.FlyingSpeed,
                Teleportation = source.TeleportationSpeed
            },
            ChallengeRating = source.ChallengeRating,
        };
    }
}