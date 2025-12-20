using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TwistedTaleweaver.Hub.Expeditions.Contracts;
using TypedSignalR.Client;

namespace TwistedTaleweaver.Hub.Expeditions;

[Hub]
public interface IExpeditionHub
{
    Task SendMessage(Guid expeditionId, MessageRequest message);
}

[Receiver]
public interface IExpeditionClient
{
    Task MessageReceived(MessageReceivedEvent @event);
}

[Authorize]
public sealed class ExpeditionHub : Hub<IExpeditionClient>, IExpeditionHub
{
    public Task SendMessage(Guid expeditionId, MessageRequest message)
    {
        // TODO: Process message received from FE
        throw new NotImplementedException();
    }
}