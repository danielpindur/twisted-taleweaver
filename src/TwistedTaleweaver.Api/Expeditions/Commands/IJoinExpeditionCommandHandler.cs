using TwistedTaleweaver.Core.Commands;

namespace TwistedTaleweaver.Api.Expeditions.Commands;

public class JoinExpeditionCommand
{
    public Guid ExpeditionId { get; set; }

    public Guid UserId { get; set; }
}

public interface IJoinExpeditionCommandHandler : ICommandHandler<JoinExpeditionCommand>
{
}