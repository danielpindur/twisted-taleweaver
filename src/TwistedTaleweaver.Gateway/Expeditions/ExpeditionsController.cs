using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwistedTaleweaver.Expeditions.CommandHandlers;
using TwistedTaleweaver.Expeditions.QueryHandlers;
using TwistedTaleweaver.Gateway.Authorization;
using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;
using TwistedTaleweaver.Gateway.Common.Pagination.Mappers;
using TwistedTaleweaver.Gateway.Expeditions.Contracts;
using TwistedTaleweaver.Gateway.Expeditions.Mappers;
using TwistedTaleweaver.Integration.Clerk.Users;
using ExpeditionSearchQuery = TwistedTaleweaver.Expeditions.QueryHandlers.ExpeditionSearchQuery;
using ExpeditionSearchQueryContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.ExpeditionSearchQuery;

namespace TwistedTaleweaver.Gateway.Expeditions;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExpeditionsController(
    IUserProvider userProvider, 
    Lazy<ICreateExpeditionCommandHandler> createExpeditionCommandHandler,
    Lazy<IExpeditionByIdQueryHandler> expeditionByIdQueryHandler,
    Lazy<IExpeditionSearchQueryHandler> expeditionSearchQueryHandler,
    Lazy<IUsersClient> usersClient) : ControllerBase
{
    /// <summary>
    /// Creates a new expedition
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Expedition>> CreateExpedition([FromBody] ExpeditionRequest request)
    {
        var user = await userProvider.GetUserAsync();
        
        var result = await createExpeditionCommandHandler.Value.ExecuteAsync(new CreateExpeditionCommand()
        {
            Name = request.Name,
            Description = request.Description,
            Difficulty = request.Difficulty,
            MaxParticipants = request.MaxParticipants,
            UserId = user.UserId
        });

        var expedition = await expeditionByIdQueryHandler.Value.ExecuteAsync(new ExpeditionByIdQuery()
            { ExpeditionId = result.ExpeditionId });

        var clerkUserByExternalId = await usersClient.Value
            .GetUsersAsync(expedition!.Participants.Select(x => x.ExternalUserId));
        
        return Ok(expedition.ToContract(clerkUserByExternalId));
    }

    /// <summary>
    /// Gets an expedition by its unique identifier
    /// </summary>
    [HttpGet("{expeditionId:guid}")]
    public async Task<ActionResult<Expedition>> GetExpeditionById([FromRoute] Guid expeditionId)
    {
        var expedition = await expeditionByIdQueryHandler.Value.ExecuteAsync(new ExpeditionByIdQuery()
            { ExpeditionId = expeditionId });

        if (expedition == null)
        {
            return NotFound();
        }
        
        var clerkUserByExternalId = await usersClient.Value
            .GetUsersAsync(expedition!.Participants.Select(x => x.ExternalUserId));

        return Ok(expedition.ToContract(clerkUserByExternalId));
    }

    /// <summary>
    /// Searches expeditions based on provided criteria
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<Expedition>>> SearchExpeditions([FromQuery]ExpeditionSearchQueryContract query)
    {
        var user = await userProvider.GetUserAsync();
        
        var paginatedExpeditions = await expeditionSearchQueryHandler.Value
            .ExecuteAsync(new ExpeditionSearchQuery()
            {
                UserId = user.UserId
            }.WithPagination(query));
        
        var externalUserIds = paginatedExpeditions.Items
            .SelectMany(x => x.Participants)
            .Select(x => x.ExternalUserId)
            .Distinct();
        
        var clerkUserByExternalId = await usersClient.Value
            .GetUsersAsync(externalUserIds);

        return Ok(paginatedExpeditions.ToContract(clerkUserByExternalId));
    }
}