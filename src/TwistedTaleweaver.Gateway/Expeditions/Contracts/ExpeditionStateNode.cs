using TwistedTaleweaver.Gateway.Creatures.Contracts;
using TwistedTaleweaver.Gateway.Creatures.DataLoaders;
using TwistedTaleweaver.Gateway.Users.Contracts;
using TwistedTaleweaver.Gateway.Users.DataLoaders;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[ObjectType<ExpeditionState>]
public static partial class ExpeditionStateNode
{
    [GraphQLDescription("User whose turn it is to act.")]
    public static async Task<User?> GetActiveUserAsync(
        [Parent] ExpeditionState state,
        UserByIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        if (!state.ActiveUserId.HasValue)
        {
            return null;
        }
        
        var user = await dataLoader.LoadAsync(state.ActiveUserId.Value, cancellationToken);
        return user!;
    }
    
    [GraphQLDescription("States of characters in the expedition.")]
    public static async Task<List<CombatCreatureState>> GetCharacterStatesAsync(
        [Parent] ExpeditionState state,
        CharacterStatesByExpeditionIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var characterStates = await dataLoader.LoadAsync(state.ExpeditionId, cancellationToken);
        return characterStates!;
    }
}