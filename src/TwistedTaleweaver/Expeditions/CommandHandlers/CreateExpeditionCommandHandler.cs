using FluentValidation;
using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;
using TwistedTaleweaver.Integration.OpenAi.Expeditions;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class CreateExpeditionCommandHandler(
    IValidator<CreateExpeditionCommand> createExpeditionCommandValidator,
    IExpeditionGenerationAdapter expeditionGenerationAdapter,
    IExpeditionsWriter expeditionsWriter,
    IExpeditionParticipantsWriter expeditionParticipantsWriter) : ICreateExpeditionCommandHandler
{
    public async Task<CreateExpeditionResult> ExecuteAsync(CreateExpeditionCommand command)
    {
        await createExpeditionCommandValidator.ValidateAndThrowAsync(command);

        var generatedExpedition = await expeditionGenerationAdapter.GenerateAsync(
            new ExpeditionGenerationCommand()
            {
                ExpeditionName = command.Name,
                ExpeditionDescription = command.Description,
                Difficulty = command.Difficulty
            });

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