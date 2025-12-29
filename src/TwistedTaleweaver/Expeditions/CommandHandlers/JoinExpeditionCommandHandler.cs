using FluentValidation;
using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class JoinExpeditionCommandHandler(
    IExpeditionParticipantsWriter expeditionParticipantsWriter,
    IValidator<JoinExpeditionCommand> joinExpeditionCommandValidator) : IJoinExpeditionCommandHandler
{
    public async Task ExecuteAsync(JoinExpeditionCommand command)
    {
        await joinExpeditionCommandValidator.ValidateAndThrowAsync(command);

        await expeditionParticipantsWriter.AddAsync(command.ExpeditionId, command.UserId);
    }
}