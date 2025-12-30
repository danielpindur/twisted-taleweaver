using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Creatures.Queries;

public class CreatureTraitsQuery
{
    public required List<Guid> CreatureIds  { get; set; }
}

public interface ICreatureTraitsQueryHandler : IQueryHandler<CreatureTraitsQuery, Dictionary<Guid, List<CreatureTrait>>>
{
}