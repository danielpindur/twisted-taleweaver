using TwistedTaleweaver.Api.GameEngine.Entities.Enums;

namespace TwistedTaleweaver.Api.GameEngine.Entities;

public class CheckResult
{
    public required RequiredCheck Check { get; set; }
    
    public int TotalRolledValue { get; set; }
    
    public CheckOutcome Outcome { get; set; }
}