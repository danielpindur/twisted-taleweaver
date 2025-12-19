using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Messages.Dtos;
using TwistedTaleweaver.Domain.Messages.Enums;

namespace TwistedTaleweaver.DataAccess.Messages.Writers;

public interface IMessagesWriter
{
    Task<MessageDto> AddSystemMessageAsync(Guid expeditionId, string content);
}

internal class MessagesWriter(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), IMessagesWriter
{
    public Task<MessageDto> AddSystemMessageAsync(Guid expeditionId, string content)
    {
        const string sql = @"
            INSERT INTO messages
            (expedition_id, type_id, content)
            VALUES (@ExpeditionId, @Type, @Content)
            RETURNING expedition_id, expedition_message_id, type_id AS type, content, created_at";

        return ExecuteWithOutputAsync<MessageDto>(sql, new
        {
            ExpeditionId = expeditionId,
            Content = content,
            Type = MessageType.System
        });
    }
}