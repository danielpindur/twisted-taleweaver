using TwistedTaleweaver.Domain.Expeditions.Enums;
using TwistedTaleweaver.Integration.OpenAi.Expeditions;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Enums;
using TwistedTaleweaver.Integration.OpenAi.Generators;

namespace TwistedTaleweaver.Integration.OpenAi.Adapters;

public interface IGenerationAdapter
{
    Task<GeneratedExpedition> GenerateAsync(ExpeditionDifficulty difficulty, string? expeditionName, string? expeditionDescription);
}

internal class GenerationAdapter(IEntityGenerator entityGenerator) : IGenerationAdapter
{
    public async Task<GeneratedExpedition> GenerateAsync(ExpeditionDifficulty difficulty, string? expeditionName, string? expeditionDescription)
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
        
        var generationString = $"Generate expedition with difficulty:{difficulty.ToString()}";

        if (expeditionName is not null)
        {
            generationString += $", name:\"{expeditionName}\"";
        }
        else if (expeditionDescription is not null)
        {
            generationString += $", description:\"{expeditionDescription}\"";
        }
        else
        {
            var randomExpeditionSetting = Enum.GetValues<ExpeditionSetting>()[Random.Shared.Next(Enum.GetValues<ExpeditionSetting>().Length)];
            var randomExpeditionObjective = Enum.GetValues<ExpeditionObjective>()[Random.Shared.Next(Enum.GetValues<ExpeditionObjective>().Length)];
        
            generationString += $", setting:{randomExpeditionSetting.ToString()}, objective:{randomExpeditionObjective.ToString()}";
        }
        
        generationString += ". The generated Name must have less then 45 characters, the generated Description must have less then 350 characters. I will be very disappointed if you generate something longer.";

        var generatedExpedition = await entityGenerator.GenerateEntityAsync<GeneratedExpedition>(generationString);

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
}