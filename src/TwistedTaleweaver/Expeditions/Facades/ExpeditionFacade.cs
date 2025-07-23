using Microsoft.Extensions.Options;
using Npgsql;
using TwistedTaleweaver.Chat.Clients;
using TwistedTaleweaver.Common;
using TwistedTaleweaver.DataAccess.Characters.Entities;
using TwistedTaleweaver.DataAccess.Characters.Repositories;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.Extensions;
using TwistedTaleweaver.DataAccess.Expeditions.Entities;
using TwistedTaleweaver.DataAccess.Expeditions.Entities.Enums;
using TwistedTaleweaver.DataAccess.Expeditions.Repositories;
using TwistedTaleweaver.DataAccess.Permissions.Entities.Enums;
using TwistedTaleweaver.DataAccess.Permissions.Repositories;
using TwistedTaleweaver.DataAccess.Streams.Repositories;
using TwistedTaleweaver.DataAccess.Users.Repositories;
using TwistedTaleweaver.Expeditions.Configurations;
using TwistedTaleweaver.Expeditions.Entities.Inputs;
using TwistedTaleweaver.Expeditions.Processors;
using TwistedTaleweaver.Users.Clients;

namespace TwistedTaleweaver.Expeditions.Facades;

internal interface IExpeditionFacade : IFacade
{
    /// <summary>
    /// Starts a new expedition for the broadcaster.
    /// </summary>
    Task StartExpeditionAsync(string broadcasterExternalId, string userExternalId);

    /// <summary>
    /// Joins an existing expedition for the broadcaster.
    /// </summary>    
    Task JoinExpeditionAsync(string broadcasterExternalId, string userExternalId);
    
    /// <summary>
    /// Processes expeditions asynchronously, handling any necessary updates or state changes.
    /// </summary>
    Task ProcessExpeditionsAsync();
}

