using TwistedTaleweaver.Gateway.Expeditions.DataLoaders;
using TwistedTaleweaver.Gateway.Messages.Contracts;
using TwistedTaleweaver.Gateway.Messages.DataLoaders;
using TwistedTaleweaver.Gateway.Users.Contracts;
using TwistedTaleweaver.Gateway.Users.DataLoaders;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[ObjectType<Expedition>]
public static partial class ExpeditionNode
{
    [GraphQLDescription("User who created the expedition.")]
    public static async Task<User> GetUserCreatedAsync(
        [Parent] Expedition expedition,
        UserByIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var user = await dataLoader.LoadAsync(expedition.CreatedByUserId, cancellationToken);
        return user ?? throw new InvalidOperationException($"User who created the expedition not found with ID {expedition.CreatedByUserId}");
    }

    [GraphQLDescription("Participants of the expedition.")]
    public static async Task<List<ExpeditionParticipant>> GetParticipantsAsync(
        [Parent] Expedition expedition, 
        ExpeditionParticipantsByExpeditionIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var participants = await dataLoader.LoadAsync(expedition.Id, cancellationToken);
        return participants ?? throw new InvalidOperationException($"Participants not found for expedition with ID {expedition.Id}");
    }

    [GraphQLDescription("The state of the expedition.")]
    public static async Task<ExpeditionState> GetStateAsync(
        [Parent] Expedition expedition,
        ExpeditionStateByExpeditionIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var state = await dataLoader.LoadAsync(expedition.Id, cancellationToken);
        return state ?? throw new InvalidOperationException($"Expedition state not found for expedition with ID {expedition.Id}");
    }
    
    [GraphQLDescription("The narrative messages of the expedition.")]
    public static async Task<List<ChatMessage>> GetNarrativeMessagesAsync(
        [Parent] Expedition expedition,
        ExpeditionNarrativeMessagesDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var chatMessages = await dataLoader.LoadAsync(expedition.Id, cancellationToken);
        return chatMessages ?? throw new InvalidOperationException($"Narrative messages not found for expedition with ID {expedition.Id}");
    }
}