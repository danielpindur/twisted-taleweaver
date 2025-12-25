using TwistedTaleweaver.Core.Commands;

namespace TwistedTaleweaver.Api.Expeditions.Commands;

public class BeginExpeditionCommand
{
    public Guid ExpeditionId { get; set; }

    public Guid UserId { get; set; }
}

public interface IBeginExpeditionCommandHandler : ICommandHandler<BeginExpeditionCommand>
{
}