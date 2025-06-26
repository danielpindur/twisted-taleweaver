using TwistedTaleweaver.Core.Logging;
using TwistedTaleweaver.Integration.Twitch.Common.Configuration;
using TwistedTaleweaver.Integration.Twitch.Helix.Client;
using TwistedTaleweaver.Integration.Twitch.Helix.Extensions;
using TwistedTaleweaver.Integration.Twitch.OAuth2.Extensions;
using TwistedTaleweaver.Integration.Twitch.OAuth2.Provider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TwistedTaleweaver.Integration.Twitch.Common.Setup;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection AddTwitchApiIntegration(this IHostApplicationBuilder builder)
    {
        var twitchConfigSection = builder.Configuration.GetSection("TwitchApi");
        
        builder.Services
            .AddOptions<TwitchApiConfiguration>()
            .Bind(twitchConfigSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        builder.Services
            .AddSingleton<OAuthTokenProvider>();

        var twitchConfig = twitchConfigSection.Get<TwitchApiConfiguration>();

        if (twitchConfig == null)
        {
            throw new InvalidOperationException("TwitchApi configuration is null.");
        }

        builder.ConfigureHelixApi((_, _, config) =>
        {
            config.AddApiHttpClients(
                client =>
                {
                    client.BaseAddress = new Uri(twitchConfig.HelixApiUrl);
                    client.DefaultRequestHeaders.Add("Client-Id", twitchConfig.ClientId);
                },
                httpClientBuilder => 
                { 
                    httpClientBuilder.AddHttpMessageHandler<LoggingHandler>(); 
                });

            config.UseProvider<OAuthTokenProvider, OAuthToken>();
        });

        builder.ConfigureOAuthApi((_, _, config) =>
        {
            config.AddApiHttpClients(
                client => 
                { 
                    client.BaseAddress = new Uri(twitchConfig.OAuthApiUrl); 
                },
                httpClientBuilder => 
                { 
                    httpClientBuilder.AddHttpMessageHandler<LoggingHandler>(); 
                });
        });

        return builder.Services;
    }
}