using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Domain.Expeditions;
using TwistedTaleweaver.Gateway.Authorization;
using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;
using TwistedTaleweaver.Gateway.Common.Pagination.Mappers;
using TwistedTaleweaver.Gateway.Expeditions.Contracts;
using ExpeditionSearchQuery = TwistedTaleweaver.Api.Expeditions.Queries.ExpeditionSearchQuery;
using ExpeditionSearchQueryContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.ExpeditionSearchQuery;

namespace TwistedTaleweaver.Gateway.Expeditions;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ExpeditionsController(
    IUserProvider userProvider, 
    Lazy<ICreateExpeditionCommandHandler> createExpeditionCommandHandler,
    Lazy<IExpeditionByIdQueryHandler> expeditionByIdQueryHandler,
    Lazy<IExpeditionSearchQueryHandler> expeditionSearchQueryHandler,
    Lazy<IJoinExpeditionCommandHandler> joinExpeditionCommandHandler,
    Lazy<IExpeditionParticipantsQueryHandler> expeditionParticipantsQueryHandler) : ControllerBase
{
    /// <summary>
    /// Creates a new expedition
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ExpeditionSlim>> CreateExpedition([FromBody] ExpeditionRequest request)
    {
        var userId = await userProvider.GetUserIdAsync();
        
        var result = await createExpeditionCommandHandler.Value.ExecuteAsync(new CreateExpeditionCommand()
        {
            Name = request.Name,
            Description = request.Description,
            Difficulty = request.Difficulty,
            MaxParticipants = request.MaxParticipants,
            UserId = userId
        });

        var expedition = await expeditionByIdQueryHandler.Value.ExecuteAsync(new ExpeditionByIdQuery()
            { ExpeditionId = result.ExpeditionId });
        
        return Ok(expedition);
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
        
        return Ok(expedition);
    }

    /// <summary>
    /// Searches expeditions based on provided criteria
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<ExpeditionSlim>>> SearchExpeditions([FromQuery]ExpeditionSearchQueryContract query)
    {
        var userId = await userProvider.GetUserIdAsync();
        
        var paginatedExpeditions = await expeditionSearchQueryHandler.Value
            .ExecuteAsync(new ExpeditionSearchQuery()
            {
                UserId = userId
            }.WithPagination(query));
        
        return Ok(paginatedExpeditions.ToContract());
    }

    /// <summary>
    /// Join an expedition
    /// </summary>
    [HttpPost("{expeditionId:guid}/join")]
    public async Task<ActionResult> JoinExpedition([FromRoute] Guid expeditionId)
    {
        var userId = await userProvider.GetUserIdAsync();

        await joinExpeditionCommandHandler.Value.ExecuteAsync(new JoinExpeditionCommand()
        {
            ExpeditionId = expeditionId,
            UserId = userId
        });

        var expeditionParticipants = (await expeditionParticipantsQueryHandler.Value
            .ExecuteAsync(new ExpeditionParticipantsQuery() { ExpeditionId = expeditionId }));

        /*
        await expeditionHub.Value.Clients.Group(expeditionId.ToString())
            .ParticipantsUpdated(expeditionParticipants.ToContract());
        */
        
        return Ok();
    }
}