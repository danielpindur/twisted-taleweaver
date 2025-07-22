namespace TwistedTaleweaver.Expeditions.Entities.States;

public abstract class Damageable
{
    public Damageable(int health)
    {
        if (health <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(health), "Health must be positive");
        }
        
        Max =  health;
        Current = health;
    }
    
    public int Max { get; init; }
    
    public int Current { get; private set; }

    public void ApplyDamage(int damage)
    {
        if (damage <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage), "Damage must be positive");
        }
        
        Current -= damage;
    }

    public void Heal(int health)
    {
        if (health <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(health), "Health must be positive");
        }
        
        Current += health;

        if (Current > Max)
        {
            Current = Max;
        }
    }
    
    public bool IsAlive => Current > 0;
}