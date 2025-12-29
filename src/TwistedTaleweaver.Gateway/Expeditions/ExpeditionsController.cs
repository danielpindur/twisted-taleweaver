// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using TwistedTaleweaver.Api.Expeditions.Commands;
// using TwistedTaleweaver.Api.Expeditions.Queries;
// using TwistedTaleweaver.Domain.Expeditions;
// using TwistedTaleweaver.Gateway.Authorization;
// using TwistedTaleweaver.Gateway.Common;
// using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;
// using TwistedTaleweaver.Gateway.Common.Pagination.Mappers;
// using TwistedTaleweaver.Gateway.Expeditions.Contracts;
// using ExpeditionSearchQuery = TwistedTaleweaver.Api.Expeditions.Queries.ExpeditionSearchQuery;
// using ExpeditionSearchQueryContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.ExpeditionSearchQuery;
// using Expedition = TwistedTaleweaver.Domain.Expeditions.Expedition;
//
// namespace TwistedTaleweaver.Gateway.Expeditions;
//
// [ApiController]
// [Route($"{ApiConstants.UriPrefix}/[controller]")]
// [Authorize]
// public sealed class ExpeditionsController(
//     IUserProvider userProvider,
//     Lazy<ICreateExpeditionCommandHandler> createExpeditionCommandHandler,
//     Lazy<IExpeditionByIdQueryHandler> expeditionByIdQueryHandler,
//     Lazy<IExpeditionSearchQueryHandler> expeditionSearchQueryHandler,
//     Lazy<IJoinExpeditionCommandHandler> joinExpeditionCommandHandler,
//     Lazy<IBeginExpeditionCommandHandler> beginExpeditionCommandHandler) : ControllerBase
// {
//     /// <summary>
//     /// Creates a new expedition
//     /// </summary>
//     [HttpPost]
//     [ProducesResponseType(typeof(ExpeditionSlim), StatusCodes.Status201Created)]
//     [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
//     [ProducesResponseType(StatusCodes.Status403Forbidden)]
//     public async Task<ActionResult<ExpeditionSlim>> CreateExpedition([FromBody] ExpeditionRequest request)
//     {
//         var userId = await userProvider.GetUserIdAsync();
//
//         var result = await createExpeditionCommandHandler.Value.ExecuteAsync(new CreateExpeditionCommand()
//         {
//             Name = request.Name,
//             Description = request.Description,
//             Difficulty = request.Difficulty,
//             MinParticipants = request.MinParticipants,
//             MaxParticipants = request.MaxParticipants,
//             UserId = userId
//         });
//
//         var expedition = await expeditionByIdQueryHandler.Value.ExecuteAsync(new ExpeditionByIdQuery()
//         {
//             ExpeditionId = result.ExpeditionId
//         });
//
//         return Ok(expedition);
//     }
//
//     /// <summary>
//     /// Gets an expedition by its unique identifier
//     /// </summary>
//     [HttpGet("{expeditionId:guid}")]
//     [ProducesResponseType(typeof(Expedition), StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status404NotFound)]
//     [ProducesResponseType(StatusCodes.Status403Forbidden)]
//     public async Task<ActionResult<Expedition>> GetExpeditionById([FromRoute] Guid expeditionId)
//     {
//         var expedition = await expeditionByIdQueryHandler.Value.ExecuteAsync(
//             new ExpeditionByIdQuery()
//             {
//                 ExpeditionId = expeditionId
//             });
//
//         if (expedition == null)
//         {
//             return NotFound();
//         }
//
//         return Ok(expedition);
//     }
//
//     /// <summary>
//     /// Searches expeditions based on provided criteria
//     /// </summary>
//     [HttpGet]
//     [ProducesResponseType(typeof(PaginatedResponse<ExpeditionSlim>), StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status403Forbidden)]
//     public async Task<ActionResult<PaginatedResponse<ExpeditionSlim>>> SearchExpeditions([FromQuery] ExpeditionSearchQueryContract query)
//     {
//         var userId = await userProvider.GetUserIdAsync();
//
//         var paginatedExpeditions = await expeditionSearchQueryHandler.Value
//             .ExecuteAsync(new ExpeditionSearchQuery()
//             {
//                 UserId = userId
//             }.WithPagination(query));
//
//         return Ok(paginatedExpeditions.ToContract());
//     }
//
//     /// <summary>
//     /// Join an expedition
//     /// </summary>
//     [HttpPost("{expeditionId:guid}/join")]
//     [ProducesResponseType(StatusCodes.Status204NoContent)]
//     [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
//     [ProducesResponseType(StatusCodes.Status403Forbidden)]
//     public async Task<ActionResult> JoinExpedition([FromRoute] Guid expeditionId)
//     {
//         var userId = await userProvider.GetUserIdAsync();
//
//         await joinExpeditionCommandHandler.Value.ExecuteAsync(new JoinExpeditionCommand()
//         {
//             ExpeditionId = expeditionId,
//             UserId = userId
//         });
//
//         return NoContent();
//     }
//
//     /// <summary>
//     /// Begin an expedition
//     /// </summary>
//     [HttpPost("{expeditionId:guid}/begin")]
//     [ProducesResponseType(StatusCodes.Status204NoContent)]
//     [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
//     [ProducesResponseType(StatusCodes.Status403Forbidden)]
//     public async Task<ActionResult> BeginExpedition([FromRoute] Guid expeditionId)
//     {
//         var userId = await userProvider.GetUserIdAsync();
//
//         await beginExpeditionCommandHandler.Value.ExecuteAsync(new BeginExpeditionCommand()
//         {
//             ExpeditionId = expeditionId,
//             UserId = userId
//         });
//
//         return NoContent();
//     }
// }