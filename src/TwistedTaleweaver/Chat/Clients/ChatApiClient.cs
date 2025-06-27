using TwistedTaleweaver.Core.Common.Configuration;
using TwistedTaleweaver.Common;
using TwistedTaleweaver.Integration.Twitch.Helix.Api;
using TwistedTaleweaver.Integration.Twitch.Helix.Model;
using Microsoft.Extensions.Options;

namespace TwistedTaleweaver.Chat.Clients;

internal interface IChatApiClient : IApiClient
{
    Task SendChatMessageAsync(string broadcasterExternalIdentifier, string message);
}

internal class ChatApiClient(
    IChatApi chatApi,
    IOptions<TwistedTaleweaverConfiguration> configuration) : IChatApiClient
{
    private readonly string _botUserId = configuration.Value.BotUserId;
    
    public async Task SendChatMessageAsync(string broadcasterExternalIdentifier, string message)
    {
        // TODO: There is a max length of chat message, we should take that into account, don't split words
        var requestBody = new SendChatMessageBody(broadcasterExternalIdentifier, _botUserId, message);
        var response = await chatApi.SendChatMessageAsync(requestBody);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to send chat message: {response.RawContent}");
        }
    }
}