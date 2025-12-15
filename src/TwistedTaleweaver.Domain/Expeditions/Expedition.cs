using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Domain.Users;

namespace TwistedTaleweaver.Domain.Expeditions;

/// <summary>
/// Represents an expedition within the system
/// </summary>
public class Expedition : ExpeditionSlim
{
    /// <summary>
    /// User who created the expedition
    /// </summary>
    [Required]
    public required User CreatedByUser { get; set; }
    
    /// <summary>
    /// <see cref="ExpeditionParticipant"/>
    /// </summary>
    [Required]
    public required List<ExpeditionParticipant> Participants { get; set; }
}