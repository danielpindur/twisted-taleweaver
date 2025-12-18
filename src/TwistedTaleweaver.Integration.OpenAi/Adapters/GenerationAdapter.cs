using System.Text;
using TwistedTaleweaver.Core.Common.Extensions;
using TwistedTaleweaver.Domain.Creatures;
using TwistedTaleweaver.Domain.Creatures.Enums;
using TwistedTaleweaver.Domain.Expeditions.Enums;
using TwistedTaleweaver.Integration.OpenAi.Creatures;
using TwistedTaleweaver.Integration.OpenAi.Expeditions;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Enums;
using TwistedTaleweaver.Integration.OpenAi.Generators;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TwistedTaleweaver.Integration.OpenAi.Adapters;

public interface IGenerationAdapter
{
    Task<GeneratedExpedition> GenerateExpeditionAsync(
        ExpeditionDifficulty difficulty, string? expeditionName, string? expeditionDescription);

    Task<GeneratedCharacter> GenerateCharacterAsync(
        string? characterName, 
        string? characterDescription,
        string expeditionDescription,
        ClassType classType,
        Gender gender,
        CreatureType creatureType,
        NarrativeAlignment narrativeAlignment,
        AttributeScores attributeScores);
}

internal class GenerationAdapter(IEntityGenerator entityGenerator) : IGenerationAdapter
{
    public async Task<GeneratedExpedition> GenerateExpeditionAsync(ExpeditionDifficulty difficulty, string? expeditionName, string? expeditionDescription)
    {
        var name = string.IsNullOrWhiteSpace(expeditionName) ? null : expeditionName;
        var description = string.IsNullOrWhiteSpace(expeditionDescription) ? null : expeditionDescription;

        if (name is not null && description is not null)
        {
            return new GeneratedExpedition()
            {
                Name = name,
                Description = description
            };
        }

        var sb = new StringBuilder();
        sb.Append($"Generate expedition with difficulty:{difficulty.ToString()}");

        if (expeditionName is not null || expeditionDescription is not null)
        {
            if (expeditionName is not null)
            {
                sb.Append($", name:\"{expeditionName}\"");
            }
            
            if (expeditionDescription is not null)
            {
                sb.Append($", description:\"{expeditionDescription}\"");
            }
        }
        else
        {
            var randomExpeditionSetting = Enum.GetValues<ExpeditionSetting>()[Random.Shared.Next(Enum.GetValues<ExpeditionSetting>().Length)];
            var randomExpeditionObjective = Enum.GetValues<ExpeditionObjective>()[Random.Shared.Next(Enum.GetValues<ExpeditionObjective>().Length)];
        
            sb.Append($", setting:{randomExpeditionSetting.ToDescription()}, objective:{randomExpeditionObjective.ToDescription()}");
        }
        
        sb.Append(". The generated Name must have less then 45 characters, the generated Description must have less then 350 characters. I will be very disappointed if you generate something longer.");

        var generatedExpedition = await entityGenerator.GenerateEntityAsync<GeneratedExpedition>(sb.ToString());

        if (expeditionName is not null)
        {
            generatedExpedition.Name = expeditionName;
        }

        if (expeditionDescription is not null)
        {
            generatedExpedition.Description = expeditionDescription;
        }
        
        return generatedExpedition;
    }

    public async Task<GeneratedCharacter> GenerateCharacterAsync(
        string? characterName,
        string? characterDescription,
        string expeditionDescription,
        ClassType classType,
        Gender gender,
        CreatureType creatureType,
        NarrativeAlignment narrativeAlignment,
        AttributeScores attributeScores)
    {
        var name = string.IsNullOrWhiteSpace(characterName) ? null : characterName;
        var description = string.IsNullOrWhiteSpace(characterDescription) ? null : characterDescription;

        var sb = new StringBuilder();
        sb.Append("Generate character with ");

        if (name is not null)
        {
            sb.Append($"name: \"{name}\", ");
        }

        if (description is not null)
        {
            sb.Append($"description: \"{description}\", ");
        }

        sb.Append($"class:\"{classType.ToString()}\", gender:\"{gender.ToString()}\", creatureType:\"{creatureType.ToString()}\", narrativeAlignment:\"{narrativeAlignment.ToString()}\", ");
        sb.Append($"attributeScores: {JsonSerializer.Serialize(attributeScores)} ");
        
        sb.Append($"for an expedition: \"{expeditionDescription}\"");
        sb.Append(". The generated Name must have less then 45 characters, the generated ContextKeywords must have less then 220 characters and not contain information contained in other response fields and request. I will be very disappointed if you generate something longer.");
        
        var generatedCharacter = await entityGenerator.GenerateEntityAsync<GeneratedCharacter>(sb.ToString());

        if (name is not null)
        {
            generatedCharacter.Name = name;
        }
        
        return generatedCharacter;
    }
}