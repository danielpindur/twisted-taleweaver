using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.Responses;
using TwistedTaleweaver.Integration.OpenAi.Adapters;
using TwistedTaleweaver.Integration.OpenAi.Generators;

namespace TwistedTaleweaver.Integration.OpenAi.Setup;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection AddOpenAiIntegration(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<OpenAIResponseClient>(_ =>
        {
            var apiKey = builder.Configuration["OpenAi:ApiKey"];
            var model = builder.Configuration["OpenAi:Model"];

            return new OpenAIResponseClient(model, apiKey);
        });

        builder.Services.AddSingleton<IEntityGenerator, EntityGenerator>();
        builder.Services.AddSingleton<IGenerationAdapter, GenerationAdapter>();

        return builder.Services;
    }
}