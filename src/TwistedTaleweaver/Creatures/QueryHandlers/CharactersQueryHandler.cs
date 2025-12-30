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
}