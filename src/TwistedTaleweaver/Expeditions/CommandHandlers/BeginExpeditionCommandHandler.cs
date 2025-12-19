using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;
using TwistedTaleweaver.DataAccess.Messages.Writers;
using TwistedTaleweaver.Messages;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class BeginExpeditionCommandHandler(
    IExpeditionsWriter expeditionsWriter,
    IMessagesWriter messagesWriter) : IBeginExpeditionCommandHandler
{
    public async Task ExecuteAsync(BeginExpeditionCommand command)
    {
        // TODO: Validation
        await using var scope = new UnitOfWorkScope();
        
        await expeditionsWriter.StartAsync(command.ExpeditionId);
        await messagesWriter.AddSystemMessageAsync(command.ExpeditionId, MessageConstants.ExpeditionStartSystemMessage);

        await scope.CompleteAsync();
    }
}