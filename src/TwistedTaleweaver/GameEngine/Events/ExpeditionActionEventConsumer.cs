using Microsoft.Extensions.Logging;
using TwistedTaleweaver.Api.Expeditions.Entities.Enums;
using TwistedTaleweaver.Api.Messages.Entities.Enums;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Creatures.QueryHandlers;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
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
    IKafkaEventPublisher kafkaEventPublisher,
    ICharacterContextsQueryHandler characterContextsQueryHandler,
    IInternalChatMessagesCommandHandler chatMessagesCommandHandler,
    INarrativeGenerationAdapter narrativeGenerationAdapter,
    ITurnOrderCommandHandler turnOrderCommandHandler,
    IExpeditionStateCommandHandler expeditionStateCommandHandler,
    IActionAdjudicationGenerationAdapter actionAdjudicationGenerationAdapter,
    IMessagesReader messagesReader,
    IActionAdjudicationCommandHandler actionAdjudicationCommandHandler,
    IMonsterContextsQueryHandler monsterContextsQueryHandler,
    IExpeditionTurnOrdersReader turnOrdersReader,
    IExpeditionsReader expeditionsReader) : IKafkaEventConsumer<ExpeditionActionEvent>
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

        if (@event.ExpeditionEventType == ExpeditionEventType.ActionAdjudicationProcessed)
        {
            return ProcessActionAdjudicationProcessed(@event);
        }

        if (@event.ExpeditionEventType == ExpeditionEventType.MonsterActionRequested)
        {
            return ProcessMonsterActionRequested(@event);
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
        
        var monsterContexts = await monsterContextsQueryHandler.ExecuteAsync(
            new GetMonsterContextsByExpeditionQuery() { ExpeditionId = @event.ExpeditionId });
        
        var generatedNarrative = await narrativeGenerationAdapter.GenerateAsync(
            new NarrativeGenerationCommand()
            {
                Expedition = expeditionContext,
                PreviousNarrativeTexts = new List<string>(),
                ActionMessagesSinceLastNarration = new List<string>(),
                IsFirstNarrativeText = true,
                Characters = characterContexts,
                AliveMonsters =  monsterContexts
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

        await kafkaEventPublisher.PublishAsync(action);
        
        await scope.CompleteAsync();
    }

    private async Task ProcessNarrativeGenerated(ExpeditionActionEvent @event)
    {
        await using var scope = new UnitOfWorkScope();
        
        await turnOrderCommandHandler.ExecuteAsync(new AdvanceTurnOrderCommand() { ExpeditionId = @event.ExpeditionId });
        await expeditionStateCommandHandler.ExecuteAsync(new PublishUpdatedExpeditionStateCommand() { ExpeditionId = @event.ExpeditionId });

        var expeditionState = await expeditionsReader.GetStatusAsync(@event.ExpeditionId);
        var turnOrder  = await turnOrdersReader.GetByExpeditionAsync(@event.ExpeditionId);

        if (expeditionState.Status == ExpeditionStatus.Started && turnOrder.ActionGenerationRequired)
        {
            var action = new ExpeditionActionEvent()
            {
                ExpeditionId = @event.ExpeditionId,
                ExpeditionEventSource = ExpeditionEventSource.System,
                ExpeditionEventType = ExpeditionEventType.MonsterActionRequested
            };

            await kafkaEventPublisher.PublishAsync(action);
        }
        
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
        
        var previousNarrativeMessages = await messagesReader
            .GetContents(@event.ExpeditionId, MessageType.Narrative);

        var expeditionContext = await expeditionContextQueryHandler.ExecuteAsync(
            new ExpeditionContextQuery() { ExpeditionId = @event.ExpeditionId });
        
        var characterContexts = await characterContextsQueryHandler.ExecuteAsync(
            new GetCharacterContextsByExpeditionQuery() { ExpeditionId = @event.ExpeditionId });

        var monsterContexts = await monsterContextsQueryHandler.ExecuteAsync(
            new GetMonsterContextsByExpeditionQuery() { ExpeditionId = @event.ExpeditionId });

        var characterPerformingAction = characterContexts.Single(x => x.CreatureId == actionMessage.SenderCharacterId);
        
        var actionAdjudication = await actionAdjudicationGenerationAdapter.GenerateAsync(
            new CharacterActionAdjudicationGenerationCommand()
            {
                ActionText = actionMessage.Content,
                CharacterPerformingAction = characterPerformingAction,
                Expedition = expeditionContext,
                OtherCharacters = characterContexts.Where(x => x.CreatureId != actionMessage.SenderCharacterId).ToList(),
                PreviousNarrativeTexts = previousNarrativeMessages,
                AliveMonsters = monsterContexts
            });

        await using var scope = new UnitOfWorkScope();
        
        var message = await chatMessagesCommandHandler.ExecuteAsync(
            new AddCharacterActionAdjudicationMessageCommand()
            {
                ActionAdjudication = actionAdjudication, 
                ExpeditionId = @event.ExpeditionId
            });

        if (actionAdjudication.IsActionPossible)
        {
            var action = new ExpeditionActionEvent()
            {
                ExpeditionId = @event.ExpeditionId,
                ExpeditionEventSource = ExpeditionEventSource.System,
                ExpeditionEventType = ExpeditionEventType.AllowedActionAdjudicationGenerated,
                SourceMessageId = message.MessageId,
                ActionAdjudication = actionAdjudication.ToActionAdjudication(characterPerformingAction.CreatureId)
            };

            await kafkaEventPublisher.PublishAsync(action);
        }
        else
        {
            await turnOrderCommandHandler.ExecuteAsync(new RetakeActionCommand() { ExpeditionId = @event.ExpeditionId });
            await expeditionStateCommandHandler.ExecuteAsync(new PublishUpdatedExpeditionStateCommand() { ExpeditionId = @event.ExpeditionId });
        }
        
        await scope.CompleteAsync();
    }
    
    private Task ProcessAllowedActionAdjudicationGenerated(ExpeditionActionEvent @event)
    {
        var actionAdjudication = @event.ActionAdjudication ?? throw new InvalidOperationException("ActionAdjudication is required for AllowedActionAdjudication events.");

        return actionAdjudicationCommandHandler.ExecuteAsync(new ProcessActionAdjudicationCommand()
        {
            ExpeditionId = @event.ExpeditionId,
            ActionAdjudication = actionAdjudication,
            ActionAdjudicationMessageId = @event.SourceMessageId ??  throw new InvalidOperationException("SourceMessageId is required for ActionAdjudication events."),
        });
    }
    
    private async Task ProcessActionAdjudicationProcessed(ExpeditionActionEvent @event)
    {
        var expeditionContext = await expeditionContextQueryHandler.ExecuteAsync(
            new ExpeditionContextQuery() { ExpeditionId = @event.ExpeditionId });

        var characterContexts = await characterContextsQueryHandler.ExecuteAsync(
            new GetCharacterContextsByExpeditionQuery() { ExpeditionId = @event.ExpeditionId });
        
        var monsterContexts = await monsterContextsQueryHandler.ExecuteAsync(
            new GetMonsterContextsByExpeditionQuery() { ExpeditionId = @event.ExpeditionId });

        var previousNarrativeMessages = await messagesReader
            .GetContents(@event.ExpeditionId, MessageType.Narrative);
        
        var actionMessagesSinceLastNarration = await messagesReader.GetContentsSinceLastNarrativeAsync(@event.ExpeditionId);
        
        var generatedNarrative = await narrativeGenerationAdapter.GenerateAsync(
            new NarrativeGenerationCommand()
            {
                Expedition = expeditionContext,
                PreviousNarrativeTexts = previousNarrativeMessages,
                ActionMessagesSinceLastNarration = actionMessagesSinceLastNarration,
                Characters = characterContexts,
                AliveMonsters = monsterContexts
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
        await kafkaEventPublisher.PublishAsync(action);
        
        await scope.CompleteAsync();
    }
    
    private async Task ProcessMonsterActionRequested(ExpeditionActionEvent @event)
    {
        var previousNarrativeMessages = await messagesReader
            .GetContents(@event.ExpeditionId, MessageType.Narrative);

        var expeditionContext = await expeditionContextQueryHandler.ExecuteAsync(
            new ExpeditionContextQuery() { ExpeditionId = @event.ExpeditionId });
        
        var characterContexts = await characterContextsQueryHandler.ExecuteAsync(
            new GetCharacterContextsByExpeditionQuery() { ExpeditionId = @event.ExpeditionId });

        var monsterContexts = await monsterContextsQueryHandler.ExecuteAsync(
            new GetMonsterContextsByExpeditionQuery() { ExpeditionId = @event.ExpeditionId });
        
        var turnOrder = await turnOrdersReader.GetByExpeditionAsync(@event.ExpeditionId);

        var monsterPerformingAction = monsterContexts.Single(x => x.CreatureId == turnOrder.CurrentActorId);
        
        var actionAdjudication = await actionAdjudicationGenerationAdapter.GenerateAsync(
            new MonsterActionAdjudicationGenerationCommand()
            {
                Expedition = expeditionContext,
                PlayerCharactersInExpedition = characterContexts,
                MonsterPerformingAction = monsterPerformingAction,
                OtherAliveMonsters = monsterContexts.Where(x => x.CreatureId != monsterPerformingAction.CreatureId).ToList(),
                PreviousNarrativeTexts = previousNarrativeMessages,
            });
     
        await using var scope = new UnitOfWorkScope();

        var message = await chatMessagesCommandHandler.ExecuteAsync(
            new AddMonsterActionAdjudicationMessageCommand()
            {
                ActionAdjudication = actionAdjudication, 
                ExpeditionId = @event.ExpeditionId
            });

        var action = new ExpeditionActionEvent()
        {
            ExpeditionId = @event.ExpeditionId,
            ExpeditionEventSource = ExpeditionEventSource.System,
            ExpeditionEventType = ExpeditionEventType.AllowedActionAdjudicationGenerated,
            SourceMessageId = message.MessageId,
            ActionAdjudication = actionAdjudication.ToActionAdjudication(monsterPerformingAction.CreatureId)
        };

        await kafkaEventPublisher.PublishAsync(action);

        await scope.CompleteAsync();
    }
}