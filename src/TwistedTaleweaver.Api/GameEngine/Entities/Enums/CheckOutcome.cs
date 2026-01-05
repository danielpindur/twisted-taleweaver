using System.ComponentModel;

namespace TwistedTaleweaver.Api.GameEngine.Entities.Enums;

public enum CheckOutcome
{
    [Description("Critical Success")]
    CriticalSuccess = 1,
    
    Success,
    
    Failure,
    
    [Description("Critical Failure")]
    CriticalFailure,
}