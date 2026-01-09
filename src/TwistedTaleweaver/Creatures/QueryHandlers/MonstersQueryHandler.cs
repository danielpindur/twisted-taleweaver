using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Core.Queries;
using TwistedTaleweaver.Creatures.Mappers;
using TwistedTaleweaver.DataAccess.Creatures.Readers;

namespace TwistedTaleweaver.Creatures.QueryHandlers;

internal class MonsterByIdQuery
{
    public Guid MonsterId { get; set; }
}

internal interface IMonstersQueryHandler : IQueryHandler<MonsterByIdQuery, Monster>
{
}

internal class MonstersQueryHandler(ICreaturesReader creaturesReader) : IMonstersQueryHandler
{
    public async Task<Monster> ExecuteAsync(MonsterByIdQuery query)
    {
        var dtos = await creaturesReader.GetMonsterAsync(query.MonsterId);
        return dtos.ToEntity();
    }
}