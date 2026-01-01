using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Creatures.Queries;

public class CreaturesByIdQuery
{
    public required List<Guid> CreatureIds { get; set; }
}

public interface ICreaturesQueryHandler : IQueryHandler<CreaturesByIdQuery, IEnumerable<Creature>>
{
}