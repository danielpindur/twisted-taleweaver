using FluentValidation;
using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Validation;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;
using TwistedTaleweaver.Integration.OpenAi.Adapters;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class CreateExpeditionCommandHandler(
    IValidator<CreateExpeditionCommand> createExpeditionCommandValidator,
    IGenerationAdapter generationAdapter,
    IExpeditionsWriter expeditionsWriter,
    IExpeditionParticipantsWriter expeditionParticipantsWriter) : ICreateExpeditionCommandHandler
{
    public async Task<CreateExpeditionResult> ExecuteAsync(CreateExpeditionCommand command)
    {
        var validationResult = await createExpeditionCommandValidator.ValidateAsync(command);
        validationResult.ThrowIfInvalid();

        var generatedExpedition = await generationAdapter.GenerateExpeditionAsync(command.Difficulty, command.Name, command.Description);

        await using var scope = new UnitOfWorkScope();

        var expeditionId = await expeditionsWriter.CreateAsync(
            command.UserId,
            generatedExpedition.Name,
            generatedExpedition.Description,
            command.Difficulty,
            command.MinParticipants,
            command.MaxParticipants);

        await expeditionParticipantsWriter.AddAsync(expeditionId, command.UserId);

        var result = new CreateExpeditionResult() { ExpeditionId = expeditionId };

        await scope.CompleteAsync();

        return result;
    }
}