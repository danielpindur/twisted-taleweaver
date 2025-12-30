using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Creatures.Queries;

public class CharactersByIdQuery
{
    public required List<Guid> CharacterIds { get; set; }
}

public interface ICharactersQueryHandler : IQueryHandler<CharactersByIdQuery, IEnumerable<Character>>
{
}