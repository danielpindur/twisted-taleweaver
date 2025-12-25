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
    IKafkaEventProducer kafkaEventProducer) : IBeginExpeditionCommandHandler
{
    public async Task ExecuteAsync(BeginExpeditionCommand command)
    {
        // TODO: Validation
        await using var scope = new UnitOfWorkScope();

        await expeditionsWriter.StartAsync(command.ExpeditionId);

        await SendExpeditionStartedMessageAsync(command);

        await scope.CompleteAsync();
    }

    private async Task SendExpeditionStartedMessageAsync(BeginExpeditionCommand command)
    {
        var messageDto = await messagesWriter.AddSystemMessageAsync(command.ExpeditionId, MessageConstants.ExpeditionStartSystemMessage);

        await kafkaEventProducer.PublishAsync(messageDto.ToMessageCreatedEvent());
    }
}