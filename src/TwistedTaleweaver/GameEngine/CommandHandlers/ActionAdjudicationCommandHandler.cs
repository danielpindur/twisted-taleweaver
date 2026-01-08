using System.Text;
using TwistedTaleweaver.Api.GameEngine.Entities;
using TwistedTaleweaver.Api.GameEngine.Entities.Enums;
using TwistedTaleweaver.Api.Messages.Entities.Enums;
using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Core.Common.Extensions;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Creatures.Extensions;
using TwistedTaleweaver.Creatures.QueryHandlers;
using TwistedTaleweaver.DataAccess.Messages.Readers;
using TwistedTaleweaver.Expeditions.CommandHandlers;
using TwistedTaleweaver.Expeditions.QueryHandlers;
using TwistedTaleweaver.GameEngine.Entities;
using TwistedTaleweaver.GameEngine.Entities.Enums;
using TwistedTaleweaver.GameEngine.Events;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Messages;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;
using TwistedTaleweaver.Messages.CommandHandlers;
using CreatureStateUpdate = TwistedTaleweaver.Api.Creatures.Entities.CreatureStateUpdate;
using RequiredCheck = TwistedTaleweaver.Api.GameEngine.Entities.RequiredCheck;

namespace TwistedTaleweaver.GameEngine.CommandHandlers;

internal class ProcessActionAdjudicationCommand
{
    public Guid ExpeditionId { get; set; }
    
    public long ActionAdjudicationMessageId { get; set; }
    
    public required ActionAdjudication ActionAdjudication { get; set; }
}

internal interface IActionAdjudicationCommandHandler : ICommandHandler<ProcessActionAdjudicationCommand>
{
}

