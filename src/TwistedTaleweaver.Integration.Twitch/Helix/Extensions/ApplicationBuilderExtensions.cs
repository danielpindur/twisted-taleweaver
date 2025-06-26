using TwistedTaleweaver.Integration.Twitch.Helix.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TwistedTaleweaver.Integration.Twitch.Helix.Extensions;

/// <summary>
/// Extension methods for IHostApplicationBuilder
/// </summary>
internal static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Add the Helix API to your host application builder.
    /// </summary>
    public static IHostApplicationBuilder ConfigureHelixApi(
        this IHostApplicationBuilder builder, 
        Action<IHostApplicationBuilder, IServiceCollection, HostConfiguration> options)
    {
        ArgumentNullException.ThrowIfNull(options);  
        
        HostConfiguration config = new HostConfiguration(builder.Services);
            
        options(builder, builder.Services, config);
            
        ServiceCollectionExtensions.AddApi(builder.Services, config);
            
        return builder;
    }
}