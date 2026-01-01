using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;
using TwistedTaleweaver.GameEngine.Events;
using TwistedTaleweaver.Messages;
using TwistedTaleweaver.Messages.CommandHandlers;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class BeginExpeditionCommandHandler(
    IExpeditionsWriter expeditionsWriter,
    IChatMessagesCommandHandler chatMessagesCommandHandler,
    IExpeditionStateCommandHandler expeditionStateCommandHandler,
    IKafkaEventProducer kafkaEventProducer) : IBeginExpeditionCommandHandler
{
    public async Task ExecuteAsync(BeginExpeditionCommand command)
    {
        // TODO: Validation
        await using var scope = new UnitOfWorkScope();

        await expeditionsWriter.StartAsync(command.ExpeditionId);

        // TODO: Probably needs outbox pattern
        await expeditionStateCommandHandler.ExecuteAsync(new PublishUpdatedExpeditionStateCommand() { ExpeditionId = command.ExpeditionId });
        await PublishExpeditionStartedMessageAsync(command.ExpeditionId);
        await PublishExpeditionActionAsync(command.ExpeditionId);

        await scope.CompleteAsync();
    }

    private Task PublishExpeditionStartedMessageAsync(Guid expeditionId)
    {
        return chatMessagesCommandHandler.ExecuteAsync(new AddSystemMessageCommand()
        {
            ExpeditionId = expeditionId,
            Content = MessageConstants.ExpeditionStartSystemMessage
        });
    }

    private async Task PublishExpeditionActionAsync(Guid expeditionId)
    {
        var action = new ExpeditionActionEvent()
        {
            ExpeditionId = expeditionId,
            ExpeditionEventSource = ExpeditionEventSource.CharacterAction,
            ExpeditionEventType = ExpeditionEventType.ExpeditionStarted
        };

        await kafkaEventProducer.PublishAsync(action);
    } 
}