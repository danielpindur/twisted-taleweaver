using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.Gateway.Creatures.Contracts;

[GraphQLDescription("Represents an user's character within the system.")]
public class Character
{
    [GraphQLDescription("Unique identifier of the character.")]
    public Guid Id { get; set; }
    
    [GraphQLDescription("The name of the character.")]
    public required string Name { get; set; }
    
    [GraphQLDescription("Gender of the character.")]
    public CreatureGender Gender { get; set; }

    [GraphQLDescription("Type of a creature, that the character is.")]
    public CreatureType CreatureType { get; set; }

    [GraphQLDescription("Narrative alignment of the character.")]
    public NarrativeAlignment Alignment { get; set; }

    [GraphQLDescription("Class of the character.")]    
    public CharacterClass Class { get; set; }
    
    [GraphQLDescription("Maximum health points of the character.")]
    public int MaxHealth { get; set; }
    
    [GraphQLDescription("Armor class of the character.")]
    public int ArmorClass { get; set; }
    
    [GraphQLDescription("Speed scores of the character.")]
    public required SpeedScores SpeedScores { get; set; }
    
    [GraphQLDescription("Attribute scores of the character.")]
    public required AttributeScores AttributeScores { get; set; }
}