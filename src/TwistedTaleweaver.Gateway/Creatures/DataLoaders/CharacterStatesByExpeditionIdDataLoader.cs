using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Gateway.Creatures.Contracts;
using TwistedTaleweaver.Gateway.Creatures.Mappers;

namespace TwistedTaleweaver.Gateway.Creatures.DataLoaders;

public class CharacterStatesByExpeditionIdDataLoader(
    IBatchScheduler batchScheduler,
    DataLoaderOptions options,
    ICombatCreatureStatesQueryHandler combatCreatureStatesQueryHandler)
    : BatchDataLoader<Guid, List<CombatCreatureState>>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<Guid, List<CombatCreatureState>>> LoadBatchAsync(
        IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var characterStatesByExpeditionId = await combatCreatureStatesQueryHandler.ExecuteAsync(
            new CharactersStatesQuery() { ExpeditionIds = keys.ToList() });

        return characterStatesByExpeditionId.ToContracts();
    }
}