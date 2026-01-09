using TwistedTaleweaver.Api.Creatures.Entities;

namespace TwistedTaleweaver.Api.Expeditions.Entities;

public class ExpeditionTurnOrder
{
    public ExpeditionTurnOrder()
    {
    }
    
    public ExpeditionTurnOrder(Guid expeditionId)
    {
        ExpeditionId = expeditionId;
    }
    
    public Guid ExpeditionId { get; set; }
    
    public int CurrentTurnIndex { get; set; }
    
    public bool AwaitingAction { get; set; }

    public List<CreatureTurn> Turns { get; set; } = new();

    public Guid? CurrentActorId => AwaitingAction && Turns.Count > 0 ? Turns[CurrentTurnIndex].CreatureId : null;

    public bool ActionGenerationRequired => AwaitingAction && Turns.Count > 0 && !Turns[CurrentTurnIndex].IsUserTurn;
    
    public void Add(CombatCreature creature, bool isUser)
    {
        var currentActorId = Turns.Count > 0
            ? Turns[CurrentTurnIndex].CreatureId
            : (Guid?)null;
        
        var priority = (creature.AttributeScores.Dexterity - 10) / 2;
        
        priority += creature.SpeedScores.Walking / 30;

        if (creature.SpeedScores.Flying is > 0)
        {
            priority += 1;
        }

        if (creature.SpeedScores.Teleportation is > 0)
        {
            priority += 2;
        }

        var creatureTurn = new CreatureTurn()
        {
            CreatureId = creature.CreatureId,
            TurnPriority = priority,
            AddedAt = DateTimeOffset.Now,
            IsUserTurn = isUser
        };
        
        Turns.Add(creatureTurn);
        Turns.Sort();

        CurrentTurnIndex = currentActorId.HasValue 
            ? Turns.FindIndex(t => t.CreatureId == currentActorId.Value) 
            : 0;
    }

    public void RemoveCreature(Guid creatureId)
    {
        Guid? currentActorAfterRemovalId = null;

        if (Turns.Count > 1) // we have actors remaining after removal
        {
            var currentActorId = Turns[CurrentTurnIndex].CreatureId;

            if (currentActorAfterRemovalId == creatureId)
            {
                currentActorAfterRemovalId = Turns[(CurrentTurnIndex + 1) % Turns.Count].CreatureId;
            }
            else
            {
                currentActorAfterRemovalId = currentActorId;
            }
        }
        
        Turns = Turns.Where(x => x.CreatureId != creatureId).ToList();
        Turns.Sort();
        
        CurrentTurnIndex = currentActorAfterRemovalId.HasValue 
            ? Turns.FindIndex(t => t.CreatureId == currentActorAfterRemovalId.Value) 
            : 0;
    }

    public void InitIndex()
    {
        CurrentTurnIndex = Turns.LastOrDefault()?.TurnPriority ?? 0;
    }
    
    public void ActionTaken()
    {
        AwaitingAction = false;
    }

    public void RetakeAction()
    {
        AwaitingAction = true;
    }
    
    public void NextTurn()
    {
        if (Turns.Count == 0)
        {
            return;
        }

        CurrentTurnIndex = (CurrentTurnIndex + 1) % Turns.Count;
        AwaitingAction = true;
    }
}