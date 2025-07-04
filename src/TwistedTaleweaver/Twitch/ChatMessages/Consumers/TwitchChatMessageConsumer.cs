using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Core.Kafka.Consumers;
using TwistedTaleweaver.Expeditions.Facades;
using TwistedTaleweaver.Kafka.Api.Twitch;
using TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Chat;
using TwistedTaleweaver.Twitch.ChatMessages.Extensions;

namespace TwistedTaleweaver.Twitch.ChatMessages.Consumers;

internal class TwitchChatMessageConsumer(IServiceProvider serviceProvider) : KafkaEventConsumer<ChatMessagePayload>
{
    public override KafkaEvent Event => TwitchEvent.ChatMessage;
        
    protected override async Task HandleAsync(ChatMessagePayload payload, CancellationToken cancellationToken)
    {
        if (payload.IsExpeditionStartCommand())
        {
            await ProcessExpeditionCreationCommand(payload);
        }
        else if (payload.IsExpeditionJoinCommand())
        {
            await ProcessExpeditionJoinCommand(payload);
        }
    }

    private async Task ProcessExpeditionCreationCommand(ChatMessagePayload payload)
    {
        var expeditionFacade = serviceProvider.GetRequiredService<IExpeditionFacade>();
        await expeditionFacade.StartExpeditionAsync(payload.BroadcasterUserId, payload.ChatterUserId);
    }

    private async Task ProcessExpeditionJoinCommand(ChatMessagePayload payload)
    {
        var expeditionFacade = serviceProvider.GetRequiredService<IExpeditionFacade>();
        await expeditionFacade.JoinExpeditionAsync(payload.BroadcasterUserId, payload.ChatterUserId);
    }
}