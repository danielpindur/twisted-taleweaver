using TwistedTaleweaver.Api.Creatures.Entities.Enums;

namespace TwistedTaleweaver.Gateway.Creatures.Contracts;

[GraphQLDescription("Represents a monster the players have to face.")]
public class Monster
{
    [GraphQLDescription("Unique identifier of the monster.")]
    public Guid Id { get; set; }
    
    [GraphQLDescription("The name of the monster.")]
    public required string Name { get; set; }

    [GraphQLDescription("Description of the monster (e.g., backstory, lore).")]
    public required string Description { get; set; }
    
    [GraphQLDescription("Gender of the monster.")]
    public CreatureGender Gender { get; set; }

    [GraphQLDescription("Type of a creature, that the monster is.")]
    public CreatureType CreatureType { get; set; }

    [GraphQLDescription("Narrative alignment of the monster.")]
    public NarrativeAlignment Alignment { get; set; }

    [GraphQLDescription("Class of the monster.")]    
    public int ChallengeRating { get; set; }
    
    [GraphQLDescription("Maximum health points of the monster.")]
    public int MaxHealth { get; set; }
    
    [GraphQLDescription("Armor class of the monster.")]
    public int ArmorClass { get; set; }
    
    [GraphQLDescription("Speed scores of the monster.")]
    public required SpeedScores SpeedScores { get; set; }
    
    [GraphQLDescription("Attribute scores of the monster.")]
    public required AttributeScores AttributeScores { get; set; }
}