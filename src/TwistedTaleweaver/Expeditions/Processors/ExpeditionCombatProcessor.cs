using TwistedTaleweaver.DataAccess.Expeditions.Entities;
using TwistedTaleweaver.DataAccess.Expeditions.Entities.Enums;
using TwistedTaleweaver.Expeditions.Entities.Inputs;
using TwistedTaleweaver.Expeditions.Entities.States;
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
    ILogger<ExpeditionCombatProcessor> logger) : IExpeditionCombatProcessor
{
    public async Task<ExpeditionOutcome> Process(ExpeditionInput input)
    {
        var expeditionState = new ExpeditionState(input);
        var expeditionOutcome = new ExpeditionOutcome() { ExpeditionId = input.ExpeditionId };
        
        var externalUserIds = expeditionState.AliveCharacters.Select(x => x.ExternalUserId).ToList();
        var usernamesByExternalUserId = await userApiClient.ResolveUsersAsync(externalUserIds);
        
        logger.LogDebug("Processing combat for expedition {ExpeditionId}", expeditionState.ExpeditionId);

        expeditionOutcome.Narrations.Add(NarrationHelper.GeneratePrologue());
        
        while (true)
        {
            var encounter = expeditionState.NextEncounter();
                
            if (encounter is null)
            {
                logger.LogDebug("All encounters processed for expedition {ExpeditionId}", expeditionState.ExpeditionId);
                break;
            }
                
            var (encounterOutcome, narrations) = ProcessEncounter(encounter, usernamesByExternalUserId);
            
            expeditionOutcome.Encounters.Add(encounterOutcome);
            expeditionOutcome.Narrations.AddRange(narrations);
                
            if (expeditionState.AliveCharacters.Count == 0)
            {
                logger.LogDebug("All characters defeated in expedition {ExpeditionId}", expeditionState.ExpeditionId);
                break;
            }
        }

        if (expeditionState.AliveCharacters.Count == 0)
        {
            expeditionOutcome.Narrations.Add(NarrationHelper.GenerateFailureEpilogue());
            expeditionOutcome.Result = ExpeditionResult.Failure;
        }
        else
        {
            expeditionOutcome.Narrations.Add(NarrationHelper.GenerateSuccessEpilogue());
            expeditionOutcome.Result = ExpeditionResult.Success;
        }
        
        return expeditionOutcome;
    }

    private static (EncounterOutcome outcome, List<string> narrations) ProcessEncounter(
        EncounterState encounter, Dictionary<string, ExternalUser> usernamesByExternalUserId)
    {
        var narrations = new List<string> { NarrationHelper.GenerateMonsterAppearMessage(encounter.Monster.Name) };
        var characterDeaths = new List<Guid>();
        
        while (encounter.Monster.IsAlive && encounter.AliveCharacters.Count > 0)
        {
            ProcessCharactersAttack(encounter, usernamesByExternalUserId, narrations);

            if (!encounter.Monster.IsAlive)
            {
                break;
            }
            
            ProcessMonsterRetaliation(encounter, usernamesByExternalUserId, narrations, characterDeaths);
        }
        
        var outcome =  new EncounterOutcome()
        {
            EncounterId = encounter.EncounterId,
            Result = encounter.Monster.IsAlive ? EncounterResult.Failure : EncounterResult.Success,
            Enemy = new EncounterOutcome.Monster
            {
                MonsterId = encounter.Monster.MonsterId,
                Name = encounter.Monster.Name,
            },
            CharacterDeaths = characterDeaths
        };

        return (outcome, narrations);
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
            
            encounter.Monster.ApplyDamage(RollDamage());
            narration.Add(NarrationHelper.GenerateCharacterHitMessage(externalUser.Username, encounter.Monster.Name));

            if (!encounter.Monster.IsAlive)
            {
                narration.Add(NarrationHelper.GenerateMonsterDiesMessage(externalUser.Username, encounter.Monster.Name));
                break;
            }
        }
    }

    private static void ProcessMonsterRetaliation(EncounterState encounter,
        Dictionary<string, ExternalUser> usernamesByExternalUserId,
        List<string> narration, 
        List<Guid> characterDeaths)
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
            
            target.ApplyDamage(RollDamage());
            narration.Add(NarrationHelper.GenerateMonsterHitMessage(externalUser.Username, encounter.Monster.Name));

            if (!target.IsAlive)
            {
                narration.Add(NarrationHelper.GenerateCharacterDiesMessage(externalUser.Username, encounter.Monster.Name));
                characterDeaths.Add(target.CharacterId);
                
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
        return Random.Shared.Next(1, 15);
    }
}