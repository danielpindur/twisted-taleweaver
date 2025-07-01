using Npgsql;
using TwistedTaleweaver.Chat.Clients;
using TwistedTaleweaver.Common;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.Extensions;
using TwistedTaleweaver.DataAccess.Expeditions.Repositories;
using TwistedTaleweaver.DataAccess.Permissions.Entities.Enums;
using TwistedTaleweaver.DataAccess.Permissions.Repositories;
using TwistedTaleweaver.DataAccess.Streams.Repositories;
using TwistedTaleweaver.DataAccess.Users.Repositories;

namespace TwistedTaleweaver.Expeditions.Facades;

internal interface IExpeditionFacade : IFacade
{
    Task StartExpedition(string broadcasterExternalId, string userExternalId);
}

internal class ExpeditionFacade(
    IUserRepository userRepository,
    IExpeditionRepository expeditionRepository,
    IStreamRepository streamRepository,
    IUserBroadcasterPermissionRepository userBroadcasterPermissionRepository,
    IChatApiClient chatApiClient,
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