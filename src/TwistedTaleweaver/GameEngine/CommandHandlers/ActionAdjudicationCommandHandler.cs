using System.Text;
using TwistedTaleweaver.Api.GameEngine.Entities.Enums;
using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Core.Common.Extensions;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Creatures.Extensions;
using TwistedTaleweaver.Creatures.Mappers;
using TwistedTaleweaver.DataAccess.Creatures.Readers;
using TwistedTaleweaver.GameEngine.Entities;
using TwistedTaleweaver.GameEngine.Entities.Enums;
using TwistedTaleweaver.GameEngine.Events;
using TwistedTaleweaver.Messages.CommandHandlers;

namespace TwistedTaleweaver.GameEngine.CommandHandlers;

internal class ProcessActionAdjudicationCommand
{
    public Guid ExpeditionId { get; set; }
    
    public required ActionAdjudication ActionAdjudication { get; set; }
}

internal interface IActionAdjudicationCommandHandler : ICommandHandler<ProcessActionAdjudicationCommand>
{
}

internal class ActionAdjudicationCommandHandler(
    ICreaturesReader creaturesReader,
    IInternalChatMessagesCommandHandler messagesCommandHandler,
    IKafkaEventProducer kafkaEventProducer) : IActionAdjudicationCommandHandler
{
    public async Task ExecuteAsync(ProcessActionAdjudicationCommand command)
    {
        var outcomeMessage = await GetOutcomeMessage(command);

        await using var scope = new UnitOfWorkScope();
        var message = await messagesCommandHandler.ExecuteAsync(new AddSystemMessageCommand() { ExpeditionId = command.ExpeditionId, Content = outcomeMessage});
        
        var action = new ExpeditionActionEvent()
        {
            ExpeditionId = command.ExpeditionId,
            ExpeditionEventSource = ExpeditionEventSource.System,
            ExpeditionEventType = ExpeditionEventType.ActionAdjudicationProcessed,
            SourceMessageId = message.MessageId
        };
        await kafkaEventProducer.PublishAsync(action);
        
        await scope.CompleteAsync();
    }

    private async Task<string> GetOutcomeMessage(ProcessActionAdjudicationCommand command)
    {
        var creature = (await creaturesReader.GetCharacterAsync(command.ActionAdjudication.CreatureId)).ToEntity();
        
        if (command.ActionAdjudication.RequiredChecks.Count == 0)
        {
            return $"{creature.Name}'s action automatically succeed due to no checks being required.";
        }
        
        var sb = new StringBuilder();
        sb.AppendLine($"{creature.Name} attempts to perform the action with the following checks:");
        sb.AppendLine();

        foreach (var requiredCheck in command.ActionAdjudication.RequiredChecks)
        {
            var attributeBonus = creature.AttributeScores.GetAttributeBonus(requiredCheck.Attribute);
            var diceRolls = RollDices(requiredCheck.DiceType, attributeBonus);
            
            var total = diceRolls.Sum() + requiredCheck.ValueToBeat;
            var outcome = GetCheckOutcome(diceRolls, total, requiredCheck);
            
            sb.AppendLine($"{requiredCheck.Attribute} {requiredCheck.DiceCount}{requiredCheck.DiceType.ToDescription()} -> {total} - {outcome.ToDescription()}");
        }

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