using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Domain.Expeditions.Enums;

namespace TwistedTaleweaver.Api.Expeditions.Commands;

public class CreateExpeditionCommand 
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public ExpeditionDifficulty Difficulty { get; set; }
    
    public byte MaxParticipants { get; set; }
    
    public Guid UserId { get; set; }
}

public class CreateExpeditionResult
{
    public Guid ExpeditionId { get; set;}
}

public interface ICreateExpeditionCommandHandler : ICommandHandler<CreateExpeditionCommand, CreateExpeditionResult>
{
}