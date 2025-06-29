namespace TwistedTaleweaver.Bridge.Twitch.Common.Processing;

internal interface INotificationProcessor
{
    Task ProcessAsync(TwitchNotification notification);
}
