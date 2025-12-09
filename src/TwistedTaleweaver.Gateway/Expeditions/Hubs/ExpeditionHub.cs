using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TwistedTaleweaver.Gateway.Messages.Contracts;
using TwistedTaleweaver.Gateway.Users.Contracts;
using TypedSignalR.Client;

namespace TwistedTaleweaver.Gateway.Messages.Hubs;

[Hub]
public interface IExpeditionHub
{
    Task CreateMessage(MessageRequest message);
}

[Receiver]
public interface IMessagesClient
{
    Task MessageCreated(Message message);
}

[Authorize]
public sealed class ExpeditionHub : Hub<IMessagesClient>, IExpeditionHub
{
    public async Task CreateMessage(MessageRequest message)
    {
        await Clients.All.MessageCreated(new Message()
        {
            User = new User() { Name = "name" },
            Text = message.Text
        });
    }
}