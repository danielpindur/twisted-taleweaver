using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwistedTaleweaver.Api.Creatures.Commands;
using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Domain.Creatures;
using TwistedTaleweaver.Gateway.Authorization;
using TwistedTaleweaver.Gateway.Characters.Contracts;

namespace TwistedTaleweaver.Gateway.Characters;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class CharactersController(
    IUserProvider userProvider,
    Lazy<ICreateCharacterCommandHandler> createCharacterCommandHandler,
    Lazy<ICharacterByIdQueryHandler> characterByIdQueryHandler) : ControllerBase
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
        
        var result = await createCharacterCommandHandler.Value.ExecuteAsync(new CreateCharacterCommand()
        {
            UserId = userId,
            ExpeditionId = request.ExpeditionId,
            Name = request.Name,
            Description = request.Description,
            Class = request.Class,
            Alignment = request.Alignment,
            CreatureType = request.CreatureType,
            Gender = request.Gender,
            AttributeScores = request.AttributeScores,
        });

        
        var character = await characterByIdQueryHandler.Value.ExecuteAsync(new CharacterByIdQuery()
        {
            CharacterId = result.CharacterId
        });
        
        return Ok(character);
    }
}