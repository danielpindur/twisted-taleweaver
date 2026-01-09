using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Gateway.Auth;
using TwistedTaleweaver.Gateway.Expeditions.Mappers;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[ObjectType<ExpeditionOffsetPage>]
public static partial class ExpeditionOffsetPageNode
{
    [GraphQLDescription("Statistics about the expeditions.")]
    public static async Task<ExpeditionStatistics> GetStatisticsAsync(
        [Parent] ExpeditionOffsetPage offsetPage,
        [Service] IUserProvider userProvider,
        [Service] IExpeditionStatisticsQueryHandler expeditionStatisticsQueryHandler,
        CancellationToken cancellationToken)
    {
        var userId = await userProvider.GetUserIdAsync();
        
        // No need for a data loader since this is per page of results -> only one call is expected per query
        var statistics = await expeditionStatisticsQueryHandler.ExecuteAsync(
            new ExpeditionStatisticsQuery()
            {
                UserId = userId,
                Statuses = offsetPage.ExpeditionSearchInput.Statuses,
                Difficulty = offsetPage.ExpeditionSearchInput.Difficulty,
                Outcome = offsetPage.ExpeditionSearchInput.Outcome,
                SearchText = offsetPage.ExpeditionSearchInput.SearchText,
            });

        return statistics.ToContract();
    }
}