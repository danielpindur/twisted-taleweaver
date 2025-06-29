using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Core.Kafka.Consumers;
using TwistedTaleweaver.Kafka.Api.Twitch;
using TwistedTaleweaver.Kafka.Api.Twitch.Payloads.Stream;
using TwistedTaleweaver.Stream.Facades;

namespace TwistedTaleweaver.Twitch.StreamUpdates.Consumers;

internal class TwitchStreamEndedConsumer(IServiceProvider serviceProvider) : KafkaEventConsumer<StreamEndedPayload>
{
    public override KafkaEvent Event => TwitchEvent.StreamEnded;
        
    protected override async Task HandleAsync(StreamEndedPayload payload, CancellationToken cancellationToken)
    {
        var streamService = serviceProvider.GetRequiredService<IStreamFacade>();
        await streamService.EndStream(payload.BroadcasterUserId);
    }
}
