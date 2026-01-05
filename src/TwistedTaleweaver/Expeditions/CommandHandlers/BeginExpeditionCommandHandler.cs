using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;
using TwistedTaleweaver.GameEngine.CommandHandlers;
using TwistedTaleweaver.GameEngine.Entities.Enums;
using TwistedTaleweaver.GameEngine.Events;
using TwistedTaleweaver.Messages;
using TwistedTaleweaver.Messages.CommandHandlers;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class BeginExpeditionCommandHandler(
    IExpeditionsWriter expeditionsWriter,
    IInternalChatMessagesCommandHandler chatMessagesCommandHandler,
    IExpeditionStateCommandHandler expeditionStateCommandHandler,
    ITurnOrderCommandHandler turnOrderCommandHandler,
    IKafkaEventProducer kafkaEventProducer) : IBeginExpeditionCommandHandler
{
    public async Task ExecuteAsync(BeginExpeditionCommand command)
    {
        // TODO: Validation
        await using var scope = new UnitOfWorkScope();

        await expeditionsWriter.StartAsync(command.ExpeditionId);
        await turnOrderCommandHandler.ExecuteAsync(new ConfigureTurnOrderCommand() { ExpeditionId = command.ExpeditionId });

        // TODO: Needs outbox pattern
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