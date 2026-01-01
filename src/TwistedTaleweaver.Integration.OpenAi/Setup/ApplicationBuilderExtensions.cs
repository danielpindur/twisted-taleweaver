using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.Responses;
using TwistedTaleweaver.Integration.OpenAi.Creatures;
using TwistedTaleweaver.Integration.OpenAi.Expeditions;
using TwistedTaleweaver.Integration.OpenAi.Generators;
using TwistedTaleweaver.Integration.OpenAi.Messages;

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
        builder.Services.AddSingleton<IExpeditionGenerationAdapter, ExpeditionGenerationAdapter>();
        builder.Services.AddSingleton<ICharacterGenerationAdapter, CharacterGenerationAdapter>();
        builder.Services.AddSingleton<INarrativeGenerationAdapter, NarrativeGenerationAdapter>();

        return builder.Services;
    }
}