using HotChocolate.Authorization;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Gateway.Authorization;
using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;
using TwistedTaleweaver.Gateway.Common.Pagination.Mappers;
using TwistedTaleweaver.Gateway.Expeditions.Mappers;
using ExpeditionContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.Expedition;

namespace TwistedTaleweaver.Gateway.Expeditions;

[QueryType]
public static class ExpeditionsQueries
{
    [GraphQLDescription("Retrieves an expedition by its unique identifier.")]
    [Authorize]
    public static async Task<ExpeditionContract?> GetExpeditionByIdAsync(
        Guid id,
        [Service] IExpeditionByIdQueryHandler expeditionByIdQueryHandler)
    {
        var expedition = await expeditionByIdQueryHandler.ExecuteAsync(
            new ExpeditionByIdQuery()
            {
                ExpeditionId = id
            });
        
        return expedition.ToContract();
    }

    [GraphQLDescription("Retrieves a paginated list of expeditions.")]
    [Authorize]
    public static async Task<OffsetPage<ExpeditionContract>> GetExpeditionsAsync(
        [Service] IExpeditionSearchQueryHandler expeditionSearchQueryHandler,
        [Service] IUserProvider userProvider,
        int page = 1,
        int pageSize = 20)
    {
        var userId = await userProvider.GetUserIdAsync();

        var paginatedExpeditions = await expeditionSearchQueryHandler.ExecuteAsync(
            new ExpeditionSearchQuery()
            {
                UserId = userId,
                PageNumber = page,
                PageSize = pageSize
            });

        return paginatedExpeditions.ToContract(ExpeditionMapper.ToContract);
    }
}