using HotChocolate.Authorization;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Gateway.Auth;
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
        
        return expedition.ToNullableContract();
    }

    [GraphQLDescription("Retrieves a paginated list of expeditions.")]
    [Authorize]
    public static async Task<OffsetPage<ExpeditionContract>> GetExpeditionsAsync(
        OffsetInput input,
        [Service] IExpeditionSearchQueryHandler expeditionSearchQueryHandler,
        [Service] IUserProvider userProvider)
    {
        var userId = await userProvider.GetUserIdAsync();

        var paginatedExpeditions = await expeditionSearchQueryHandler.ExecuteAsync(
            new ExpeditionSearchQuery()
            {
                UserId = userId,
                PageNumber = input.Page,
                PageSize = input.PageSize
            });

        return paginatedExpeditions.ToContract(ExpeditionMapper.ToContract);
    }
}