using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Creatures.Mappers;
using TwistedTaleweaver.DataAccess.Creatures.Readers;

namespace TwistedTaleweaver.Creatures.QueryHandlers;

internal class CreaturesQueryHandler(ICreaturesReader creaturesReader) : ICreaturesQueryHandler
{
    public async Task<IEnumerable<Creature>> ExecuteAsync(CreaturesByIdQuery query)
    {
        var dtos = await creaturesReader.GetCreaturesAsync(query.CreatureIds);

        return dtos.ToEntities();
    }
}