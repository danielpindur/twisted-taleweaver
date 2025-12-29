using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.DataAccess.Creatures.Readers;
using TwistedTaleweaver.Domain.Creatures;

namespace TwistedTaleweaver.Creatures.QueryHandlers;

internal class CharacterByIdQueryHandler(ICreaturesReader creaturesReader) : ICharacterByIdQueryHandler
{
    public async Task<Character?> ExecuteAsync(CharacterByIdQuery query)
    {
        var dto = await creaturesReader.GetCharacterAsync(query.CharacterId);

        if (dto is null)
        {
            return null;
        }

        var traits = await creaturesReader.GetTraitsAsync(query.CharacterId);

        var character = new Character()
        {
            CharacterId = dto.CreatureId,
            Name = dto.Name,
            ContextKeywords = dto.ContextKeywords,
            Gender = dto.Gender,
            CreatureType = dto.CreatureType,
            Alignment = dto.Alignment,
            Traits = traits.Select(x => new Trait()
            {
                Name = x.TraitName,
                Effect = x.Effect,
                ConditionForPerform = x.ConditionDescription
            }).ToList(),
            AttributeScores = new AttributeScores()
            {
                Strength = dto.Strength,
                Dexterity = dto.Dexterity,
                Constitution = dto.Constitution,
                Intelligence = dto.Intelligence,
                Wisdom = dto.Wisdom,
                Charisma = dto.Charisma,
            },
            MaxHealth = dto.MaxHealth,
            ArmorClass = dto.ArmorClass,
            SpeedScores = new SpeedScores()
            {
                Walking = dto.WalkingSpeed,
                Climbing = dto.ClimbingSpeed,
                Swimming = dto.SwimmingSpeed,
                Burrowing = dto.BurrowingSpeed,
                Flying = dto.FlyingSpeed,
                Teleportation = dto.TeleportationSpeed
            },
            Class = dto.Class,
        };

        return character;
    }
}