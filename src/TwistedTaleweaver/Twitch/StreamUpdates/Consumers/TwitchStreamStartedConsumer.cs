using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Core.Kafka.Consumers;
using TwistedTaleweaver.Kafka.Api.Twitch;
using TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Stream;
using TwistedTaleweaver.Stream.Facades;

namespace TwistedTaleweaver.Twitch.StreamUpdates.Consumers;

internal class TwitchStreamStartedConsumer(IServiceProvider serviceProvider) : KafkaEventConsumer<StreamStartedPayload>
{
    public override KafkaEvent Event => TwitchEvent.StreamStarted;
        
    protected override async Task HandleAsync(StreamStartedPayload payload, CancellationToken cancellationToken)
    {
        var streamFacade = serviceProvider.GetRequiredService<IStreamFacade>();
        await streamFacade.StartStream(payload.BroadcasterUserId, payload.StreamId, payload.StartedAt);
    }
}
