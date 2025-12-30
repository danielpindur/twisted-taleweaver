using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.DataAccess.Creatures.Dtos;

namespace TwistedTaleweaver.Creatures.Mappers;

public static class CharactersMapper
{
    public static IEnumerable<Character> ToEntities(this IEnumerable<CharacterDto> source)
    {
        return source.Select(ToEntity);
    }
    
    public static Character ToEntity(this CharacterDto source)
    {
        return new Character()
        {
            CharacterId = source.CreatureId,
            Name = source.Name,
            ContextKeywords = source.ContextKeywords,
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
            Class = source.Class,
        };
    }
}