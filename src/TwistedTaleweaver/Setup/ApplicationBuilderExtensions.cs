using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwistedTaleweaver.Core.Setup;
using TwistedTaleweaver.DataAccess.Setup;
using TwistedTaleweaver.Integration.Clerk.Setup;
using TwistedTaleweaver.Integration.OpenAi.Setup;

namespace TwistedTaleweaver.Setup;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection AddTwistedTaleweaver(this IHostApplicationBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        builder.AddOpenAiIntegration();
        builder.AddClerkIntegration();
        builder.AddDataAccess();

        return builder.Services
            .AddCommandHandlers(assembly)
            .AddQueryHandlers(assembly)
            .AddFluentValidation(assembly);
    }
}