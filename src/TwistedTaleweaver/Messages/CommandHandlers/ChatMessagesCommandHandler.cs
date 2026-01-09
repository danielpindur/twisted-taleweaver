using TwistedTaleweaver.Api.Messages.Commands;
using TwistedTaleweaver.Api.Messages.Entities;
using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.DataAccess.Messages.Writers;
using TwistedTaleweaver.Expeditions.CommandHandlers;
using TwistedTaleweaver.GameEngine.CommandHandlers;
using TwistedTaleweaver.GameEngine.Entities.Enums;
using TwistedTaleweaver.GameEngine.Events;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;
using TwistedTaleweaver.Messages.Helpers;
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

internal class AddCharacterActionAdjudicationMessageCommand
{
    public Guid ExpeditionId { get; set; }
    
    public required GeneratedCharacterActionAdjudication ActionAdjudication { get; set; }
}

internal class AddMonsterActionAdjudicationMessageCommand
{
    public Guid ExpeditionId { get; set; }
    
    public required GeneratedMonsterActionAdjudication ActionAdjudication { get; set; }
}

internal interface IInternalChatMessagesCommandHandler : 
    IChatMessagesCommandHandler,
    ICommandHandler<AddSystemMessageCommand, ChatMessage>,
    ICommandHandler<AddNarrativeMessageCommand, ChatMessage>,
    ICommandHandler<AddCharacterActionAdjudicationMessageCommand, ChatMessage>,
    ICommandHandler<AddMonsterActionAdjudicationMessageCommand, ChatMessage>
{
}

internal class ChatMessagesCommandHandler(
    IMessagesWriter messagesWriter,
    IExpeditionParticipantsReader expeditionParticipantsReader,
    IKafkaEventPublisher kafkaEventPublisher,
    ITurnOrderCommandHandler turnOrderCommandHandler,
    IExpeditionStateCommandHandler expeditionStateCommandHandler) : IInternalChatMessagesCommandHandler
{
    public async Task<ChatMessage> ExecuteAsync(AddSystemMessageCommand command)
    {
        var messageDto = await messagesWriter.AddSystemMessageAsync(command.ExpeditionId, command.Content);

        await kafkaEventPublisher.PublishAsync(messageDto.ToMessageCreatedEvent());

        return messageDto.ToEntity();
    }
    
    public async Task<ChatMessage> ExecuteAsync(AddNarrativeMessageCommand command)
    {
        var messageDto = await messagesWriter.AddNarrativeMessageAsync(command.ExpeditionId, command.Content);

        await kafkaEventPublisher.PublishAsync(messageDto.ToMessageCreatedEvent());
        
        return messageDto.ToEntity();
    }

    public async Task<ChatMessage> ExecuteAsync(AddCharacterMessageCommand command)
    {
        // TODO: Validations
        var character = await expeditionParticipantsReader.GetForExpeditionByUserAsync(command.ExpeditionId, command.UserId);

        await using var scope = new UnitOfWorkScope();
        
        var messageDto = await messagesWriter.AddCharacterMessageAsync(command.ExpeditionId, command.Content, character.CharacterId!.Value);
        await kafkaEventPublisher.PublishAsync(messageDto.ToMessageCreatedEvent());
        
        await turnOrderCommandHandler.ExecuteAsync(new ActionTakenCommand() { ExpeditionId = command.ExpeditionId });
        await expeditionStateCommandHandler.ExecuteAsync(new PublishUpdatedExpeditionStateCommand() { ExpeditionId = command.ExpeditionId });
        
        var action = new ExpeditionActionEvent()
        {
            ExpeditionId = command.ExpeditionId,
            ExpeditionEventSource = ExpeditionEventSource.CharacterAction,
            ExpeditionEventType = ExpeditionEventType.CharacterActionRequested,
            SourceMessageId = messageDto.MessageId
        };
        await kafkaEventPublisher.PublishAsync(action);
        
        await scope.CompleteAsync();
        
        return messageDto.ToEntity();
    }

    public async Task<ChatMessage> ExecuteAsync(AddCharacterActionAdjudicationMessageCommand command)
    {
        var content = ActionAdjudicationMessageHelper.FormatActionAdjudicationMessage(command.ActionAdjudication);
        
        var messageDto = await messagesWriter.AddctionAdjudicationMessage(command.ExpeditionId, content);

        await kafkaEventPublisher.PublishAsync(messageDto.ToMessageCreatedEvent());
        
        return messageDto.ToEntity();
    }
    
    public async Task<ChatMessage> ExecuteAsync(AddMonsterActionAdjudicationMessageCommand command)
    {
        var content = ActionAdjudicationMessageHelper.FormatActionAdjudicationMessage(command.ActionAdjudication);
        
        var messageDto = await messagesWriter.AddctionAdjudicationMessage(command.ExpeditionId, content);

        await kafkaEventPublisher.PublishAsync(messageDto.ToMessageCreatedEvent());
        
        return messageDto.ToEntity();
    }

    
}