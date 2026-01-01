using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.DataAccess.Messages.Writers;
using TwistedTaleweaver.Messages.Mappers;

namespace TwistedTaleweaver.Messages.CommandHandlers;

internal class AddSystemMessageCommand
{
    public Guid ExpeditionId { get; set; }
    
    public required string Content { get; set; }
}

internal class AddNarrativeMessageCommand
{
    public Guid ExpeditionId { get; set; }
    
    public required string Content { get; set; }
}

internal interface IChatMessagesCommandHandler : 
    ICommandHandler<AddSystemMessageCommand>,
    ICommandHandler<AddNarrativeMessageCommand>
{
}

internal class ChatMessagesCommandHandler(
    IMessagesWriter messagesWriter,
    IKafkaEventProducer kafkaEventProducer) : IChatMessagesCommandHandler
{
    public async Task ExecuteAsync(AddSystemMessageCommand command)
    {
        var messageDto = await messagesWriter.AddSystemMessageAsync(command.ExpeditionId, command.Content);

        await kafkaEventProducer.PublishAsync(messageDto.ToMessageCreatedEvent());
    }
    
    public async Task ExecuteAsync(AddNarrativeMessageCommand command)
    {
        var messageDto = await messagesWriter.AddNarrativeMessageAsync(command.ExpeditionId, command.Content);

        await kafkaEventProducer.PublishAsync(messageDto.ToMessageCreatedEvent());
    }
}