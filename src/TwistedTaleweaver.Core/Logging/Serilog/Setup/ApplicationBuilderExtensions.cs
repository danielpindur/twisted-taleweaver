using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;

namespace TwistedTaleweaver.Core.Logging.Serilog.Setup;

/// <summary>
/// Extension methods for configuring Serilog logging in the application builder.
/// </summary>
internal static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configures logging for Axiom format using Serilog
    /// </summary>
    public static IServiceCollection AddTwistedTaleweaverLogging(this IHostApplicationBuilder builder)
    {
        // TODO: We should look at adding Serilog.Enrichers SensitiveDataMasking to not log confidential stuff
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console(new JsonFormatter())
            .Destructure.With<JsonElementDestructuringPolicy>()
            .CreateLogger();

        builder.Logging
            .ClearProviders()
            .AddSerilog();
        
        builder.Services.AddTransient<LoggingHandler>();
        
        return builder.Services;
    }
}