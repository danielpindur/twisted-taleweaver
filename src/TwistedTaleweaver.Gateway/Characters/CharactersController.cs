using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwistedTaleweaver.Domain.Creatures;
using TwistedTaleweaver.Gateway.Authorization;
using TwistedTaleweaver.Gateway.Characters.Contracts;

namespace TwistedTaleweaver.Gateway.Characters;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class CharactersController(IUserProvider userProvider) : ControllerBase
{
    /// <summary>
    /// Creates a new character for an expedition participant
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Character), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Character>> CreateCharacter([FromBody] CharacterRequest request)
    {
        var userId = await userProvider.GetUserIdAsync();
        
        /*
        var result = await createExpeditionCommandHandler.Value.ExecuteAsync(new CreateExpeditionCommand()
        {
            Name = request.Name,
            Description = request.Description,
            Difficulty = request.Difficulty,
            MinParticipants = request.MinParticipants,
            MaxParticipants = request.MaxParticipants,
            UserId = userId
        });

        var character = await expeditionByIdQueryHandler.Value.ExecuteAsync(new ExpeditionByIdQuery()
        {
            ExpeditionId = result.ExpeditionId,
            UserId = userId
        });
        */
        
        return Ok(null);
    }
}