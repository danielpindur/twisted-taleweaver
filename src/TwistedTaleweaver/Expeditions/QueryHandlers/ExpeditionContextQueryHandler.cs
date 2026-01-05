using TwistedTaleweaver.Core.Queries;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities;
using TwistedTaleweaver.Integration.OpenAi.Expeditions.Mappers;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

internal class ExpeditionContextQuery
{
    public Guid ExpeditionId { get; set; }
}

internal interface IExpeditionContextQueryHandler : IQueryHandler<ExpeditionContextQuery, ExpeditionContext>
{
}

internal class ExpeditionContextQueryHandler(IExpeditionsReader expeditionsReader) : IExpeditionContextQueryHandler
{
    public async Task<ExpeditionContext> ExecuteAsync(ExpeditionContextQuery query)
    {
        var expedition = await expeditionsReader.GetByIdAsync(query.ExpeditionId);
        
        if (expedition == null)
        {
            throw new InvalidOperationException($"Expedition with ID {query.ExpeditionId} not found.");
        }

        return expedition.ToExpeditionContext();
    }
}