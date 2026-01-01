using TwistedTaleweaver.Api.Messages.Entities;
using TwistedTaleweaver.Core.Commands;

namespace TwistedTaleweaver.Api.Messages.Commands;

public class AddCharacterMessageCommand
{
    public Guid ExpeditionId { get; set; }
    
    public Guid UserId { get; set; }
    
    public required string Content { get; set; }
}

public interface IChatMessagesCommandHandler : ICommandHandler<AddCharacterMessageCommand, ChatMessage>
{
}