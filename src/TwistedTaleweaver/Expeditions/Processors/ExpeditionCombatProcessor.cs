using TwistedTaleweaver.Expeditions.Entities.Inputs;
using TwistedTaleweaver.Expeditions.Entities.Outcomes;
using TwistedTaleweaver.Expeditions.Entities.States;
using TwistedTaleweaver.Expeditions.Facades;
using TwistedTaleweaver.Expeditions.Helpers;
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
        
        var externalUserIds = expeditionState.AliveCharacters.Select(x => x.ExternalUserId).ToList();
        var usernamesByExternalUserId = await userApiClient.ResolveUsersAsync(externalUserIds);
        
        logger.LogDebug("Processing combat for expedition {ExpeditionId}", expeditionState.ExpeditionId);

        expeditionOutcome.Prologue = NarrationHelper.GeneratePrologue();
        
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
                
            if (expeditionState.AliveCharacters.Count == 0)
            {
                logger.LogDebug("All characters defeated in expedition {ExpeditionId}", expeditionState.ExpeditionId);
                break;
            }
        }

        if (expeditionState.AliveCharacters.Count == 0)
        {
            expeditionOutcome.Epilogue = NarrationHelper.GenerateFailureEpilogue();
        }
        else
        {
            expeditionOutcome.Epilogue = NarrationHelper.GenerateSuccessEpilogue();
        }
        
        return expeditionOutcome;
    }

    private static EncounterOutcome ProcessEncounter(EncounterState encounter, Dictionary<string, ExternalUser> usernamesByExternalUserId)
    {
        var narration = new List<string> { NarrationHelper.GenerateMonsterAppearMessage(encounter.Monster.Name) };

        while (encounter.Monster.Health.IsAlive && encounter.AliveCharacters.Count > 0)
        {
            ProcessCharactersAttack(encounter, usernamesByExternalUserId, narration);

            if (!encounter.Monster.Health.IsAlive)
            {
                break;
            }
            
            ProcessMonsterRetaliation(encounter, usernamesByExternalUserId, narration);
        }
        
        return new EncounterOutcome()
        {
            EncounterId = encounter.EncounterId,
            Narration = narration
        };
    }
    
    private static void ProcessCharactersAttack(
        EncounterState encounter, 
        Dictionary<string, ExternalUser> usernamesByExternalUserId,
        List<string> narration)
    {
        var attackCount = Math.Min(10, encounter.AliveCharacters.Count);

        for (var i = 0; i < attackCount; i++)
        {
            var characterIndex = Random.Shared.Next(encounter.AliveCharacters.Count);
            var character = encounter.AliveCharacters[characterIndex];

            var externalUser = usernamesByExternalUserId[character.ExternalUserId];

            if (!RollSuccess())
            {
                narration.Add(NarrationHelper.GenerateCharacterMissedMessage(externalUser.Username, encounter.Monster.Name));
                continue;
            }
            
            encounter.Monster.Health.ApplyDamage(RollDamage());
            narration.Add(NarrationHelper.GenerateCharacterHitMessage(externalUser.Username, encounter.Monster.Name));

            if (!encounter.Monster.Health.IsAlive)
            {
                narration.Add(NarrationHelper.GenerateMonsterDiesMessage(externalUser.Username, encounter.Monster.Name));
                break;
            }
        }
    }

    private static void ProcessMonsterRetaliation(
        EncounterState encounter,
        Dictionary<string, ExternalUser> usernamesByExternalUserId,
        List<string> narration)
    {
        var attackCount = Math.Min(2, encounter.AliveCharacters.Count);

        for (var i = 0; i < attackCount; i++)
        {
            var targetIndex = Random.Shared.Next(encounter.AliveCharacters.Count);
            var target = encounter.AliveCharacters[targetIndex];
            var externalUser = usernamesByExternalUserId[target.ExternalUserId];

            if (!RollSuccess())
            {
                narration.Add(NarrationHelper.GenerateMonsterMissedMessage(externalUser.Username, encounter.Monster.Name));
                continue;
            }
            
            target.Health.ApplyDamage(RollDamage());
            narration.Add(NarrationHelper.GenerateMonsterHitMessage(externalUser.Username, encounter.Monster.Name));

            if (!target.Health.IsAlive)
            {
                narration.Add(NarrationHelper.GenerateCharacterDiesMessage(externalUser.Username, encounter.Monster.Name));
                encounter.AliveCharacters[targetIndex] = encounter.AliveCharacters[^1];
                encounter.AliveCharacters.RemoveAt(encounter.AliveCharacters.Count - 1);

                if (encounter.AliveCharacters.Count == 0)
                {
                    // No more alive targets
                    break;
                }
            }
        }
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