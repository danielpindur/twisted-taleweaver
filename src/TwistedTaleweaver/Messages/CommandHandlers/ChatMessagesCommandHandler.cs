using System.Text;
using TwistedTaleweaver.Api.Messages.Commands;
using TwistedTaleweaver.Api.Messages.Entities;
using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Core.Common.Extensions;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.DataAccess.Messages.Writers;
using TwistedTaleweaver.Expeditions.CommandHandlers;
using TwistedTaleweaver.GameEngine.CommandHandlers;
using TwistedTaleweaver.GameEngine.Entities.Enums;
using TwistedTaleweaver.GameEngine.Events;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities.Enums;
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

internal class AddActionAdjudicationMessageCommand
{
    public Guid ExpeditionId { get; set; }
    
    public required GeneratedActionAdjudication ActionAdjudication { get; set; }
}

internal interface IInternalChatMessagesCommandHandler : 
    IChatMessagesCommandHandler,
    ICommandHandler<AddSystemMessageCommand, ChatMessage>,
    ICommandHandler<AddNarrativeMessageCommand, ChatMessage>,
    ICommandHandler<AddActionAdjudicationMessageCommand, ChatMessage>
{
}

internal class ChatMessagesCommandHandler(
    IMessagesWriter messagesWriter,
    IExpeditionParticipantsReader expeditionParticipantsReader,
    IKafkaEventProducer kafkaEventProducer,
    ITurnOrderCommandHandler turnOrderCommandHandler,
    IExpeditionStateCommandHandler expeditionStateCommandHandler) : IInternalChatMessagesCommandHandler
{
    public async Task<ChatMessage> ExecuteAsync(AddSystemMessageCommand command)
    {
        var messageDto = await messagesWriter.AddSystemMessageAsync(command.ExpeditionId, command.Content);

        await kafkaEventProducer.PublishAsync(messageDto.ToMessageCreatedEvent());

        return messageDto.ToEntity();
    }
    
    public async Task<ChatMessage> ExecuteAsync(AddNarrativeMessageCommand command)
    {
        var messageDto = await messagesWriter.AddNarrativeMessageAsync(command.ExpeditionId, command.Content);

        await kafkaEventProducer.PublishAsync(messageDto.ToMessageCreatedEvent());
        
        return messageDto.ToEntity();
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
            ExpeditionEventType = ExpeditionEventType.CharacterActionRequested,
            SourceMessageId = messageDto.MessageId
        };
        await kafkaEventProducer.PublishAsync(action);
        
        await scope.CompleteAsync();
        
        return messageDto.ToEntity();
    }

    public async Task<ChatMessage> ExecuteAsync(AddActionAdjudicationMessageCommand command)
    {
        var content = FormatActionAdjudicationMessage(command.ActionAdjudication);
        
        var messageDto = await messagesWriter.AddctionAdjudicationMessage(command.ExpeditionId, content);

        await kafkaEventProducer.PublishAsync(messageDto.ToMessageCreatedEvent());
        
        return messageDto.ToEntity();
    }

    private static string FormatActionAdjudicationMessage(GeneratedActionAdjudication adjudication)
    {
        var sb = new StringBuilder();

        sb.AppendLine(adjudication.IsActionPossible
            ? "**Action Possible:** ✅"
            : "**Action Possible:** ❌");

        string riskEmoji = adjudication.RiskLevel switch
        {
            ActionRiskLevel.Trivial => "🟢",
            ActionRiskLevel.Low => "🟡",
            ActionRiskLevel.Moderate => "🟠",
            ActionRiskLevel.High => "🔴",
            ActionRiskLevel.Extreme => "💀",
            _ => ""
        };
        sb.AppendLine($"**Risk Level:** {riskEmoji} {adjudication.RiskLevel.ToString().ToUpper()}");
        sb.AppendLine();

        if (adjudication.Reasoning.HasValue)
        {
            sb.AppendLine("**Reasoning:**");
            sb.AppendLine($"*{adjudication.Reasoning.ActualValue}*");
            sb.AppendLine();
        }

        if (adjudication.RequiredChecks.Count > 0)
        {
            sb.AppendLine("**Required Checks:**");
            foreach (var check in adjudication.RequiredChecks)
            {
                sb.AppendLine($"**{check.Attribute} ({check.Difficulty} - {check.NumberOfDicesToRoll}{check.DiceToRoll.ToDescription()}):** *{check.Justification}*");
            }
            sb.AppendLine();
        }

        if (adjudication.AppliedTraits.Count > 0)
        {
            sb.AppendLine("**Applied Traits:**");
            foreach (var trait in adjudication.AppliedTraits)
            {
                sb.AppendLine($"✨ **{trait.TraitName}:** {trait.EffectSummary}");
            }
            sb.AppendLine();
        }
        
        if (adjudication.PotentialConsequence.HasValue)
        {
            var consequence = adjudication.PotentialConsequence.ActualValue;
            
            string severityEmoji = consequence.Severity switch
            {
                ConsequenceSeverity.Minor => "🟡",
                ConsequenceSeverity.Moderate => "🟠",
                ConsequenceSeverity.Severe => "💀",
                _ => ""
            };

            
            sb.AppendLine("**Potential Consequence:**");
            sb.AppendLine($"**Severity:** {severityEmoji} {consequence.Severity.ToString().ToUpper()}");
            sb.AppendLine($"**Description:** *{consequence.Description}*");
            sb.AppendLine();
        }

        if (!adjudication.IsActionPossible)
        {
            sb.AppendLine("⚠️ *Choose your next action wisely… another attempt may yet succeed if you prepare carefully.*");
        }

        return sb.ToString();
    }
}