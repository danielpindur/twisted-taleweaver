using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Creatures.Mappers;
using TwistedTaleweaver.DataAccess.Creatures.Readers;

namespace TwistedTaleweaver.Creatures.QueryHandlers;

internal class CharactersQueryHandler(ICreaturesReader creaturesReader) : ICharactersQueryHandler
{
    public async Task<IEnumerable<Character>> ExecuteAsync(CharactersByIdQuery query)
    {
        var dtos = await creaturesReader.GetCharactersAsync(query.CharacterIds);
        return dtos.ToEntities();
    }

    public async Task<Character?> ExecuteAsync(CharacterByIdQuery query)
    {
        return (await ExecuteAsync(new CharactersByIdQuery() { CharacterIds = [query.CharacterId] })).SingleOrDefault();
    }

    public async Task<IEnumerable<Character>> ExecuteAsync(CharactersByExpeditionIdQuery query)
    {
        var dtos = await creaturesReader.GetCharactersAsync(query.ExpeditionId);
        return dtos.ToEntities();
    }
}