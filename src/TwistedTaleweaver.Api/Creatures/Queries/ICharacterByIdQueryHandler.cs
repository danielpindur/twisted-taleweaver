using TwistedTaleweaver.Core.Queries;
using TwistedTaleweaver.Domain.Creatures;

namespace TwistedTaleweaver.Api.Creatures.Queries;

public class CharacterByIdQuery
{
    public Guid CharacterId { get; set; }
}

public interface ICharacterByIdQueryHandler : IQueryHandler<CharacterByIdQuery, Character?>
{
}