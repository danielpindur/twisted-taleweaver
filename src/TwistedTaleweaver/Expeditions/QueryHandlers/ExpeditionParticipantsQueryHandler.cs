using Dapper;
using TwistedTaleweaver.Common.DataAccess;
using TwistedTaleweaver.Common.DataAccess.Extensions;
using TwistedTaleweaver.Common.QueryHandlers;
using TwistedTaleweaver.Expeditions.Entities;

namespace TwistedTaleweaver.Expeditions.QueryHandlers;

public class ExpeditionParticipantsQuery
{
    public required List<Guid> ExpeditionIds { get; set; }
}

public interface IExpeditionParticipantsQueryHandler 
    : IQueryHandler<ExpeditionParticipantsQuery, Dictionary<Guid, List<ExpeditionParticipant>>>;

internal class ExpeditionParticipantsQueryHandler(IDbConnectionFactory connectionFactory) : IExpeditionParticipantsQueryHandler
{
    public async Task<Dictionary<Guid, List<ExpeditionParticipant>>> ExecuteAsync(ExpeditionParticipantsQuery query)
    {
        var participants = await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                SELECT 
                    ep.expedition_id,
                    ep.user_id,
                    u.external_user_id
                FROM expedition_participants ep
                JOIN users u ON u.user_id = ep.user_id
                WHERE ep.expedition_id = ANY(@ExpeditionIds)";

            return await connection.QueryAsync<ExpeditionParticipantDto>(sql, new
            { 
                query.ExpeditionIds
            }, tx);
        });
        
        return participants
            .GroupBy(x => x.ExpeditionId)
            .ToDictionary(
                x => x.Key, 
                x => x.Select(y => y as ExpeditionParticipant).ToList());
    }
    
    private class ExpeditionParticipantDto : ExpeditionParticipant
    {
        public Guid ExpeditionId { get; set; }
    }
}