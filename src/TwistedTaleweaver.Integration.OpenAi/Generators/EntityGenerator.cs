using Newtonsoft.Json;
using NJsonSchema;
using NJsonSchema.Generation;
using OpenAI.Responses;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TwistedTaleweaver.Integration.OpenAi.Generators;

internal interface IEntityGenerator
{
    Task<TEntity> GenerateEntityAsync<TEntity>(string prompt);
}

internal class EntityGenerator(OpenAIResponseClient responseClient) : IEntityGenerator
{
    public async Task<TEntity> GenerateEntityAsync<TEntity>(string prompt)
    {
        var settings = new SystemTextJsonSchemaGeneratorSettings()
        {
            SchemaType = SchemaType.Swagger2,
            AlwaysAllowAdditionalObjectProperties = false,
            GenerateEnumMappingDescription = false,
            FlattenInheritanceHierarchy = true
        };
        
        var schema = JsonSchema.FromType<TEntity>(settings);

        var options = new ResponseCreationOptions()
        {
            TextOptions = new ResponseTextOptions()
            {
                TextFormat = ResponseTextFormat.CreateJsonSchemaFormat(
                    schema.Title ?? typeof(TEntity).Name,
                    BinaryData.FromString(schema.ToJson(Formatting.None)),
                    jsonSchemaIsStrict: true)
            },
            Temperature = 0.7f,
            TopP = 0.8f
        };

        var responseItems = new List<ResponseItem>
        {
            ResponseItem.CreateUserMessageItem(prompt),
            ResponseItem.CreateSystemMessageItem(TwistedTaleweaverContextHelper.ExpeditionGenerationContext)
        };
        
        var response = await responseClient.CreateResponseAsync(responseItems, options);

        if (response.Value is null || response.Value.Error is not null)
        {
            throw new InvalidOperationException(
                $"Failed to generate {typeof(TEntity).Name}: {response.Value?.Error?.Message ?? "Unknown error"}");
        }

        var entity = JsonSerializer.Deserialize<TEntity>(response.Value.GetOutputText());
        
        if (entity is null)
        {
            throw new InvalidOperationException($"Failed to deserialize generated {typeof(TEntity).Name}.");
        }
        
        return entity;
    }
}