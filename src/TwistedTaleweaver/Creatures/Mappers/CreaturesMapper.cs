using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.DataAccess.Creatures.Dtos;

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
}