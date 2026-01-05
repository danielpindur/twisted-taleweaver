using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Core.Queries;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Mappers;

namespace TwistedTaleweaver.Creatures.QueryHandlers;

public class GetCharacterContextsByExpeditionQuery
{
    public Guid ExpeditionId { get; set; }
}

internal interface ICharacterContextsQueryHandler : IQueryHandler<GetCharacterContextsByExpeditionQuery, List<CharacterContext>>
{
}

internal class CharacterContextsQueryHandler(
    ICharactersQueryHandler charactersQueryHandler,
    ICreatureTraitsQueryHandler creatureTraitsQueryHandler,
    ICombatCreatureStatesQueryHandler combatCreatureStatesQueryHandler) : ICharacterContextsQueryHandler
{
    public async Task<List<CharacterContext>> ExecuteAsync(GetCharacterContextsByExpeditionQuery query)
    {
        var characters = (await charactersQueryHandler.ExecuteAsync(
            new CharactersByExpeditionIdQuery() { ExpeditionId = query.ExpeditionId })).ToList();
        
        var characterIds = characters.Select(c => c.CreatureId).ToList();
        
        var traitsByCharacterId = await creatureTraitsQueryHandler.ExecuteAsync(
            new CreatureTraitsQuery() { CreatureIds = characterIds });

        var characterStatesByExpeditionId = await combatCreatureStatesQueryHandler.ExecuteAsync(
            new CharactersStatesQuery() { ExpeditionIds = [query.ExpeditionId] });

        var characterStateById = characterStatesByExpeditionId[query.ExpeditionId].ToDictionary(x => x.CreatureId);

        return characters.ToCharacterContexts(traitsByCharacterId, characterStateById);
    }
}