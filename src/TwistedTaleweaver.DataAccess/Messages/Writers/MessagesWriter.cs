using TwistedTaleweaver.Api.Messages.Entities.Enums;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Messages.Dtos;

namespace TwistedTaleweaver.DataAccess.Messages.Writers;

public interface IMessagesWriter
{
    Task<MessageDto> AddSystemMessageAsync(Guid expeditionId, string content);
    
    Task<MessageDto> AddNarrativeMessageAsync(Guid expeditionId, string content);
    
    Task<MessageDto> AddctionAdjudicationMessage(Guid expeditionId, string content);

    Task<MessageDto> AddCharacterMessageAsync(Guid expeditionId, string content, Guid characterId);
}

internal class MessagesWriter(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IMessagesWriter
{
    public Task<MessageDto> AddSystemMessageAsync(Guid expeditionId, string content)
    {
        const string sql = @"
            INSERT INTO messages
            (expedition_id, type_id, content)
            VALUES (@ExpeditionId, @Type, @Content)
            RETURNING message_id, expedition_id, type_id AS type, content, created_at";

        return ExecuteWithOutputAsync<MessageDto>(sql, new
        {
            ExpeditionId = expeditionId,
            Content = content,
            Type = MessageType.System
        });
    }

    public Task<MessageDto> AddNarrativeMessageAsync(Guid expeditionId, string content)
    {
        const string sql = @"
            INSERT INTO messages
            (expedition_id, type_id, content)
            VALUES (@ExpeditionId, @Type, @Content)
            RETURNING message_id, expedition_id, type_id AS type, content, created_at";

        return ExecuteWithOutputAsync<MessageDto>(sql, new
        {
            ExpeditionId = expeditionId,
            Content = content,
            Type = MessageType.Narrative
        });
    }

    public Task<MessageDto> AddctionAdjudicationMessage(Guid expeditionId, string content)
    {
        const string sql = @"
            INSERT INTO messages
            (expedition_id, type_id, content)
            VALUES (@ExpeditionId, @Type, @Content)
            RETURNING message_id, expedition_id, type_id AS type, content, created_at";

        return ExecuteWithOutputAsync<MessageDto>(sql, new
        {
            ExpeditionId = expeditionId,
            Content = content,
            Type = MessageType.ActionAdjudication
        });
    }

    public Task<MessageDto> AddCharacterMessageAsync(Guid expeditionId, string content, Guid characterId)
    {
        const string sql = @"
            INSERT INTO messages
            (expedition_id, type_id, sender_character_id, content)
            VALUES (@ExpeditionId, @Type, @CharacterId, @Content)
            RETURNING message_id, expedition_id, type_id AS type, sender_character_id, content, created_at";

        return ExecuteWithOutputAsync<MessageDto>(sql, new
        {
            ExpeditionId = expeditionId,
            Content = content,
            Type = MessageType.Action,
            CharacterId = characterId
        });
    }
}