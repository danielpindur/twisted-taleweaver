using System.Text;
using System.Text.Json;
using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Api.Creatures.Entities.Enums;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;
using TwistedTaleweaver.Integration.OpenAi.Generators;

namespace TwistedTaleweaver.Integration.OpenAi.Creatures;

public class CharacterGenerationCommand
{
    public string? CharacterName { get; set; }
    
    public string? CharacterDescription { get; set; }
    
    public required string ExpeditionDescription { get; set; }
        
    public CharacterClass CharacterClass { get; set; }
    
    public CreatureGender Gender { get; set; }
        
    public CreatureType CreatureType { get; set; }
    
    public NarrativeAlignment NarrativeAlignment { get; set; }
        
    public required AttributeScores AttributeScores { get; set; }
}

public interface ICharacterGenerationAdapter
{
    Task<GeneratedCharacter> GenerateAsync(CharacterGenerationCommand command);
}

internal class CharacterGenerationAdapter(IEntityGenerator entityGenerator) : ICharacterGenerationAdapter
{
    public async Task<GeneratedCharacter> GenerateAsync(CharacterGenerationCommand command)
    {
        var name = string.IsNullOrWhiteSpace(command.CharacterName) ? null : command.CharacterName;
        var description = string.IsNullOrWhiteSpace(command.CharacterDescription) ? null : command.CharacterDescription;

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

        sb.Append(
            $"class:\"{command.CharacterClass.ToString()}\", gender:\"{command.Gender.ToString()}\", creatureType:\"{command.CreatureType.ToString()}\", narrativeAlignment:\"{command.NarrativeAlignment.ToString()}\", ");
        sb.Append($"attributeScores: {JsonSerializer.Serialize(command.AttributeScores)} ");

        sb.Append($"for an expedition: \"{command.ExpeditionDescription}\"");
        sb.Append(
            ". The generated Name must have less then 45 characters, the generated Description must have less then 350 characters and not contain information contained in other response fields and request. I will be very disappointed if you generate something longer.");

        var generatedCharacter = await entityGenerator.GenerateEntityAsync<GeneratedCharacter>(
            sb.ToString(), 
            TwistedTaleweaverContextHelper.CharacterGenerationContext,
            RandomnessSettingsHelper.GenerationSettings);

        if (name is not null)
        {
            generatedCharacter.Name = name;
        }

        if (description is not null)
        {
            generatedCharacter.Description = description;
        }

        return generatedCharacter;
    }
}