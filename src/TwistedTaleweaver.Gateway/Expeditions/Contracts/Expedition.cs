using TwistedTaleweaver.Api.Expeditions.Entities.Enums;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[GraphQLDescription("Represents an expedition.")]
public class Expedition
{
    [GraphQLDescription("Unique identifier of the expedition.")] 
    public Guid Id { get; init; }
    
    [GraphQLDescription("Name of the expedition.")]
    public required string Name { get; init; }

    [GraphQLDescription("Description of the expedition.")]
    public required string Description { get; init; }

    [GraphQLDescription("Difficulty level of the expedition.")]
    public ExpeditionDifficulty Difficulty { get; init; }

    [GraphQLDescription("Minimum number of participants required for the expedition.")]
    public int MinParticipants { get; init; }

    [GraphQLDescription("Maximum number of participants allowed for the expedition.")]
    public int MaxParticipants { get; init; }

    [GraphQLDescription("Current number of participants in the expedition.")]
    public int CurrentParticipants { get; init; }

    [GraphQLDescription("Outcome of the expedition, if completed.")]
    public ExpeditionOutcome? Outcome { get; init; }

    [GraphQLDescription("Date and time when the expedition was created.")]
    public DateTimeOffset CreatedAt { get; init; }
    
    [GraphQLDescription("Date and time when the expedition was started, if applicable.")]
    public DateTimeOffset? StartedAt { get; init; }
    
    [GraphQLDescription("Date and time when the expedition was completed, if applicable.")]
    public DateTimeOffset? CompletedAt { get; init; }

    [GraphQLDescription("Lesson learned from the expedition, if any.")]    
    public string? LessonLearned { get; init; }
    
    [GraphQLIgnore]
    public Guid CreatedByUserId { get; init; }
}