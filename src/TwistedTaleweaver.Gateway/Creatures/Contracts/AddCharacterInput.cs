using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.Gateway.Creatures.Contracts;

[GraphQLDescription("Request to create a new character in the expedition.")]
public class AddCharacterInput
{
    [GraphQLDescription("Identifier of the expedition to create the character in.")]
    public Guid ExpeditionId { get; set; }

    [GraphQLDescription("Name of the character - if empty a name will be generated.")]
    public string? Name { get; set; }

    [GraphQLDescription("Description of the expedition to drive the narrative - if empty a description will be generated.")]
    public string? Description { get; set; }

    [GraphQLDescription("Class of the character.")]
    public CharacterClass Class { get; set; }

    [GraphQLDescription("Gender of the character.")]
    public CreatureGender Gender { get; set; }

    [GraphQLDescription("Type of a creature, that the character is.")]
    public CreatureType CreatureType { get; set; }

    [GraphQLDescription("Narrative alignment of the character.")]
    public NarrativeAlignment Alignment { get; set; }

    [GraphQLDescription("Attribute scores of the character.")]
    public required AttributeScores AttributeScores { get; set; }
}