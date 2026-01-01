using TwistedTaleweaver.Api.Messages.Commands;
using TwistedTaleweaver.Api.Messages.Entities;
using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.DataAccess.Messages.Writers;
using TwistedTaleweaver.Expeditions.CommandHandlers;
using TwistedTaleweaver.GameEngine.CommandHandlers;
using TwistedTaleweaver.GameEngine.Events;
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

internal interface IInternalChatMessagesCommandHandler : 
    IChatMessagesCommandHandler,
    ICommandHandler<AddSystemMessageCommand>,
    ICommandHandler<AddNarrativeMessageCommand>
{
}

internal class ChatMessagesCommandHandler(
    IMessagesWriter messagesWriter,
    IExpeditionParticipantsReader expeditionParticipantsReader,
    IKafkaEventProducer kafkaEventProducer,
    ITurnOrderCommandHandler turnOrderCommandHandler,
    IExpeditionStateCommandHandler expeditionStateCommandHandler) : IInternalChatMessagesCommandHandler
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

    public async Task<ChatMessage> ExecuteAsync(AddCharacterMessageCommand command)
    {
        // TODO: Validations
        var character = await expeditionParticipantsReader.GetForExpeditionByUserAsync(command.ExpeditionId, command.UserId);

        await using var scope = new UnitOfWorkScope();
        
        var messageDto = await messagesWriter.AddCharacterMessageAsync(command.ExpeditionId, command.Content, character.CharacterId!.Value);
        await kafkaEventProducer.PublishAsync(messageDto.ToMessageCreatedEvent());
        
        await turnOrderCommandHandler.ExecuteAsync(new ActionTakenCommand() { ExpeditionId = command.ExpeditionId });
        await expeditionStateCommandHandler.ExecuteAsync(new PublishUpdatedExpeditionStateCommand() { ExpeditionId = command.ExpeditionId });
        
        var action = new ExpeditionActionEvent()
        {
            ExpeditionId = command.ExpeditionId,
            ExpeditionEventSource = ExpeditionEventSource.CharacterAction,
            ExpeditionEventType = ExpeditionEventType.CharacterActionRequested
        };
        await kafkaEventProducer.PublishAsync(action);
        
        await scope.CompleteAsync();
        
        return messageDto.ToEntity();
    }
}