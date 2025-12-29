// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using TwistedTaleweaver.Api.Messages.Queries;
// using TwistedTaleweaver.Domain.Messages;
// using TwistedTaleweaver.Gateway.Common;
// using TwistedTaleweaver.Gateway.Messages.Contracts;
//
// namespace TwistedTaleweaver.Gateway.Messages;
//
// [ApiController]
// [Route($"{ApiConstants.UriPrefix}/[controller]")]
// [Authorize]
// public class MessagesController(
//     IExpeditionMessagesSearchQueryHandler expeditionMessagesSearchQueryHandler) : ControllerBase
// {
//     /// <summary>
//     /// Expedition chat messages search
//     /// </summary>
//     [HttpGet]
//     [ProducesResponseType(typeof(List<ChatMessage>), StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status403Forbidden)]
//     public async Task<ActionResult<List<ChatMessage>>> SearchMessages([FromQuery] MessageSearchRequest query)
//     {
//         var searchQuery = new ExpeditionMessagesSearchQuery()
//         {
//             ExpeditionId = query.ExpeditionId,
//             LastExpeditionMessageId = query.LastExpeditionMessageId
//         };
//
//         if (query.PageSize.HasValue)
//         {
//             searchQuery.PageSize = query.PageSize.Value;
//         }
//
//         var expeditionMessages = await expeditionMessagesSearchQueryHandler
//             .ExecuteAsync(searchQuery);
//
//         return Ok(expeditionMessages.ToList());
//     }
// }