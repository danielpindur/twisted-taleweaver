namespace TwistedTaleweaver.Expeditions.Configurations;

using System.ComponentModel.DataAnnotations;

public class ExpeditionConfiguration
{
    [Range(1, int.MaxValue, ErrorMessage = "TimeoutPeriodMinutes must be greater than 0")]
    public int TimeoutPeriodMinutes { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "JoinPeriodMinutes must be greater than 0")]
    public int JoinPeriodMinutes { get; set; }
}