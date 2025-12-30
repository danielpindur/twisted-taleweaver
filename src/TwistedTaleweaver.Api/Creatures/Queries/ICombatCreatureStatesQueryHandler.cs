using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Creatures.Queries;

public class CharactersStatesQuery
{
    public required List<Guid> ExpeditionIds  { get; set; }
}

public interface ICombatCreatureStatesQueryHandler : IQueryHandler<CharactersStatesQuery, Dictionary<Guid, List<CombatCreatureState>>>
{
}