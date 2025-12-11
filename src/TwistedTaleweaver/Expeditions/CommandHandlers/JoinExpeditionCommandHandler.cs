using FluentValidation;
using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class JoinExpeditionCommandHandler(
    IExpeditionsReader expeditionsReader,
    IExpeditionParticipantsReader expeditionParticipantsReader,
    IExpeditionParticipantsWriter expeditionParticipantsWriter) : IJoinExpeditionCommandHandler
{
    public async Task ExecuteAsync(JoinExpeditionCommand command)
    {
        if (!await ExpeditionExistsAsync(command))
        {
            throw new ValidationException("Expedition does not exist.");
        }
        
        if (await HasAlreadyJoinedAsync(command))
        {
            throw new ValidationException("User has already joined the expedition.");
        }
        
        await expeditionParticipantsWriter.AddAsync(command.ExpeditionId, command.UserId);
    }
    
    private async Task<bool> HasAlreadyJoinedAsync(JoinExpeditionCommand command)
    {
        return await expeditionParticipantsReader.ExistsAsync(command.ExpeditionId, command.UserId);
    }

    private async Task<bool> ExpeditionExistsAsync(JoinExpeditionCommand command)
    {
        return await expeditionsReader.ExistsAsync(command.ExpeditionId);
    }
}