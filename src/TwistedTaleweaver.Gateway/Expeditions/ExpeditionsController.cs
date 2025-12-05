using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwistedTaleweaver.Expeditions.CommandHandlers;
using TwistedTaleweaver.Expeditions.QueryHandlers;
using TwistedTaleweaver.Gateway.Authorization;
using TwistedTaleweaver.Gateway.Expeditions.Contracts;
using TwistedTaleweaver.Gateway.Expeditions.Mappers;

namespace TwistedTaleweaver.Gateway.Expeditions;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExpeditionsController(
    IUserProvider userProvider, 
    ICreateExpeditionCommandHandler createExpeditionCommandHandler,
    IExpeditionByIdQueryHandler expeditionByIdQueryHandler) : ControllerBase
{
    /// <summary>
    /// Creates a new expedition
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Expedition>> CreateExpedition([FromBody] ExpeditionRequest request)
    {
        var result = await createExpeditionCommandHandler.ExecuteAsync(new CreateExpeditionCommand()
        {
            Name = request.Name,
            Description = request.Description,
            Difficulty = request.Difficulty,
            MaxParticipants = request.MaxParticipants,
            ExternalUserId = userProvider.UserId
        });

        var expedition = await expeditionByIdQueryHandler.ExecuteAsync(new ExpeditionByIdQuery()
            { ExpeditionId = result.ExpeditionId });
        
        return Ok(expedition!.ToContract());
    }

    /// <summary>
    /// Gets an expedition by its unique identifier
    /// </summary>
    [HttpGet("{expeditionId:guid}")]
    public async Task<ActionResult<Expedition>> GetExpeditionById([FromRoute] Guid expeditionId)
    {
        var expedition = await expeditionByIdQueryHandler.ExecuteAsync(new ExpeditionByIdQuery()
            { ExpeditionId = expeditionId });

        if (expedition == null)
        {
            return NotFound();
        }

        return Ok(expedition.ToContract());
    }
}