internal class ActionAdjudicationCommandHandler(
    IExpeditionContextQueryHandler expeditionContextQueryHandler,
    ICharacterContextsQueryHandler characterContextsQueryHandler,
    IActionOutcomeGenerationAdapter actionOutcomeGenerationAdapter,
    IInternalChatMessagesCommandHandler messagesCommandHandler,
    IUpdateCreaturesStateCommandHandler updateCreaturesStateCommandHandler,
    IMessagesReader messagesReader,
    IKafkaEventPublisher kafkaEventPublisher) : IActionAdjudicationCommandHandler
{
    public async Task ExecuteAsync(ProcessActionAdjudicationCommand command)
    {
        var characterContexts = await characterContextsQueryHandler.ExecuteAsync(
            new GetCharacterContextsByExpeditionQuery() { ExpeditionId = command.ExpeditionId });

        var actorCharacterContext = characterContexts.Single(x => x.CreatureId == command.ActionAdjudication.CreatureId);
        
        var checkResults = GetCheckResults(command, actorCharacterContext);
        
        var expeditionContext = await expeditionContextQueryHandler.ExecuteAsync(
            new ExpeditionContextQuery() { ExpeditionId = command.ExpeditionId });
        
        var adjudicationMessage = await messagesReader.GetAsync(command.ExpeditionId, command.ActionAdjudicationMessageId);
        var previousNarrativeTexts = await messagesReader.GetContents(command.ExpeditionId, MessageType.Narrative, adjudicationMessage.MessageId);
        
        var actionOutcome = await actionOutcomeGenerationAdapter.GenerateAsync(
            new ActionOutcomeGenerationCommand()
            {
                ActionAdjudicationMessage = adjudicationMessage.Content,
                CharacterPerformingAction = actorCharacterContext,
                OtherCharacters = characterContexts.Where(x => x.CreatureId != command.ActionAdjudication.CreatureId).ToList(),
                Expedition = expeditionContext,
                PreviousNarrativeTexts = previousNarrativeTexts,
                CheckResults = checkResults
            });
        
        var outcomeMessage = GetOutcomeMessage(command, actorCharacterContext, actionOutcome);

        await using var scope = new UnitOfWorkScope();
        
        var message = await messagesCommandHandler.ExecuteAsync(new AddSystemMessageCommand() { ExpeditionId = command.ExpeditionId, Content = outcomeMessage});
        await UpdateCreaturesState(actionOutcome, characterContexts);
        
        var action = new ExpeditionActionEvent()
        {
            ExpeditionId = command.ExpeditionId,
            ExpeditionEventSource = ExpeditionEventSource.System,
            ExpeditionEventType = ExpeditionEventType.ActionAdjudicationProcessed,
            SourceMessageId = message.MessageId
        };
        await kafkaEventPublisher.PublishAsync(action);
        
        await scope.CompleteAsync();
    }

    private static List<CheckResult> GetCheckResults(ProcessActionAdjudicationCommand command, CharacterContext characterContext)
    {
        var results = new List<CheckResult>();
        
        if (command.ActionAdjudication.RequiredChecks.Count == 0)
        {
            return results;
        }

        foreach (var requiredCheck in command.ActionAdjudication.RequiredChecks)
        {
            var attributeBonus = characterContext.AttributeScores.GetAttributeBonus(requiredCheck.Attribute);
            var diceRolls = RollDices(requiredCheck.DiceType, attributeBonus);
            
            var total = diceRolls.Sum() + requiredCheck.ValueToBeat;
            var outcome = GetCheckOutcome(diceRolls, total, requiredCheck);
            
            results.Add(new CheckResult()
            {
                Check = requiredCheck,
                TotalRolledValue = total,
                Outcome = outcome,
            });
        }

        return results;
    }

    private Task UpdateCreaturesState(
        GeneratedActionOutcome actionOutcome,
        List<CharacterContext> characterContexts)
    {
        if (!actionOutcome.PlayerControlledCharacterUpdates.HasValue ||
            actionOutcome.PlayerControlledCharacterUpdates.ActualValue.Count == 0)
        {
            return Task.CompletedTask;
        }

        var contextByCreatureId = characterContexts.ToDictionary(x => x.CreatureId);

        return updateCreaturesStateCommandHandler.ExecuteAsync(
            new UpdateCreaturesStateCommand()
            {
                StateUpdates = actionOutcome.PlayerControlledCharacterUpdates.ActualValue.Select(x =>
                    new CreatureStateUpdate()
                    {
                        CreatureId = Guid.Parse(x.CreatureId),
                        HealthUpdate = x.HealthChange.HasValue ? x.HealthChange.ActualValue : null,
                        Status = x.CreatureStatusChange.HasValue
                            ? x.CreatureStatusChange.ActualValue
                            : contextByCreatureId[Guid.Parse(x.CreatureId)].Status
                    }).ToList()
            });
    }

    private static string GetOutcomeMessage(
        ProcessActionAdjudicationCommand command,
        CharacterContext characterContext, 
        GeneratedActionOutcome actionOutcome)
    {
        if (command.ActionAdjudication.RequiredChecks.Count == 0)
        {
            return $"{characterContext.Name}'s action automatically succeed due to no checks being required.\n\n{actionOutcome.ActionOutcomeShortDescription}";
        }
        
        var sb = new StringBuilder();
        sb.AppendLine($"{characterContext.Name} attempts to perform the action with the following checks:");
        sb.AppendLine();

        foreach (var requiredCheck in command.ActionAdjudication.RequiredChecks)
        {
            var attributeBonus = characterContext.AttributeScores.GetAttributeBonus(requiredCheck.Attribute);
            var diceRolls = RollDices(requiredCheck.DiceType, attributeBonus);
            
            var total = diceRolls.Sum() + requiredCheck.ValueToBeat;
            var outcome = GetCheckOutcome(diceRolls, total, requiredCheck);
            
            sb.AppendLine($"{requiredCheck.Attribute} {requiredCheck.DiceCount}{requiredCheck.DiceType.ToDescription()} -> {total} - {outcome.ToDescription()}");
        }

        sb.AppendLine();
        sb.AppendLine(actionOutcome.ActionOutcomeShortDescription);

        return sb.ToString();
    }

    private static CheckOutcome GetCheckOutcome(List<int> diceRolls, int total, RequiredCheck requiredCheck)
    {
        var margin = total - requiredCheck.ValueToBeat;
            
        var allRollsMax = diceRolls.All(d => d == (int)requiredCheck.DiceType);
        var allRollsMIn = diceRolls.All(d => d == 1);
        
        if (allRollsMax || margin >= 5)
        {
            return CheckOutcome.CriticalSuccess;
        }

        if (allRollsMIn || margin <= -5)
        {
            return CheckOutcome.CriticalFailure;
        }

        if (total >= requiredCheck.ValueToBeat)
        {
            return CheckOutcome.Success;
        }

        return CheckOutcome.Failure;
    }

    private static int RollDice(DiceType diceType)
    {
        return Random.Shared.Next(1, (int)diceType + 1);
    }

    private static List<int> RollDices(DiceType diceType, int count)
    {
        var rollResults = new List<int>();

        for (var i = 0; i < count; i++)
        {
            rollResults.Add(RollDice(diceType));
        }
        
        return rollResults;
    }
}