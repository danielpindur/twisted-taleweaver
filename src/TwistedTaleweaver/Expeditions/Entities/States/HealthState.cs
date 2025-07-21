namespace TwistedTaleweaver.Expeditions.Entities.States;

public class HealthState
{
    public HealthState(int health)
    {
        Max =  health;
        Current = health;
    }
    
    public int Max { get; init; }
    
    public int Current { get; private set; }

    public void ApplyDamage(int damage)
    {
        Current -= damage;
    }

    public void Heal(int health)
    {
        Current += health;

        if (Current > Max)
        {
            Current = Max;
        }
    }
    
    public bool IsAlive => Current > 0;
}