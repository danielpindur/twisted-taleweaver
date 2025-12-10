using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwistedTaleweaver.Domain.Expeditions;
using TwistedTaleweaver.Expeditions.CommandHandlers;
using TwistedTaleweaver.Expeditions.QueryHandlers;
using TwistedTaleweaver.Gateway.Authorization;
using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;
using TwistedTaleweaver.Gateway.Common.Pagination.Mappers;
using TwistedTaleweaver.Gateway.Expeditions.Contracts;
using ExpeditionSearchQuery = TwistedTaleweaver.Expeditions.QueryHandlers.ExpeditionSearchQuery;
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
        var user = await userProvider.GetUserAsync();
        
        var paginatedExpeditions = await expeditionSearchQueryHandler.Value
            .ExecuteAsync(new ExpeditionSearchQuery()
            {
                UserId = user.UserId
            }.WithPagination(query));
        
        return Ok(paginatedExpeditions.ToContract());
    }

    /// <summary>
    /// Join an expedition
    /// </summary>
    [HttpPost("{expeditionId:guid}/join")]
    public async Task<ActionResult> JoinExpedition([FromRoute] Guid expeditionId)
    {
        var user = await userProvider.GetUserAsync();

        await joinExpeditionCommandHandler.Value.ExecuteAsync(new JoinExpeditionCommand()
        {
            ExpeditionId = expeditionId,
            UserId = user.UserId
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