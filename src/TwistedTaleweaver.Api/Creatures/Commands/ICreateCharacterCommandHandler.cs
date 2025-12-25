using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Domain.Creatures;
using TwistedTaleweaver.Domain.Creatures.Enums;

namespace TwistedTaleweaver.Api.Creatures.Commands;

public class CreateCharacterCommand
{
    public Guid UserId { get; set; }

    public Guid ExpeditionId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public ClassType Class { get; set; }

    public Gender Gender { get; set; }

    public CreatureType CreatureType { get; set; }

    public NarrativeAlignment Alignment { get; set; }

    public required AttributeScores AttributeScores { get; set; }
}

public class CreateCharacterResult
{
    public Guid CharacterId { get; set; }
}

public interface ICreateCharacterCommandHandler : ICommandHandler<CreateCharacterCommand, CreateCharacterResult>
{
}