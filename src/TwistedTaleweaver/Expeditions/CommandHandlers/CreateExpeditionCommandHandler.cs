using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.Core.Database.UnitOfWork;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;
using TwistedTaleweaver.Integration.OpenAi.Adapters;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class CreateExpeditionCommandHandler(
    IGenerationAdapter generationAdapter,
    IExpeditionsWriter expeditionsWriter,
    IExpeditionParticipantsWriter expeditionParticipantsWriter) : ICreateExpeditionCommandHandler
{
    public async Task<CreateExpeditionResult> ExecuteAsync(CreateExpeditionCommand command)
    {
        var generatedExpedition = await generationAdapter.GenerateAsync(command.Difficulty, command.Name, command.Description);

        await using var scope = new UnitOfWorkScope();
        
        var expeditionId = await expeditionsWriter.CreateAsync(
            command.UserId,
            generatedExpedition.Name,
            generatedExpedition.Description,
            command.Difficulty,
            command.MaxParticipants);
            
        await expeditionParticipantsWriter.AddAsync(expeditionId, command.UserId);

        var result = new CreateExpeditionResult() { ExpeditionId = expeditionId };
            
        await scope.CompleteAsync();

        return result;
    }
}