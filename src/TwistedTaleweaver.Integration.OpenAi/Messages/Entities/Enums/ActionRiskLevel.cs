namespace TwistedTaleweaver.Integration.OpenAi.Messages.Entities.Enums;

public enum ActionRiskLevel
{
    /// <summary>
    /// Little to no danger; failure carries minimal consequence.
    /// </summary>
    Trivial = 1,

    /// <summary>
    /// Some risk; failure may cause minor setbacks or embarrassment.
    /// </summary>
    Low,

    /// <summary>
    /// Meaningful danger; failure can worsen the situation or cause harm.
    /// </summary>
    Moderate,

    /// <summary>
    /// High stakes; failure is likely to cause serious harm or loss.
    /// </summary>
    High,

    /// <summary>
    /// Reckless or desperate; failure may be catastrophic or irreversible.
    /// </summary>
    Extreme
}