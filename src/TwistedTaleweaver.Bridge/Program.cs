using TwistedTaleweaver.Bridge.Twitch.Clients;
using TwistedTaleweaver.Bridge.Twitch.Common.Processing;
using TwistedTaleweaver.Bridge.Twitch.Configuration;
using TwistedTaleweaver.Bridge.Twitch.Subscriptions;
using TwistedTaleweaver.Core.Common.Setup;
using TwistedTaleweaver.Core.Kafka.Setup;
using TwistedTaleweaver.Integration.Twitch.Common.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<TwitchWebsocketConfiguration>()
    .Bind(builder.Configuration.GetSection("TwitchWebsocket"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.AddTwistedTaleweaverCore();
builder.AddKafkaMessaging().WithProducers();
builder.AddTwitchApiIntegration();

builder.Services
    .AddTransient<INotificationProcessorFactory, NotificationProcessorFactory>()
    .Scan(x => x
        .FromAssemblyOf<INotificationProcessor>()
        .AddClasses(classes => classes.AssignableTo<INotificationProcessor>(), false)
        .AsSelf()
        .WithTransientLifetime()
    );

builder.Services
    .AddHostedService<TwitchEventSubWebSocketClient>()
    .AddTransient<ITwitchEventSubscriptionCreator, TwitchEventSubscriptionCreator>();


var app = builder.Build();
app.Run();
