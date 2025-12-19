using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Api.Messages.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.Domain.Expeditions;
using TwistedTaleweaver.Expeditions.Mappers;
using TwistedTaleweaver.Integration.Clerk.Users;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionByIdQueryHandler(
    IExpeditionsReader expeditionsReader,
    IUsersAdapter usersAdapter,
    IExpeditionParticipantsQueryHandler expeditionParticipantsQueryHandler,
    IExpeditionMessagesSearchQueryHandler expeditionMessagesSearchQueryHandler) : IExpeditionByIdQueryHandler
{
    public async Task<Expedition?> ExecuteAsync(ExpeditionByIdQuery query)
    {
        var dto = await expeditionsReader.GetByIdAsync(query.ExpeditionId);
        
        if (dto is null)
        {
            return null;
        }

        var participants = await expeditionParticipantsQueryHandler.ExecuteAsync(
            new ExpeditionParticipantsQuery() { ExpeditionId = query.ExpeditionId });

        var userByExternalIdTask = usersAdapter
            .GetUsersAsync(new List<string>() { dto.CreatedByExternalUserId });
        
        var messages = await expeditionMessagesSearchQueryHandler.ExecuteAsync(
            new ExpeditionMessagesSearchQuery() { ExpeditionId = query.ExpeditionId });
        
        return dto.ToExpedition(participants, messages, await userByExternalIdTask);
    }
}