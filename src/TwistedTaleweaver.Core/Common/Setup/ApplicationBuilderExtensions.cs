using TwistedTaleweaver.Core.Common.Configuration;
using TwistedTaleweaver.Core.Logging.Serilog.Setup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TwistedTaleweaver.Core.Common.Setup;

/// <summary>
/// Extension methods for setting up the TwistedTaleweaver core services in an application builder.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Registers the core TwistedTaleweaver services and configuration in the application builder.
    /// </summary>
    public static IServiceCollection AddTwistedTaleweaverCore(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddOptions<TwistedTaleweaverConfiguration>()
            .Bind(builder.Configuration.GetSection("TwistedTaleweaver"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        builder.AddTwistedTaleweaverLogging();
        
        return builder.Services;
    }
}