namespace TwistedTaleweaver.Gateway.Users.Contracts;

[GraphQLDescription("Represents an user.")]
public class User
{
    [GraphQLDescription("Unique identifier of the user.")]
    public Guid Id { get; init; }
    
    [GraphQLDescription("External identifier of the user.")]
    public required string ExternalId { get; init; }
    
    [GraphQLDescription("Name of the user.")]
    public required string Name { get; init; }
}