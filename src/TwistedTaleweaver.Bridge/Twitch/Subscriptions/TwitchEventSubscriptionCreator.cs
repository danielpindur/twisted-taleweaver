using TwistedTaleweaver.Bridge.Twitch.Common.Conditions;
using TwistedTaleweaver.Bridge.Twitch.Common.Extensions;
using TwistedTaleweaver.Bridge.Twitch.Common.Models.Enums;
using TwistedTaleweaver.Bridge.Twitch.Configuration;
using TwistedTaleweaver.Bridge.Twitch.Events.ChannelChatMessage.Conditions;
using TwistedTaleweaver.Bridge.Twitch.Events.StreamOffline.Conditions;
using TwistedTaleweaver.Bridge.Twitch.Events.StreamOnline.Conditions;
using TwistedTaleweaver.Core.Common.Configuration;
using TwistedTaleweaver.Integration.Twitch.Helix.Api;
using TwistedTaleweaver.Integration.Twitch.Helix.Model;
using Microsoft.Extensions.Options;

namespace TwistedTaleweaver.Bridge.Twitch.Subscriptions;

internal interface ITwitchEventSubscriptionCreator
{
    Task Create(string sessionId);
}

internal class TwitchEventSubscriptionCreator(
    IEventSubApi eventSubApi, 
    ILogger<TwitchEventSubscriptionCreator> logger, 
    IOptions<TwitchWebsocketConfiguration> twitchWebsocketConfiguration,
    IOptions<TwistedTaleweaverConfiguration> twistedTaleweaverConfiguration) : ITwitchEventSubscriptionCreator
{
    public async Task Create(string sessionId)
    {
        var botUserId = twistedTaleweaverConfiguration.Value.BotUserId;
        var broadcasters = twitchWebsocketConfiguration.Value.Broadcasters;

        foreach (var broadcaster in broadcasters)
        {
            await CreateBroadcasterSubscriptions(sessionId, broadcaster.BroadcasterId, botUserId);
        }

        logger.LogInformation("Successfully created Twitch EventSub subscriptions for session {SessionId}", sessionId);
    }

    private async Task CreateBroadcasterSubscriptions(string sessionId, string broadcasterId, string botUserId)
    {
        var subscriptionDetails = new List<SubscriptionDetail>()
        {
            new(SubscriptionType.StreamOnline, 1, new StreamOnlineCondition(broadcasterId)),
            new(SubscriptionType.StreamOffline, 1, new StreamOfflineCondition(broadcasterId)),
            new(SubscriptionType.ChannelChatMessage, 1, new ChannelChatMessageCondition(broadcasterId, botUserId))
        };

        var subscriptionTasks = subscriptionDetails.Select(x => CreateSubscription(sessionId, x)).ToList();
        await Task.WhenAll(subscriptionTasks);
    }

    private async Task CreateSubscription(string sessionId, SubscriptionDetail subscriptionDetail)
    {
        var request = new CreateEventSubSubscriptionBody(
            subscriptionDetail.SubscriptionType.ToApiType(), 
            subscriptionDetail.Version.ToString(),
            subscriptionDetail.Condition,
            GetTransport(sessionId));
            
        var response = await eventSubApi.CreateEventsubSubscriptionAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(
                $"Failed to create Twitch {subscriptionDetail.SubscriptionType.ToString()} v.{subscriptionDetail.Version} EventSub subscription");
        }
    }

    private static CreateEventSubSubscriptionBodyTransport GetTransport(string sessionId)
    {
        return new CreateEventSubSubscriptionBodyTransport(CreateEventSubSubscriptionBodyTransport.MethodEnum.Websocket)
        {
            SessionId = sessionId
        };
    }

    private class SubscriptionDetail(SubscriptionType subscriptionType, int version, ISubscriptionCondition condition)
    {
        public SubscriptionType SubscriptionType { get; } = subscriptionType;

        public int Version { get; } = version;

        public ISubscriptionCondition Condition { get; } = condition;
    }
}