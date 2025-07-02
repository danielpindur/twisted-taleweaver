using Npgsql;
using TwistedTaleweaver.Chat.Clients;
using TwistedTaleweaver.Common;
using TwistedTaleweaver.DataAccess.Characters.Repositories;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.Extensions;
using TwistedTaleweaver.DataAccess.Expeditions.Entities.Enums;
using TwistedTaleweaver.DataAccess.Expeditions.Repositories;
using TwistedTaleweaver.DataAccess.Permissions.Entities.Enums;
using TwistedTaleweaver.DataAccess.Permissions.Repositories;
using TwistedTaleweaver.DataAccess.Streams.Repositories;
using TwistedTaleweaver.DataAccess.Users.Repositories;
using TwistedTaleweaver.Users.Clients;

namespace TwistedTaleweaver.Expeditions.Facades;

internal interface IExpeditionFacade : IFacade
{
    Task StartExpedition(string broadcasterExternalId, string userExternalId);
    
    Task JoinExpedition(string broadcasterExternalId, string userExternalId);
}

internal class ExpeditionFacade(
    IUserRepository userRepository,
    IExpeditionRepository expeditionRepository,
    IStreamRepository streamRepository,
    IUserBroadcasterPermissionRepository userBroadcasterPermissionRepository,
    ICharacterRepository characterRepository,
    IChatApiClient chatApiClient,
    IUserApiClient userApiClient,
    Func<IUnitOfWork> createUnitOfWork,
    ILogger<ExpeditionFacade> logger) : IExpeditionFacade
{
    public async Task StartExpedition(string broadcasterExternalId, string userExternalId)
    {
        await using var unitOfWork = createUnitOfWork();

        await unitOfWork.ExecuteInTransactionAsync(async transaction =>
        {
            var broadcaster = await userRepository.GetOrCreateAsync(broadcasterExternalId, transaction);
            var creator = await userRepository.GetOrCreateAsync(userExternalId, transaction);

            if (!await CanCreateExpedition(creator.UserId, broadcaster.UserId, transaction))
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

            // TODO: Check if expedition cooldown passed

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

    public async Task JoinExpedition(string broadcasterExternalId, string userExternalId)
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

    private async Task<bool> CanCreateExpedition(Guid userId, Guid broadcasterId, NpgsqlTransaction transaction)
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
}