using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;
using TwistedTaleweaver.DataAccess.Messages.Writers;
using TwistedTaleweaver.Messages;
using TwistedTaleweaver.Messages.Mappers;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class BeginExpeditionCommandHandler(
    IExpeditionsWriter expeditionsWriter,
    IMessagesWriter messagesWriter,
    IExpeditionStateCommandHandler expeditionStateCommandHandler,
    IKafkaEventProducer kafkaEventProducer) : IBeginExpeditionCommandHandler
{
    public async Task ExecuteAsync(BeginExpeditionCommand command)
    {
        // TODO: Validation
        await using var scope = new UnitOfWorkScope();

        await expeditionsWriter.StartAsync(command.ExpeditionId);

        await expeditionStateCommandHandler.ExecuteAsync(
            new PublishUpdatedExpeditionStateCommand() { ExpeditionId = command.ExpeditionId });
        await PublishExpeditionStartedMessageAsync(command.ExpeditionId);

        await scope.CompleteAsync();
    }

    private async Task PublishExpeditionStartedMessageAsync(Guid expeditionId)
    {
        var messageDto = await messagesWriter.AddSystemMessageAsync(expeditionId, MessageConstants.ExpeditionStartSystemMessage);

        await kafkaEventProducer.PublishAsync(messageDto.ToMessageCreatedEvent());
    }
}