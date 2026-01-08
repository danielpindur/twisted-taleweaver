using TwistedTaleweaver.Gateway.Creatures.Contracts;
using TwistedTaleweaver.Gateway.Creatures.DataLoaders;
using TwistedTaleweaver.Gateway.Expeditions.DataLoaders;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[ObjectType<ExpeditionState>]
public static partial class ExpeditionStateNode
{
    [GraphQLDescription("Creature whose turn it is to act.")]
    public static async Task<Creature?> GetActiveCreatureAsync(
        [Parent] ExpeditionState state,
        CreatureByIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        if (!state.ActiveCreatureId.HasValue)
        {
            return null;
        }
        
        var creature = await dataLoader.LoadAsync(state.ActiveCreatureId.Value, cancellationToken);
        return creature ?? 
               throw new InvalidOperationException($"Active creature not found with ID {state.ActiveCreatureId.Value}");
    }
    
    [GraphQLDescription("States of creatures in the expedition.")]
    public static async Task<List<CombatCreatureState>> GetCreatureStatesAsync(
        [Parent] ExpeditionState state,
        CreatureStatesByExpeditionIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var creatureStates = await dataLoader.LoadAsync(state.ExpeditionId, cancellationToken);
        return creatureStates ?? 
               throw new InvalidOperationException($"Creature states not found for expedition with ID {state.ExpeditionId}");
    }
}