using Microsoft.Extensions.Logging;
using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Mappers;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Mappers;
using TwistedTaleweaver.Integration.OpenAi.Messages;
using TwistedTaleweaver.Messages.CommandHandlers;

namespace TwistedTaleweaver.GameEngine.Events;

internal class ExpeditionActionEventConsumer(
    ILogger<ExpeditionActionEventConsumer> logger,
    IExpeditionsReader expeditionsReader,
    IKafkaEventProducer kafkaEventProducer,
    IChatMessagesCommandHandler chatMessagesCommandHandler,
    INarrativeGenerationAdapter narrativeGenerationAdapter,
    ICharactersQueryHandler charactersQueryHandler,
    ICreatureTraitsQueryHandler creatureTraitsQueryHandler,
    ICombatCreatureStatesQueryHandler combatCreatureStatesQueryHandler) : IKafkaEventConsumer<ExpeditionActionEvent>
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

        logger.LogError("Unsupported expedition action event type: {EventType}", @event.ExpeditionEventType);
        return Task.CompletedTask;
    }

    private async Task ProcessExpeditionStarted(ExpeditionActionEvent @event)
    {
        var expedition = await expeditionsReader.GetByIdAsync(@event.ExpeditionId);
        
        if (expedition == null)
        {
            throw new InvalidOperationException($"Expedition with ID {@event.ExpeditionId} not found.");
        }
        
        var characters = (await charactersQueryHandler.ExecuteAsync(
            new CharactersByExpeditionIdQuery() { ExpeditionId = expedition.ExpeditionId })).ToList();
        
        var characterIds = characters.Select(c => c.CharacterId).ToList();
        
        var traitsByCharacterId = await creatureTraitsQueryHandler.ExecuteAsync(
            new CreatureTraitsQuery() { CreatureIds = characterIds });

        var characterStatesByExpeditionId = await combatCreatureStatesQueryHandler.ExecuteAsync(
            new CharactersStatesQuery() { ExpeditionIds = [expedition.ExpeditionId] });

        var characterStateById = characterStatesByExpeditionId[expedition.ExpeditionId].ToDictionary(x => x.CreatureId);

        var generatedNarrative = await narrativeGenerationAdapter.GenerateAsync(
            new NarrativeGenerationCommand()
            {
                Expedition = expedition.ToExpeditionContext(),
                PreviousNarrativeTexts = new List<string>(),
                ActionMessagesSinceLastNarration = new List<string>(),
                IsFirstNarrativeText = true,
                Characters = characters.ToCharacterContexts(traitsByCharacterId, characterStateById)
            });

        await chatMessagesCommandHandler.ExecuteAsync(
            new AddNarrativeMessageCommand()
            {
                ExpeditionId = expedition.ExpeditionId, 
                Content = generatedNarrative.Content
            });
        
        var action = new ExpeditionActionEvent()
        {
            ExpeditionId = expedition.ExpeditionId,
            ExpeditionEventSource = ExpeditionEventSource.System,
            ExpeditionEventType = ExpeditionEventType.NarrativeGenerated
        };

        await kafkaEventProducer.PublishAsync(action);
    }

    private async Task ProcessNarrativeGenerated(ExpeditionActionEvent @event)
    {
        
    }
}