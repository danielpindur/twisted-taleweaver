using FluentValidation;
using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.Core.Validation;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class JoinExpeditionCommandHandler(
    IExpeditionParticipantsWriter expeditionParticipantsWriter,
    IValidator<JoinExpeditionCommand> joinExpeditionCommandValidator) : IJoinExpeditionCommandHandler
{
    public async Task ExecuteAsync(JoinExpeditionCommand command)
    {
        var validationResult = await joinExpeditionCommandValidator.ValidateAsync(command);
        validationResult.ThrowIfInvalid();

        await expeditionParticipantsWriter.AddAsync(command.ExpeditionId, command.UserId);
    }
}