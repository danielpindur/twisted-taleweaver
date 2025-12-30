namespace TwistedTaleweaver.Gateway.Creatures.Contracts;

[GraphQLDescription("Breakdown of a character's attribute scores that represents their physical and mental capabilities.")]
public class AttributeScores
{
    [GraphQLDescription("Strength represents a character's physical power and ability to exert force.")]
    public int Strength { get; set; }
    
    [GraphQLDescription("Dexterity represents a character's agility, reflexes, and balance.")]
    public int Dexterity { get; set; }
    
    [GraphQLDescription("Constitution represents a character's health, stamina, and resilience against physical challenges.")]
    public int Constitution { get; set; }
    
    [GraphQLDescription("Intelligence represents a character's mental acuity, knowledge, and reasoning ability.")]
    public int Intelligence { get; set; }
    
    [GraphQLDescription("Wisdom represents a character's perception, insight, and intuition about the world around them.")]
    public int Wisdom { get; set; }

    [GraphQLDescription("Charisma represents a character's charm, social skills, and ability to influence others.")]
    public int Charisma { get; set; }
}