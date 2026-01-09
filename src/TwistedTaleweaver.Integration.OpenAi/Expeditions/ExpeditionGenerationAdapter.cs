using System.Text;
using TwistedTaleweaver.Api.Expeditions.Entities.Enums;
using TwistedTaleweaver.Core.Common.Extensions;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities.Enums;
using TwistedTaleweaver.Integration.OpenAi.Generators;

namespace TwistedTaleweaver.Integration.OpenAi.Expeditions;

public class ExpeditionGenerationCommand
{
    public string? ExpeditionName { get; set; }
    
    public string? ExpeditionDescription { get; set; }
    
    public ExpeditionDifficulty Difficulty { get; set; }
}

public interface IExpeditionGenerationAdapter
{
    Task<GeneratedExpedition> GenerateAsync(ExpeditionGenerationCommand command);
}

internal class ExpeditionGenerationAdapter(IEntityGenerator entityGenerator) : IExpeditionGenerationAdapter
{
    public async Task<GeneratedExpedition> GenerateAsync(ExpeditionGenerationCommand command)
    {
        var name = string.IsNullOrWhiteSpace(command.ExpeditionName) ? null : command.ExpeditionName;
        var description = string.IsNullOrWhiteSpace(command.ExpeditionDescription) ? null : command.ExpeditionDescription;

        if (name is not null && description is not null)
        {
            return new GeneratedExpedition()
            {
                Name = name,
                Description = description
            };
        }

        var sb = new StringBuilder();
        sb.Append($"Generate expedition with difficulty:{command.Difficulty.ToString()}");

        if (name is not null || description is not null)
        {
            if (name is not null)
            {
                sb.Append($", name:\"{name}\"");
            }

            if (description is not null)
            {
                sb.Append($", description:\"{description}\"");
            }
        }
        else
        {
            var randomExpeditionSetting = Enum.GetValues<ExpeditionSetting>()[Random.Shared.Next(Enum.GetValues<ExpeditionSetting>().Length)];
            var randomExpeditionObjective = Enum.GetValues<ExpeditionObjective>()[Random.Shared.Next(Enum.GetValues<ExpeditionObjective>().Length)];

            sb.Append($", setting:{randomExpeditionSetting.ToDescription()}, objective:{randomExpeditionObjective.ToDescription()}");
        }

        sb.Append(". The generated Name must have less then 45 characters, the generated Description must have less then 350 characters. I will be very disappointed if you generate something longer.");

        var generatedExpedition = await entityGenerator.GenerateEntityAsync<GeneratedExpedition>(
            sb.ToString(), 
            TwistedTaleweaverContextHelper.ExpeditionGenerationContext,
            RandomnessSettingsHelper.GenerationSettings);

        if (name is not null)
        {
            generatedExpedition.Name = name;
        }

        if (description is not null)
        {
            generatedExpedition.Description = description;
        }

        return generatedExpedition;
    }
}