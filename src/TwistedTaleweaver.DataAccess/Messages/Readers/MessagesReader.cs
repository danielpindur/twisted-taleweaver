using TwistedTaleweaver.Api.Messages.Entities.Enums;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Messages.Dtos;

namespace TwistedTaleweaver.DataAccess.Messages.Readers;

public interface IMessagesReader
{
    Task<IEnumerable<MessageDto>> SearchAsync(Guid expeditionId, long? lastMessageId, int pageSize);
    
    Task<IEnumerable<string>> GetContents(Guid expeditionId, MessageType messageType, long lastMessageId);
    
    Task<MessageDto> GetAsync(Guid expeditionId, long messageId);

    Task<IEnumerable<string>> GetContentsSinceLastNarrativeAsync(Guid expeditionId);
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

    public Task<IEnumerable<string>> GetContents(Guid expeditionId, MessageType messageType, long lastMessageId)
    {
        const string sql = @"
            SELECT 
                m.content
            FROM messages m
            WHERE m.expedition_id = @ExpeditionId
                AND m.message_id < @LastMessageId AND m.type_id = @MessageType
            ORDER BY m.message_id";

        return QueryAsync<string>(sql, new
        {
            ExpeditionId = expeditionId,
            LastMessageId = lastMessageId,
            MessageType = messageType
        });
    }

    public Task<MessageDto> GetAsync(Guid expeditionId, long messageId)
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
            WHERE m.expedition_id = @ExpeditionId AND m.message_id = @MessageId";

        return QuerySingleAsync<MessageDto>(sql, new
        {
            ExpeditionId = expeditionId,
            MessageId = messageId
        });
    }

    public Task<IEnumerable<string>> GetContentsSinceLastNarrativeAsync(Guid expeditionId)
    {
        const string sql = @"
        SELECT 
            m.content
        FROM messages m
        WHERE m.expedition_id = @ExpeditionId
            AND m.message_id > COALESCE(
                (
                    SELECT MAX(message_id)
                    FROM messages
                    WHERE expedition_id = @ExpeditionId
                        AND type_id = @NarrativeType
                ), 0)
        ORDER BY m.message_id";

        return QueryAsync<string>(sql, new
        {
            ExpeditionId = expeditionId,
            NarrativeType = MessageType.Narrative
        });
    }
}