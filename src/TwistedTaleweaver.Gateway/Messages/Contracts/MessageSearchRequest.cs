using System.ComponentModel.DataAnnotations;

namespace TwistedTaleweaver.Gateway.Messages.Contracts;

/// <summary>
/// Search query for chat messages infinite scroll search 
/// </summary>
public class MessageSearchRequest
{
    /// <summary>
    /// Identifier of the expedition to get messages for
    /// </summary>
    [Required]
    public Guid ExpeditionId { get; set; }

    /// <summary>
    /// Identifier of the last message previously retrieved
    /// </summary>
    public int? LastExpeditionMessageId { get; set; }

    /// <summary>
    /// Number of items per page to retrieve
    /// </summary>
    public int? PageSize { get; set; }
}