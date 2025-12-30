using HotChocolate.Authorization;
using HotChocolate.Subscriptions;
using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Gateway.Authorization;
using TwistedTaleweaver.Gateway.Expeditions.Contracts;
using TwistedTaleweaver.Gateway.Expeditions.Helpers;
using TwistedTaleweaver.Gateway.Expeditions.Mappers;

namespace TwistedTaleweaver.Gateway.Expeditions;

[MutationType]
public static class ExpeditionsMutations
{
    [GraphQLDescription("Creates a new expedition.")]
    [Authorize]
    public static async Task<Expedition> AddExpeditionAsync(
        AddExpeditionInput input,
        [Service] IUserProvider userProvider,
        [Service] ICreateExpeditionCommandHandler createExpeditionCommandHandler,
        [Service] IExpeditionByIdQueryHandler expeditionByIdQueryHandler)
    {
        var userId = await userProvider.GetUserIdAsync();

        var creationResult = await createExpeditionCommandHandler.ExecuteAsync(
            new CreateExpeditionCommand()
            {
                Name = input.Name,
                Description = input.Description,
                Difficulty = input.Difficulty,
                MinParticipants = input.MinParticipants,
                MaxParticipants = input.MaxParticipants,
                UserId = userId
            });

        var expedition = await expeditionByIdQueryHandler.ExecuteAsync(
            new ExpeditionByIdQuery() { ExpeditionId = creationResult.ExpeditionId });

        return expedition!.ToContract();
    }

    [GraphQLDescription("Joins an existing expedition.")]
    [Authorize]
    public static async Task<Expedition> JoinExpeditionAsync(
        JoinExpeditionInput input,
        [Service] IUserProvider userProvider,
        [Service] IJoinExpeditionCommandHandler joinExpeditionCommandHandler,
        [Service] IExpeditionByIdQueryHandler expeditionByIdQueryHandler,
        [Service] ITopicEventSender topicEventSender)
    {
        var userId = await userProvider.GetUserIdAsync();

        await joinExpeditionCommandHandler.ExecuteAsync(
            new JoinExpeditionCommand() { ExpeditionId = input.ExpeditionId, UserId = userId });
        
        var expedition = await expeditionByIdQueryHandler.ExecuteAsync(
            new ExpeditionByIdQuery() { ExpeditionId = input.ExpeditionId });

        var expeditionContract = expedition!.ToContract();

        await topicEventSender.PublishParticipantsChanged(expeditionContract);

        return expeditionContract;
    }
    
    [GraphQLDescription("Starts an existing expedition.")]
    [Authorize]
    public static async Task<Expedition> StartExpeditionAsync(
        StartExpeditionInput input,
        [Service] IUserProvider userProvider,
        [Service] IBeginExpeditionCommandHandler beginExpeditionCommandHandler,
        [Service] IExpeditionByIdQueryHandler expeditionByIdQueryHandler)
    {
        var userId = await userProvider.GetUserIdAsync();

        await beginExpeditionCommandHandler.ExecuteAsync(
            new BeginExpeditionCommand() { ExpeditionId = input.ExpeditionId, UserId = userId });
        
        var expedition = await expeditionByIdQueryHandler.ExecuteAsync(
            new ExpeditionByIdQuery() { ExpeditionId = input.ExpeditionId });

        return expedition!.ToContract();
    }
}