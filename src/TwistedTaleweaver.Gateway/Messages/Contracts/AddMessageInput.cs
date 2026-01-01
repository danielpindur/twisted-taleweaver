namespace TwistedTaleweaver.Gateway.Messages.Contracts;

[GraphQLDescription("Input for adding a new message to an expedition.")]
public class AddMessageInput
{
    [GraphQLDescription("Unique identifier of the expedition to which the message is being added.")]
    public Guid ExpeditionId { get; set; }
    
    [GraphQLDescription("Content of the message to be added.")]
    public required string Content { get; set; }
}