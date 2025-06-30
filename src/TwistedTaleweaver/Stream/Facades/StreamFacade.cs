using Npgsql;
using TwistedTaleweaver.Chat.Clients;
using TwistedTaleweaver.Common;
using TwistedTaleweaver.DataAccess.Broadcasters.Repositories;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.Extensions;
using TwistedTaleweaver.DataAccess.Streams.Repositories;
using TwistedTaleweaver.DataAccess.Users.Entities;
using TwistedTaleweaver.DataAccess.Users.Repositories;

namespace TwistedTaleweaver.Stream.Facades;

internal interface IStreamFacade : IFacade
{
    Task StartStream(string externalBroadcasterId, string streamExternalId, DateTimeOffset startedAt);
    
    Task EndStream(string externalBroadcasterId);
}

internal class StreamFacade(
    IUserRepository userRepository,
    IStreamRepository streamRepository,
    IBroadcasterRepository broadcasterRepository,
    IChatApiClient chatApiClient,
    IDebouncer debouncer,
    Func<IUnitOfWork> createUnitOfWork,
    ILogger<StreamFacade> logger) : IStreamFacade
{
    private static readonly TimeSpan EndStreamGracePeriod = TimeSpan.FromSeconds(30);
    
    public async Task StartStream(string externalBroadcasterId, string streamExternalId, DateTimeOffset startedAt)
    {
        await using var unitOfWork = createUnitOfWork();

        await unitOfWork.ExecuteInTransactionAsync(async transaction =>
        {
            var broadcaster = await GetBroadcasterUserAsync(externalBroadcasterId, transaction);

            var endStreamKey = new DebouncerKey<Guid>(broadcaster.UserId, DebouncerActionType.EndStream);
            var pendingStreamEndCancelled = debouncer.TryCancelOperation(endStreamKey);
            
            var currentlyActiveStream = await streamRepository.GetActiveStreamAsync(broadcaster.UserId, transaction);

            if (currentlyActiveStream is not null && currentlyActiveStream.ExternalStreamId == streamExternalId)
            {
                logger.LogInformation("Stream start ignored (duplicate external stream ID {StreamExternalId})", streamExternalId);
                return;
            }
            
            if (currentlyActiveStream is not null)
            {
                await streamRepository.EndStreamAsync(currentlyActiveStream.StreamId, transaction);

                logger.LogWarning("Ended currently active stream for broadcaster {BroadcasterId} due to new stream start", broadcaster.UserId);
            }

            await streamRepository.StartStreamAsync(broadcaster.UserId, streamExternalId, startedAt, transaction);
            
            logger.LogDebug("Started new stream for broadcaster {BroadcasterId} with external ID {StreamExternalId}", broadcaster.UserId, streamExternalId);

            if (!pendingStreamEndCancelled)
            {
                await chatApiClient.SendChatMessageAsync(broadcaster.ExternalUserId,
                    "Ah… fresh footsteps in the dark. Welcome, little wanderers - I've been waiting far too long.");
            }
        });
    }

    public async Task EndStream(string externalBroadcasterId)
    {
        var broadcaster = await GetBroadcasterUserAsync(externalBroadcasterId);

        var currentlyActiveStream = await streamRepository.GetActiveStreamAsync(broadcaster.UserId);

        if (currentlyActiveStream is null)
        {
            logger.LogWarning("No active stream found for broadcaster {BroadcasterId} to end", broadcaster.UserId);
            return;
        }

        var endStreamKey = new DebouncerKey<Guid>(broadcaster.UserId, DebouncerActionType.EndStream);
        debouncer.ScheduleDelayedOperation(
            endStreamKey,
            EndStreamGracePeriod,
            () => ExecuteDelayedEndStream(broadcaster.UserId, currentlyActiveStream.StreamId, broadcaster.ExternalUserId));
    }
    
    private async Task ExecuteDelayedEndStream(Guid broadcasterUserId, Guid streamId, string externalUserId)
    {
        try
        {
            await using var unitOfWork = createUnitOfWork();

            await unitOfWork.ExecuteInTransactionAsync(async transaction =>
            {
                // Double-check the stream is still active before ending
                var currentlyActiveStream = await streamRepository.GetActiveStreamAsync(broadcasterUserId, transaction);
            
                if (currentlyActiveStream is null)
                {
                    logger.LogDebug("No active stream found for broadcaster {BroadcasterId}, skipping delayed end", 
                        broadcasterUserId);
                    return;
                }

                if (currentlyActiveStream.StreamId != streamId)
                {
                    logger.LogDebug("Stream {StreamId} for broadcaster {BroadcasterId} is no longer the active stream, skipping delayed end", 
                        streamId, broadcasterUserId);
                    return;
                }

                await streamRepository.EndStreamAsync(streamId, transaction);

                logger.LogDebug("Ended active stream for broadcaster {BroadcasterId} with stream ID {StreamId} after grace period", 
                    broadcasterUserId, streamId);
                
                await chatApiClient.SendChatMessageAsync(externalUserId,
                    "My watch ends… for now. Tread carefully, for when I return, so does the chaos.");
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing delayed end stream for broadcaster {BroadcasterId} and stream {StreamId}", 
                broadcasterUserId, streamId);
        
            // Don't rethrow - this is running in a background task
        }
    }

    private async Task<User> GetBroadcasterUserAsync(string externalBroadcasterId, NpgsqlTransaction? transaction = null)
    {
        var broadcasterUser = await userRepository.GetOrCreateAsync(externalBroadcasterId, transaction);
        
        // TODO: This really shouldn't be here as Broadcaster would always be created before subscribing
        await broadcasterRepository.GetOrCreateAsync(broadcasterUser.UserId, transaction);
        
        return broadcasterUser;
    }
}