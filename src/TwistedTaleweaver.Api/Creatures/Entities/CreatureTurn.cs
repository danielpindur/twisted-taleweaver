namespace TwistedTaleweaver.Api.Creatures.Entities;

public class CreatureTurn : IComparable<CreatureTurn>
{
    public Guid CreatureId { get; set; }
    
    public bool IsUserTurn { get; set; }
    
    public int TurnPriority { get; set; }
    
    public DateTimeOffset AddedAt { get; set; }

    public int CompareTo(CreatureTurn? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (TurnPriority > other.TurnPriority)
        {
            return -1;
        }

        if (TurnPriority < other.TurnPriority)
        {
            return 1;
        }

        if (AddedAt < other.AddedAt)
        {
            return -1;
        }

        if (AddedAt > other.AddedAt)
        {
            return 1;
        }

        return CreatureId.CompareTo(other.CreatureId);
    }
}