using TwistedTaleweaver.Api.Messages.Entities;
using TwistedTaleweaver.Core.Queries;

namespace TwistedTaleweaver.Api.Messages.Queries;

public class GetNarrativeMessagesQuery
{
    public required List<Guid> ExpeditionIds { get; set; }
}

public interface INarrativeMessagesQueryHandler : IQueryHandler<GetNarrativeMessagesQuery, Dictionary<Guid, List<ChatMessage>>>
{
}