namespace TwistedTaleweaver.GameEngine.Entities;

public class ActionAdjudication
{
    public Guid CreatureId { get; set; }
    
    public required List<RequiredCheck> RequiredChecks { get; set; }
}