namespace TwistedTaleweaver.GameEngine.Entities.Enums;

public enum ExpeditionEventType
{
    /// <summary>
    /// Expedition was started -> generate narrative
    /// </summary>
    ExpeditionStarted = 1,
    
    /// <summary>
    /// Narrative was generated -> we need next actor to take it's turn
    /// </summary>
    NarrativeGenerated,
    
    /// <summary>
    /// An action has been read from the character message
    /// </summary>
    CharacterActionRequested,
    
    /// <summary>
    /// An action adjudication message was generated -> we need to process the results
    /// </summary>
    AllowedActionAdjudicationGenerated
}