using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Creatures.Queries;

public class CharacterByIdQuery
{
    public Guid CharacterId { get; set; }
}

public interface ICharacterByIdQueryHandler : IQueryHandler<CharacterByIdQuery, Character?>
{
}