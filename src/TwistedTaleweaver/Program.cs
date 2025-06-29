using TwistedTaleweaver.Core.Common.Setup;
using TwistedTaleweaver.Core.Kafka.Setup;
using TwistedTaleweaver.DataAccess.Setup;
using TwistedTaleweaver.Integration.Twitch.Common.Setup;
using TwistedTaleweaver.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.AddTwistedTaleweaverCore();
builder.AddKafkaMessaging().WithConsumers();
builder.AddDataAccess();
builder.AddTwistedTaleweaver().WithHostedServices();
builder.AddTwitchApiIntegration();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis")
                            ?? throw new InvalidOperationException("Redis connection string is not configured.");
});


var app = builder.Build();
app.Run();
