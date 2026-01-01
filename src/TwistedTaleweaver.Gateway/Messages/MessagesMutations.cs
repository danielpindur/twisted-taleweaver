using HotChocolate.Authorization;
using TwistedTaleweaver.Api.Messages.Commands;
using TwistedTaleweaver.Gateway.Auth;
using TwistedTaleweaver.Gateway.Messages.Contracts;
using TwistedTaleweaver.Gateway.Messages.Mappers;

namespace TwistedTaleweaver.Gateway.Messages;

[MutationType]
public static class MessagesMutations
{
    [GraphQLDescription("Creates a new message in an expedition.")]
    [Authorize]
    public static async Task<ChatMessage> AddMessageAsync(
        AddMessageInput input,
        [Service] IUserProvider userProvider,
        [Service] IChatMessagesCommandHandler chatMessagesCommandHandler)
    {
        var userId = await userProvider.GetUserIdAsync();

        var chatMessage = await chatMessagesCommandHandler.ExecuteAsync(
            new AddCharacterMessageCommand()
            {
                ExpeditionId = input.ExpeditionId,
                UserId = userId,
                Content = input.Content
            });

        return chatMessage.ToContract();
    }
}