using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TwistedTaleweaver.Gateway.Expeditions.Contracts;
using TwistedTaleweaver.Gateway.Messages.Contracts;
using TwistedTaleweaver.Gateway.Users.Contracts;
using TypedSignalR.Client;

namespace TwistedTaleweaver.Gateway.Expeditions.Hubs;

[Hub]
public interface IExpeditionHub
{
    Task SendMessage(MessageRequest message);
}

[Receiver]
public interface IExpeditionClient
{
    Task ParticipantsUpdated(List<ExpeditionParticipant> participants);
    
    Task OnMessageSent(Message message);
}

[Authorize]
public sealed class ExpeditionHub : Hub<IExpeditionClient>, IExpeditionHub
{
    public async Task SendMessage(MessageRequest message)
    {
        await Clients.All.OnMessageSent(new Message()
        {
            User = new User() { Name = "name" },
            Text = message.Text
        });
    }
}