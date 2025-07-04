using System.Diagnostics;
using TwistedTaleweaver.Expeditions.Facades;

namespace TwistedTaleweaver.Expeditions.Tasks;

internal class ExpeditionBackgroundService(
    IServiceProvider serviceProvider,
    ILogger<ExpeditionBackgroundService> logger) : BackgroundService
{
    private static readonly TimeSpan ProcessingInterval = TimeSpan.FromSeconds(5);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Expedition background service started with processing interval of {IntervalMilliseconds}ms",
            ProcessingInterval.TotalMilliseconds);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                
                using var scope = serviceProvider.CreateScope();
                var expeditionFacade = scope.ServiceProvider.GetRequiredService<IExpeditionFacade>();
                await expeditionFacade.ProcessExpeditionsAsync();
                
                stopwatch.Stop();

                if (stopwatch.Elapsed > ProcessingInterval * .8)
                {
                    logger.LogWarning(
                        "Expedition processing took {ElapsedMilliseconds}ms, which is longer than 80% the configured interval of {IntervalMilliseconds}ms",
                        stopwatch.ElapsedMilliseconds,
                        ProcessingInterval.TotalMilliseconds);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing expeditions in the background service");
            }

            await Task.Delay(ProcessingInterval, stoppingToken);
        }
    }
}