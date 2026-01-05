using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Messages.Entities;

namespace TwistedTaleweaver.GameEngine.CommandHandlers;

internal class ProcessActionAdjudicationCommand
{
    public required GeneratedActionAdjudication ActionAdjudication { get; set; }
    
    public required CharacterContext CharacterActor { get; set; }
}

internal class ActionAdjudicationProcessingResult
{
    public bool CanPerform { get; set; }
    
    public string? FailureReason { get; set; }
}

internal interface IActionAdjudicationCommandHandler : ICommandHandler<ProcessActionAdjudicationCommand, ActionAdjudicationProcessingResult>
{
}

internal class ActionAdjudicationCommandHandler : IActionAdjudicationCommandHandler
{
    public Task<ActionAdjudicationProcessingResult> ExecuteAsync(ProcessActionAdjudicationCommand command)
    {
        throw new NotImplementedException();
    }
}