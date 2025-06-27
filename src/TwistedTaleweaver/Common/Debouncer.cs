using System.Collections.Concurrent;

namespace TwistedTaleweaver.Common;

internal enum DebouncerActionType
{
    EndStream
}

internal record DebouncerKey<TKey>(TKey Key, DebouncerActionType ActionType) where TKey : notnull;

internal interface IDebouncer
{
    void ScheduleDelayedOperation<TKey>(
        DebouncerKey<TKey> key,
        TimeSpan delay, 
        Func<Task> operation) where TKey : notnull;
    
    bool TryCancelOperation<TKey>(DebouncerKey<TKey> key) where TKey : notnull;
    
    void ScheduleDelayedOperation<TKey>(
        DebouncerKey<TKey> key,
        TimeSpan delay, 
        Func<TKey, Task> operation) where TKey : notnull;
}

internal class Debouncer(ILogger<Debouncer> logger) : IDebouncer
{
    private readonly ConcurrentDictionary<object, CancellationTokenSource> _pendingOperations = new();

    public void ScheduleDelayedOperation<TKey>(
        DebouncerKey<TKey> key,
        TimeSpan delay, 
        Func<Task> operation) where TKey : notnull
    {
        ScheduleDelayedOperationInternal(key, delay, operation);
    }

    public void ScheduleDelayedOperation<TKey>(
        DebouncerKey<TKey> key,
        TimeSpan delay, 
        Func<TKey, Task> operation) where TKey : notnull
    {
        ScheduleDelayedOperationInternal(key, delay, () => operation(key.Key));
    }

    public bool TryCancelOperation<TKey>(DebouncerKey<TKey> key) where TKey : notnull
    {
        if (_pendingOperations.TryRemove(key, out var cts))
        {
            cts.Cancel();
            cts.Dispose();
            
            logger.LogDebug("Cancelled pending operation {ActionType} for key {Key}", 
                key.ActionType, key.Key);
            
            return true;
        }
        
        return false;
    }

    private void ScheduleDelayedOperationInternal<TKey>(
        DebouncerKey<TKey> key,
        TimeSpan delay, 
        Func<Task> operation) where TKey : notnull
    {
        // Cancel any existing operation for this key
        if (_pendingOperations.TryRemove(key, out var existingCts))
        {
            existingCts.Cancel();
            existingCts.Dispose();
            
            logger.LogDebug("Replaced pending operation {ActionType} for key {Key}", 
                key.ActionType, key.Key);
        }

        var cts = new CancellationTokenSource();
        _pendingOperations[key] = cts;

        logger.LogDebug("Scheduling delayed operation {ActionType} for key {Key} with {Delay}ms delay", 
            key.ActionType, key.Key, delay.TotalMilliseconds);

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(delay, cts.Token);
                
                // Execute the operation
                await operation();
                
                // Clean up
                _pendingOperations.TryRemove(key, out _);
                
                logger.LogDebug("Executed delayed operation {ActionType} for key {Key}", 
                    key.ActionType, key.Key);
            }
            catch (OperationCanceledException)
            {
                logger.LogDebug("Delayed operation {ActionType} was cancelled previously for key {Key}", 
                    key.ActionType, key.Key);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing delayed operation {ActionType} for key {Key}", 
                    key.ActionType, key.Key);
                
                _pendingOperations.TryRemove(key, out _);
            }
            finally
            {
                cts.Dispose();
            }
        }, cts.Token);
    }
}