internal class ExpeditionFacade(
    IUserRepository userRepository,
    IExpeditionRepository expeditionRepository,
    IStreamRepository streamRepository,
    IUserBroadcasterPermissionRepository userBroadcasterPermissionRepository,
    ICharacterRepository characterRepository,
    IChatApiClient chatApiClient,
    IUserApiClient userApiClient,
    IExpeditionCombatProcessor expeditionCombatProcessor,
    IExpeditionOutcomeRepository expeditionOutcomeRepository,
    IOptions<ExpeditionConfiguration> configuration,
    Func<IUnitOfWork> createUnitOfWork,
    ILogger<ExpeditionFacade> logger) : IExpeditionFacade
{
    private readonly TimeSpan _joinPeriod = TimeSpan.FromMinutes(configuration.Value.JoinPeriodMinutes);
    private readonly int _timeoutPeriodMinutes = configuration.Value.TimeoutPeriodMinutes;

    public async Task StartExpeditionAsync(string broadcasterExternalId, string userExternalId)
    {
        await using var unitOfWork = createUnitOfWork();

        await unitOfWork.ExecuteInTransactionAsync(async transaction =>
        {
            var broadcaster = await userRepository.GetOrCreateAsync(broadcasterExternalId, transaction);
            var creator = await userRepository.GetOrCreateAsync(userExternalId, transaction);

            if (!await CanCreateExpeditionAsync(creator.UserId, broadcaster.UserId, transaction))
            {
                logger.LogWarning(
                    "Chatter {ChatterId} does not have permission to create an expedition for broadcaster {BroadcasterId} when processing expedition creation command",
                    creator.UserId, broadcaster.UserId);
                return;
            }

            var activeStream = await streamRepository.GetActiveStreamAsync(broadcaster.UserId, transaction);

            if (activeStream is null)
            {
                logger.LogWarning("No active stream found for broadcaster {BroadcasterId} when processing expedition creation command",
                    broadcaster.UserId);
                return;
            }

            var hasExpeditionInProgress = await expeditionRepository
                .HasExpeditionInProgressAsync(broadcaster.UserId, transaction);

            if (hasExpeditionInProgress)
            {
                logger.LogWarning(
                    "Broadcaster {BroadcasterId} already has an expedition in progress when processing expedition creation command",
                    broadcaster.UserId);
                
                await chatApiClient.SendChatMessageAsync(broadcaster.ExternalUserId,
                    "One tale already unfolds... Do not be greedy, mortal. Let the current screams finish first.");
                
                return;
            }

            var lastExpedition = await expeditionRepository.GetLastExpeditionAsync(broadcaster.UserId, transaction);

            if (!ExpeditionTimeoutHasPassed(lastExpedition))
            {
                logger.LogWarning(
                    "Broadcaster {BroadcasterId} has an expedition timeout that has not passed when processing expedition creation command",
                    broadcaster.UserId);
                
                await chatApiClient.SendChatMessageAsync(broadcaster.ExternalUserId,
                    "The realm recoils from your last foray — patience, the next gate stirs slowly.");
                
                return;
            }

            var expeditionId = await expeditionRepository.CreateExpeditionAsync(
                activeStream.StreamId,
                creator.UserId,
                transaction);

            logger.LogDebug(
                "Created new expedition with ID {ExpeditionId} for broadcaster {BroadcasterId} when processing expedition creation command",
                expeditionId, broadcaster.UserId);

            await chatApiClient.SendChatMessageAsync(broadcaster.ExternalUserId,
                "The ink is wet, the page awaits... I’ve summoned horrors untold. Who dares become part of this next tale?");
        });
    }

    private bool ExpeditionTimeoutHasPassed(Expedition? lastExpedition)
    {
        if (lastExpedition?.CompletedAt is null)
        {
            return true;
        }
        
        return (DateTimeOffset.Now - lastExpedition.CompletedAt.Value).TotalMinutes > _timeoutPeriodMinutes;
    }

    public async Task JoinExpeditionAsync(string broadcasterExternalId, string userExternalId)
    {
        await using var unitOfWork = createUnitOfWork();

        await unitOfWork.ExecuteInTransactionAsync(async transaction =>
        {
            var chatter = await userRepository.GetOrCreateAsync(userExternalId, transaction);
            var broadcaster = await userRepository.GetOrCreateAsync(broadcasterExternalId, transaction);

            var startedExpedition = (await expeditionRepository
                .GetByBroadcasterAndStatusAsync(
                    broadcaster.UserId,
                    ExpeditionStatus.Created,
                    transaction)).SingleOrDefault();

            if (startedExpedition is null)
            {
                logger.LogWarning("No started expedition found for broadcaster {BroadcasterId} when processing join command", broadcaster.UserId);
                return;
            }

            var character = await characterRepository.GetOrCreateAsync(chatter.UserId, broadcaster.UserId, transaction);
            var characterHasAlreadyJoined = await expeditionRepository
                .IsCharacterInExpeditionAsync(
                    character.CharacterId,
                    startedExpedition.ExpeditionId,
                    transaction);

            var externalUser = await userApiClient.ResolveUserAsync(character.ExternalUserId);

            if (characterHasAlreadyJoined)
            {
                await chatApiClient.SendChatMessageAsync(broadcaster.ExternalUserId,
                    $"You've already stepped into the abyss, {externalUser.Username}. Begging to fall twice is... unbecoming.");

                logger.LogDebug(
                    "Character {CharacterId} has already joined expedition {ExpeditionId} for broadcaster {BroadcasterId} when processing join command",
                    character.CharacterId, startedExpedition.ExpeditionId, broadcaster.UserId);
                return;
            }

            await expeditionRepository.JoinExpeditionAsync(
                    character.CharacterId,
                    startedExpedition.ExpeditionId,
                    transaction);

            logger.LogDebug(
                "Character {CharacterId} joined expedition {ExpeditionId} for broadcaster {BroadcasterId} when processing join command",
                character.CharacterId, startedExpedition.ExpeditionId, broadcaster.UserId);

            await chatApiClient.SendChatMessageAsync(broadcaster.ExternalUserId,
                $"Another soul steps forward - {externalUser.Username}, soon to be shattered or swallowed whole. Welcome to the nightmare.");
        });
    }

    public async Task ProcessExpeditionsAsync()
    {
        await ProcessExpeditionsCombatAsync();
        await ProcessExpeditionStartAsync();
    }

    private async Task ProcessExpeditionStartAsync()
    {
        await using var unitOfWork = createUnitOfWork();

        await unitOfWork.ExecuteInTransactionAsync(async transaction =>
        {
            var expeditionsToStart = await expeditionRepository.GetExpeditionsToStartAsync(_joinPeriod, transaction);
            
            foreach (var expedition in expeditionsToStart)
            {
                try
                {
                    var expeditionHasParticipants = await expeditionRepository.HasParticipantsAsync(expedition.ExpeditionId, transaction);

                    if (expeditionHasParticipants)
                    {
                        await StartExpeditionAsync(expedition, transaction);
                    }
                    else
                    {
                        await CancelExpeditionAsync(expedition, transaction);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,
                        "Failed to process expedition {ExpeditionId} starting",
                        expedition.ExpeditionId);
                }
            }
        });
    }

    private async Task CancelExpeditionAsync(Expedition expedition, NpgsqlTransaction transaction)
    {
        await expeditionRepository.UpdateExpeditionStatusAsync(
            expedition.ExpeditionId,
            ExpeditionStatus.Created,
            ExpeditionStatus.Cancelled,
            transaction);

        logger.LogDebug(
            "Expedition {ExpeditionId} transitioned to Cancelled status",
            expedition.ExpeditionId);

        await chatApiClient.SendChatMessageAsync(expedition.BroadcasterExternalUserId,
            "No one dared to chase the tale. The expedition fades, unwritten and unwanted.");
    }
    
    private async Task StartExpeditionAsync(Expedition expedition, NpgsqlTransaction transaction)
    {
        await expeditionRepository.UpdateExpeditionStatusAsync(
            expedition.ExpeditionId,
            ExpeditionStatus.Created,
            ExpeditionStatus.Started,
            transaction);

        logger.LogDebug(
            "Expedition {ExpeditionId} transitioned to Started status",
            expedition.ExpeditionId);

        await chatApiClient.SendChatMessageAsync(expedition.BroadcasterExternalUserId,
            "No more names shall be etched in this tale - the expedition begins, and so does the suffering.");
    }

    private async Task<bool> CanCreateExpeditionAsync(Guid userId, Guid broadcasterId, NpgsqlTransaction transaction)
    {
        if (userId == broadcasterId)
        {
            return true;
        }

        return await userBroadcasterPermissionRepository
            .HasActivePermissionAsync(
                userId,
                broadcasterId,
                PermissionType.ManageExpeditions,
                transaction);
    }

    private async Task ProcessExpeditionsCombatAsync()
    {
        var expeditionsToCalculate = await expeditionRepository.GetExpeditionsToCalculateAsync();

        foreach (var expedition in expeditionsToCalculate)
        {
            try
            {
                await ProcessSingleExpeditionCombatAsync(expedition);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process combat for expedition {ExpeditionId}", expedition.ExpeditionId);
                await expeditionRepository.UpdateExpeditionStatusAsync(expedition.ExpeditionId, ExpeditionStatus.Started, ExpeditionStatus.Failed);
            }
        }
    }

    private async Task ProcessSingleExpeditionCombatAsync(Expedition expedition)
    {
        var participants = (await expeditionRepository.GetParticipantsAsync(expedition.ExpeditionId)).ToList();

        var expeditionOutcome = await expeditionCombatProcessor.Process(new ExpeditionInput()
        {
            ExpeditionId = expedition.ExpeditionId,
            Characters = participants.Select(x => new CharacterInput()
            {
                CharacterId = x.CharacterId, 
                ExternalUserId = x.ExternalUserId, 
                Health = 100
            }).ToList(),
            Encounters =
            [
                new()
                {
                    EncounterId = Guid.NewGuid(),
                    Monster = new MonsterInput()
                    {
                        MonsterId = Guid.NewGuid(),
                        Name = "The Hollow Maw",
                        Health = 100
                    }
                },
                /*new()
                {
                    EncounterId = Guid.NewGuid(),
                    Monster = new MonsterInput()
                    {
                        MonsterId = Guid.NewGuid(),
                        Name = "Mother of Needles",
                        Health = 80
                    }
                },
                new()
                {
                    EncounterId = Guid.NewGuid(),
                    Monster = new MonsterInput()
                    {
                        MonsterId = Guid.NewGuid(),
                        Name = "Warden of the Forgotten",
                        Health = 120
                    }
                },
                new()
                {
                    EncounterId = Guid.NewGuid(),
                    Monster = new MonsterInput()
                    {
                        MonsterId = Guid.NewGuid(),
                        Name = "The Choir of Teeth",
                        Health = 90
                    }
                }*/
            ]
        });
            
        await ProcessOutcomeAsync(expedition, expeditionOutcome, participants);
    }

    private async Task ProcessOutcomeAsync(
        Expedition expedition,
        ExpeditionOutcome expeditionOutcome,
        List<ExpeditionParticipant> participants)
    {
        var experienceIncrements = CalculateExperienceGains(expeditionOutcome, participants);
        
        await using var unitOfWork = createUnitOfWork();

        await unitOfWork.ExecuteInTransactionAsync(async transaction =>
        {
            await expeditionOutcomeRepository.AddAsync(expeditionOutcome, transaction);

            if (experienceIncrements.Count != 0)
            {
                await characterRepository.AddExperienceIncrementsAsync(experienceIncrements, transaction);
            }
            
            await expeditionRepository.UpdateExpeditionStatusAsync(
                expedition.ExpeditionId, 
                ExpeditionStatus.Started,
                ExpeditionStatus.Completed,
                transaction);

            foreach (var narration in expeditionOutcome.Narrations)
            {
                await chatApiClient.SendChatMessageAsync(expedition.BroadcasterExternalUserId, narration);
            }
        });
    }

    private static List<Guid> GetSurvivors(
        ExpeditionOutcome expeditionOutcome,
        List<ExpeditionParticipant> participants)
    {
        var survivors = participants.Select(x => x.CharacterId).ToHashSet();

        foreach (var encounter in expeditionOutcome.Encounters)
        {
            survivors.ExceptWith(encounter.CharacterDeaths);
        }
        
        return survivors.ToList();
    }

    private static List<CharacterExperienceIncrement> CalculateExperienceGains(
        ExpeditionOutcome expeditionOutcome,
        List<ExpeditionParticipant> participants)
    {
        var survivors = GetSurvivors(expeditionOutcome, participants);
        var experienceGain = Random.Shared.Next(10, 51);

        return survivors.Select(x => new CharacterExperienceIncrement()
        {
            ExpeditionId = expeditionOutcome.ExpeditionId,
            CharacterId = x,
            Amount = experienceGain
        }).ToList();
    }
}