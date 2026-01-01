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
    
    [GraphQLDescription("States of characters in the expedition.")]
    public static async Task<List<CombatCreatureState>> GetCharacterStatesAsync(
        [Parent] ExpeditionState state,
        CharacterStatesByExpeditionIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var characterStates = await dataLoader.LoadAsync(state.ExpeditionId, cancellationToken);
        return characterStates ?? 
               throw new InvalidOperationException($"Character states not found for expedition with ID {state.ExpeditionId}");
    }
}