using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Creatures.Mappers;
using TwistedTaleweaver.DataAccess.Creatures.Readers;

namespace TwistedTaleweaver.Creatures.QueryHandlers;

internal class CreatureTraitsQueryHandler(ICreaturesReader creaturesReader) : ICreatureTraitsQueryHandler
{
    public async Task<Dictionary<Guid, List<CreatureTrait>>> ExecuteAsync(CreatureTraitsQuery query)
    {
        var creatureTraits = await creaturesReader.GetTraitsAsync(query.CreatureIds);

        var creatureTraitsByCreatureId = creatureTraits
            .GroupBy(x => x.CreatureId)
            .ToDictionary(
                x => x.Key,
                y => y.ToList().ToEntities());

        foreach (var creatureId in query.CreatureIds)
        {
            if (!creatureTraitsByCreatureId.ContainsKey(creatureId))
            {
                creatureTraitsByCreatureId[creatureId] = new List<CreatureTrait>();
            }
        }

        return creatureTraitsByCreatureId;
    }
}