using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Messages.Dtos;

namespace TwistedTaleweaver.DataAccess.Messages.Readers;

public interface IMessagesReader
{
    Task<IEnumerable<MessageDto>> SearchAsync(Guid expeditionId, long? lastMessageId, int pageSize);
}

internal class MessagesReader(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IMessagesReader
{
    public Task<IEnumerable<MessageDto>> SearchAsync(Guid expeditionId, long? lastMessageId, int pageSize)
    {
        const string sql = @"
            SELECT 
                m.message_id,
                m.expedition_id,
                m.sender_character_id,
                m.type_id AS type,
                m.content,
                m.created_at
            FROM messages m
            WHERE m.expedition_id = @ExpeditionId
                AND (@LastMessageId IS NULL OR m.message_id < @LastMessageId)
            ORDER BY m.message_id DESC
            LIMIT @PageSize";

        return QueryAsync<MessageDto>(sql, new
        {
            ExpeditionId = expeditionId,
            LastMessageId = lastMessageId,
            PageSize = pageSize
        });
    }
}