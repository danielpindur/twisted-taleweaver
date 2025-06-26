using TwistedTaleweaver.Integration.Twitch.OAuth2.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TwistedTaleweaver.Integration.Twitch.OAuth2.Extensions;

/// <summary>
/// Extension methods for IHostApplicationBuilder
/// </summary>
internal static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Add the OAuth API to your host application builder.
    /// </summary>
    public static IHostApplicationBuilder ConfigureOAuthApi(
        this IHostApplicationBuilder builder)
    {
        HostConfiguration config = new HostConfiguration(builder.Services);
            
        ServiceCollectionExtensions.AddApi(builder.Services, config);
            
        return builder;
    }

    /// <summary>
    /// Add the OAuth API to your host application builder.
    /// </summary>
    public static IHostApplicationBuilder ConfigureOAuthApi(
        this IHostApplicationBuilder builder, 
        Action<IHostApplicationBuilder, IServiceCollection, HostConfiguration> options)
    {
        HostConfiguration config = new HostConfiguration(builder.Services);
            
        options(builder, builder.Services, config);
            
        ServiceCollectionExtensions.AddApi(builder.Services, config);
            
        return builder;
    }
}