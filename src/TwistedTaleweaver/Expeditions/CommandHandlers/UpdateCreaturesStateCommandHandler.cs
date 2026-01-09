using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.DataAccess.Creatures.Writers;
using TwistedTaleweaver.GameEngine.CommandHandlers;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class UpdateCreaturesStateCommand
{
    public Guid ExpeditionId { get; set; }

    public required List<CreatureStateUpdate> StateUpdates { get; set; }
}

internal interface IUpdateCreaturesStateCommandHandler : ICommandHandler<UpdateCreaturesStateCommand>
{
}

internal class UpdateCreaturesStateCommandHandler(
    ICreaturesWriter creaturesWriter,
    ICombatCreatureStatesQueryHandler combatCreatureStatesQueryHandler,
    ITurnOrderCommandHandler turnOrderCommandHandler) : IUpdateCreaturesStateCommandHandler
{
    public async Task ExecuteAsync(UpdateCreaturesStateCommand command)
    {
        await creaturesWriter.SaveStateUpdatesAsync(command.StateUpdates);
        
        var updatedCreatureIds = command.StateUpdates.Select(x => x.CreatureId);

        var statesAfterUpdate = await GetStatesAfterUpdate(command.ExpeditionId);

        foreach (var creatureId in updatedCreatureIds)
        {
            statesAfterUpdate.TryGetValue(creatureId, out var creatureState);

            if (creatureState is not null && creatureState.CurrentHealth > 0)
            {
                continue;
            }
            
            await turnOrderCommandHandler.ExecuteAsync(new RemoveCreatureCommand() { ExpeditionId = command.ExpeditionId, CreatureId = creatureId });
        }
    }

    private async Task<Dictionary<Guid, CombatCreatureState>> GetStatesAfterUpdate(Guid expeditionId)
    {
        var statesAfterUpdate = await combatCreatureStatesQueryHandler
            .ExecuteAsync(new CharactersStatesQuery() { ExpeditionIds = [expeditionId] });
        
        return statesAfterUpdate[expeditionId].ToDictionary(x => x.CreatureId);
    }
}