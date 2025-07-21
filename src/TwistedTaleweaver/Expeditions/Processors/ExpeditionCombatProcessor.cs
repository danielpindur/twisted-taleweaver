using TwistedTaleweaver.Expeditions.Entities.Inputs;
using TwistedTaleweaver.Expeditions.Entities.Outcomes;
using TwistedTaleweaver.Expeditions.Entities.States;
using TwistedTaleweaver.Expeditions.Facades;
using TwistedTaleweaver.Users.Clients;
using TwistedTaleweaver.Users.Entities;

namespace TwistedTaleweaver.Expeditions.Processors;

internal interface IExpeditionCombatProcessor
{
    /// <summary>
    /// Processes the combat for the given expedition.
    /// </summary>
    Task<ExpeditionOutcome> Process(ExpeditionInput input);
}

internal class ExpeditionCombatProcessor(
    IUserApiClient userApiClient,
    ILogger<ExpeditionFacade> logger) : IExpeditionCombatProcessor
{
    public async Task<ExpeditionOutcome> Process(ExpeditionInput input)
    {
        var expeditionState = new ExpeditionState(input);
        var expeditionOutcome = new ExpeditionOutcome() { ExpeditionId = input.ExpeditionId };
        
        var externalUserIds = expeditionState.Characters.Select(x => x.ExternalUserId).ToList();
        var usernamesByExternalUserId = await userApiClient.ResolveUsersAsync(externalUserIds);
        
        logger.LogDebug("Processing combat for expedition {ExpeditionId}", expeditionState.ExpeditionId);

        expeditionOutcome.Prologue = "So... brave little souls have stepped forward. The gate creaks open, and the realm stretches its jaws. Let us begin.";
        
        while (true)
        {
            var encounter = expeditionState.NextEncounter();
                
            if (encounter is null)
            {
                logger.LogDebug("All encounters processed for expedition {ExpeditionId}", expeditionState.ExpeditionId);
                break;
            }
                
            var encounterOutcome = ProcessEncounter(encounter, usernamesByExternalUserId);
            expeditionOutcome.Encounters.Add(encounterOutcome);
                
            expeditionState.RemoveDeadCharacters();

            if (expeditionState.Characters.Count == 0)
            {
                logger.LogDebug("All characters defeated in expedition {ExpeditionId}", expeditionState.ExpeditionId);
                break;
            }
        }

        if (expeditionState.Characters.Count == 0)
        {
            expeditionOutcome.Epilogue = "The tale collapses in ruin. None will sing of this expedition — only whisper.";
        }
        else
        {
            expeditionOutcome.Epilogue = "Against every curse and claw, they endured. The expedition survives — for now.";
        }
        
        return expeditionOutcome;
    }

    private EncounterOutcome ProcessEncounter(EncounterState encounter, Dictionary<string, ExternalUser> usernamesByExternalUserId)
    {
        var narration = new List<string>();

        narration.Add($"A wild {encounter.Monster.Name} appears!");

        foreach (var character in encounter.Characters)
        {
            if (!encounter.Monster.Health.IsAlive)
            {
                break;
            }
            
            var externalUser = usernamesByExternalUserId[character.ExternalUserId];
            
            if (RollSuccess())
            {
                encounter.Monster.Health.ApplyDamage(RollDamage());

                if (encounter.Monster.Health.IsAlive)
                {
                    narration.Add($"{externalUser.Username} strikes the {encounter.Monster.Name}!");
                }
                else
                {
                    narration.Add($"With a final cry, {externalUser.Username} fells the beast. A chapter ends in blood.");                    
                }
            }
            else
            {
                narration.Add($"{externalUser.Username} misses their attack!");
            }
        }
        
        if (encounter.Monster.Health.IsAlive)
        {
            var retaliationTargets = encounter.Characters.Where(c => c.Health.IsAlive).ToList();
            
            if (retaliationTargets.Count > 0)
            {
                var attackCount = Math.Min(2, retaliationTargets.Count);
                
                for (var i = 0; i < attackCount; i++)
                {
                    var target = retaliationTargets[Random.Shared.Next(retaliationTargets.Count)];
                    var externalUser = usernamesByExternalUserId[target.ExternalUserId];

                    if (RollSuccess())
                    {
                        target.Health.ApplyDamage(RollDamage());

                        if (target.Health.IsAlive)
                        {
                            narration.Add($"The {encounter.Monster.Name} lashes out at {externalUser.Username}, wounding them!");
                        }
                        else
                        {
                            narration.Add($"A scream cut short — {externalUser.Username} falls, their tale ends in silence.");
                        }
                    }
                    else
                    {
                        narration.Add($"The {encounter.Monster.Name} swipes at {externalUser.Username} but misses!");
                    }
                }
            }
        }
        
        return new EncounterOutcome()
        {
            EncounterId = encounter.EncounterId,
            Narration = narration
        };
    }
    
    private static bool RollSuccess()
    {
        return Random.Shared.NextDouble() < 0.7;
    }

    private static int RollDamage()
    {
        return Random.Shared.Next(0, 15);
    }
}