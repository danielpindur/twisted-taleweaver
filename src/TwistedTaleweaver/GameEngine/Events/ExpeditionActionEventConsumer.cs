using Microsoft.Extensions.Logging;
using TwistedTaleweaver.Api.Messages.Entities.Enums;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Creatures.QueryHandlers;
using TwistedTaleweaver.DataAccess.Messages.Readers;
using TwistedTaleweaver.Expeditions.CommandHandlers;
using TwistedTaleweaver.Expeditions.QueryHandlers;
using TwistedTaleweaver.GameEngine.CommandHandlers;
using TwistedTaleweaver.GameEngine.Entities.Enums;
using TwistedTaleweaver.GameEngine.Mappers;
using TwistedTaleweaver.Integration.OpenAi.Messages;
using TwistedTaleweaver.Messages.CommandHandlers;

namespace TwistedTaleweaver.GameEngine.Events;

internal class ExpeditionActionEventConsumer(
    ILogger<ExpeditionActionEventConsumer> logger,
    IExpeditionContextQueryHandler expeditionContextQueryHandler,
    IKafkaEventProducer kafkaEventProducer,
    ICharacterContextsQueryHandler characterContextsQueryHandler,
    IInternalChatMessagesCommandHandler chatMessagesCommandHandler,
    INarrativeGenerationAdapter narrativeGenerationAdapter,
    ITurnOrderCommandHandler turnOrderCommandHandler,
    IExpeditionStateCommandHandler expeditionStateCommandHandler,
    IActionAdjudicationGenerationAdapter actionAdjudicationGenerationAdapter,
    IMessagesReader messagesReader) : IKafkaEventConsumer<ExpeditionActionEvent>
{
    public Task ConsumeAsync(ExpeditionActionEvent @event, CancellationToken cancellationToken)
    {
        if (@event.ExpeditionEventType == ExpeditionEventType.ExpeditionStarted)
        {
            return ProcessExpeditionStarted(@event);
        }

        if (@event.ExpeditionEventType == ExpeditionEventType.NarrativeGenerated)
        {
            return ProcessNarrativeGenerated(@event);
        }

        if (@event.ExpeditionEventType == ExpeditionEventType.CharacterActionRequested)
        {
            return ProcessCharacterActionRequested(@event);
        }

        if (@event.ExpeditionEventType == ExpeditionEventType.AllowedActionAdjudicationGenerated)
        {
            return ProcessAllowedActionAdjudicationGenerated(@event);
        }

        logger.LogError("Unsupported expedition action event type: {EventType}", @event.ExpeditionEventType);
        return Task.CompletedTask;
    }

    private async Task ProcessExpeditionStarted(ExpeditionActionEvent @event)
    {
        var expeditionContext = await expeditionContextQueryHandler.ExecuteAsync(
            new ExpeditionContextQuery() { ExpeditionId = @event.ExpeditionId });

        var characterContexts = await characterContextsQueryHandler.ExecuteAsync(
            new GetCharacterContextsByExpeditionQuery() { ExpeditionId = @event.ExpeditionId });
        
        var generatedNarrative = await narrativeGenerationAdapter.GenerateAsync(
            new NarrativeGenerationCommand()
            {
                Expedition = expeditionContext,
                PreviousNarrativeTexts = new List<string>(),
                ActionMessagesSinceLastNarration = new List<string>(),
                IsFirstNarrativeText = true,
                Characters = characterContexts
            });

        await using var scope = new UnitOfWorkScope();
        
        var message = await chatMessagesCommandHandler.ExecuteAsync(
            new AddNarrativeMessageCommand()
            {
                ExpeditionId = @event.ExpeditionId, 
                Content = generatedNarrative.Content
            });
        
        var action = new ExpeditionActionEvent()
        {
            ExpeditionId = @event.ExpeditionId,
            ExpeditionEventSource = ExpeditionEventSource.System,
            ExpeditionEventType = ExpeditionEventType.NarrativeGenerated,
            SourceMessageId = message.MessageId
        };

        await kafkaEventProducer.PublishAsync(action);
        
        await scope.CompleteAsync();
    }

    private async Task ProcessNarrativeGenerated(ExpeditionActionEvent @event)
    {
        await using var scope = new UnitOfWorkScope();
        
        await turnOrderCommandHandler.ExecuteAsync(new AdvanceTurnOrderCommand() { ExpeditionId = @event.ExpeditionId });
        await expeditionStateCommandHandler.ExecuteAsync(new PublishUpdatedExpeditionStateCommand() { ExpeditionId = @event.ExpeditionId });

        await scope.CompleteAsync();
    }
    
    private async Task ProcessCharacterActionRequested(ExpeditionActionEvent @event)
    {
        var actionMessageId = @event.SourceMessageId ?? throw new InvalidOperationException("SourceMessageId is required for CharacterActionRequested events.");
        
        var actionMessage = await messagesReader.GetAsync(@event.ExpeditionId, actionMessageId);

        if (actionMessage.Type != MessageType.Action || actionMessage.SenderCharacterId is null)
        {
            throw new InvalidOperationException($"Message with ID {actionMessageId} is not a valid action message.");
        }
        
        var previousNarrativeMessages = await messagesReader.GetContents(
            @event.ExpeditionId, 
            MessageType.Narrative,
            actionMessageId);

        var expeditionContext = await expeditionContextQueryHandler.ExecuteAsync(
            new ExpeditionContextQuery() { ExpeditionId = @event.ExpeditionId });
        
        var characterContexts = await characterContextsQueryHandler.ExecuteAsync(
            new GetCharacterContextsByExpeditionQuery() { ExpeditionId = @event.ExpeditionId });

        var actionAdjudication = await actionAdjudicationGenerationAdapter.GenerateAsync(
            new ActionAdjudicationGenerationCommand()
            {
                ActionText = actionMessage.Content,
                CharacterPerformingAction = characterContexts.Single(x => x.CreatureId == actionMessage.SenderCharacterId),
                Expedition = expeditionContext,
                OtherCharacters = characterContexts.Where(x => x.CreatureId != actionMessage.SenderCharacterId).ToList(),
                PreviousNarrativeTexts = previousNarrativeMessages
            });

        await using var scope = new UnitOfWorkScope();
        
        var message = await chatMessagesCommandHandler.ExecuteAsync(
            new AddActionAdjudicationMessageCommand()
            {
                ActionAdjudication = actionAdjudication, 
                ExpeditionId = @event.ExpeditionId
            });

        await turnOrderCommandHandler.ExecuteAsync(new RetakeActionCommand() { ExpeditionId = @event.ExpeditionId });
        
        var action = new ExpeditionActionEvent()
        {
            ExpeditionId = @event.ExpeditionId,
            ExpeditionEventSource = ExpeditionEventSource.System,
            ExpeditionEventType = ExpeditionEventType.AllowedActionAdjudicationGenerated,
            SourceMessageId = message.MessageId,
            ActionAdjudication = actionAdjudication.ToActionAdjudication()
        };

        await kafkaEventProducer.PublishAsync(action);
        
        await scope.CompleteAsync();
    }
    
    private Task ProcessAllowedActionAdjudicationGenerated(ExpeditionActionEvent @event)
    {
        return Task.CompletedTask;
    }
}