// <auto-generated>
/*
 * Twitch API Swagger UI (Unofficial)
 *
 * Unofficial Swagger UI for Twitch API.  All endpoints are generated automatically from the [twitch docs](https://dev.twitch.tv/docs/api/reference) page.  __Features:__  * Swagger UI for all Twitch API endpoints * Schemas for _Request Query Parameters_, _Request Body_, _Response Body_ * Some additional schemas like _Clip_, _ChatBadge_, _Prediction_, _Game_, _Channel_, _Video_ etc. * Response codes and examples * Generated types for TypeScript: [ts-twitch-api](https://github.com/DmitryScaletta/ts-twitch-api)  __Repository:__ [github.com/DmitryScaletta/twitch-api-swagger](https://github.com/DmitryScaletta/twitch-api-swagger)
 *
 * The version of the OpenAPI document: helix
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

#nullable enable

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using TwistedTaleweaver.Integration.Twitch.Helix.Client;
using TwistedTaleweaver.Integration.Twitch.Helix.Model;
using System.Diagnostics.CodeAnalysis;

namespace TwistedTaleweaver.Integration.Twitch.Helix.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// This class is registered as transient.
    /// </summary>
    public interface IHypeTrainApi : IApi
    {
        /// <summary>
        /// The class containing the events
        /// </summary>
        HypeTrainApiEvents Events { get; }

        /// <summary>
        /// Gets information about the broadcaster’s current or most recent Hype Train event.
        /// </summary>
        /// <remarks>
        /// Gets information about the broadcaster’s current or most recent Hype Train event.  Instead of polling for events, consider [subscribing](https://dev.twitch.tv/docs/eventsub/manage-subscriptions) to Hype Train events ([Begin](https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#channelhype%5Ftrainbegin), [Progress](https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#channelhype%5Ftrainprogress), [End](https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#channelhype%5Ftrainend)).  __Authorization:__  Requires a [user access token](https://dev.twitch.tv/docs/authentication#user-access-tokens) that includes the **channel:read:hype\\_train** scope.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="broadcasterId">The ID of the broadcaster that’s running the Hype Train. This ID must match the User ID in the user access token.</param>
        /// <param name="first">The maximum number of items to return per page in the response. The minimum page size is 1 item per page and the maximum is 100 items per page. The default is 1. (optional)</param>
        /// <param name="after">The cursor used to get the next page of results. The **Pagination** object in the response contains the cursor’s value. [Read More](https://dev.twitch.tv/docs/api/guide#pagination) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="IGetHypeTrainEventsApiResponse"/>&gt;</returns>
        Task<IGetHypeTrainEventsApiResponse> GetHypeTrainEventsAsync(string broadcasterId, Option<int> first = default, Option<string> after = default, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets information about the broadcaster’s current or most recent Hype Train event.
        /// </summary>
        /// <remarks>
        /// Gets information about the broadcaster’s current or most recent Hype Train event.  Instead of polling for events, consider [subscribing](https://dev.twitch.tv/docs/eventsub/manage-subscriptions) to Hype Train events ([Begin](https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#channelhype%5Ftrainbegin), [Progress](https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#channelhype%5Ftrainprogress), [End](https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#channelhype%5Ftrainend)).  __Authorization:__  Requires a [user access token](https://dev.twitch.tv/docs/authentication#user-access-tokens) that includes the **channel:read:hype\\_train** scope.
        /// </remarks>
        /// <param name="broadcasterId">The ID of the broadcaster that’s running the Hype Train. This ID must match the User ID in the user access token.</param>
        /// <param name="first">The maximum number of items to return per page in the response. The minimum page size is 1 item per page and the maximum is 100 items per page. The default is 1. (optional)</param>
        /// <param name="after">The cursor used to get the next page of results. The **Pagination** object in the response contains the cursor’s value. [Read More](https://dev.twitch.tv/docs/api/guide#pagination) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="IGetHypeTrainEventsApiResponse"/>?&gt;</returns>
        Task<IGetHypeTrainEventsApiResponse?> GetHypeTrainEventsOrDefaultAsync(string broadcasterId, Option<int> first = default, Option<string> after = default, System.Threading.CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// The <see cref="IGetHypeTrainEventsApiResponse"/>
    /// </summary>
    public interface IGetHypeTrainEventsApiResponse : TwistedTaleweaver.Integration.Twitch.Helix.Client.IApiResponse, IOk<TwistedTaleweaver.Integration.Twitch.Helix.Model.GetHypeTrainEventsResponse?>
    {
        /// <summary>
        /// Returns true if the response is 200 Ok
        /// </summary>
        /// <returns></returns>
        bool IsOk { get; }

        /// <summary>
        /// Returns true if the response is 401 Unauthorized
        /// </summary>
        /// <returns></returns>
        bool IsUnauthorized { get; }
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class HypeTrainApiEvents
    {
        /// <summary>
        /// The event raised after the server response
        /// </summary>
        public event EventHandler<ApiResponseEventArgs>? OnGetHypeTrainEvents;

        /// <summary>
        /// The event raised after an error querying the server
        /// </summary>
        public event EventHandler<ExceptionEventArgs>? OnErrorGetHypeTrainEvents;

        internal void ExecuteOnGetHypeTrainEvents(HypeTrainApi.GetHypeTrainEventsApiResponse apiResponse)
        {
            OnGetHypeTrainEvents?.Invoke(this, new ApiResponseEventArgs(apiResponse));
        }

        internal void ExecuteOnErrorGetHypeTrainEvents(Exception exception)
        {
            OnErrorGetHypeTrainEvents?.Invoke(this, new ExceptionEventArgs(exception));
        }
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public sealed partial class HypeTrainApi : IHypeTrainApi
    {
        private JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// The logger factory
        /// </summary>
        public ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// The logger
        /// </summary>
        public ILogger<HypeTrainApi> Logger { get; }

        /// <summary>
        /// The HttpClient
        /// </summary>
        public HttpClient HttpClient { get; }

        /// <summary>
        /// The class containing the events
        /// </summary>
        public HypeTrainApiEvents Events { get; }

        /// <summary>
        /// A token provider of type <see cref="OauthTokenProvider"/>
        /// </summary>
        public TokenProvider<OAuthToken> OauthTokenProvider { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HypeTrainApi"/> class.
        /// </summary>
        /// <returns></returns>
        public HypeTrainApi(ILogger<HypeTrainApi> logger, ILoggerFactory loggerFactory, HttpClient httpClient, JsonSerializerOptionsProvider jsonSerializerOptionsProvider, HypeTrainApiEvents hypeTrainApiEvents,
            TokenProvider<OAuthToken> oauthTokenProvider)
        {
            _jsonSerializerOptions = jsonSerializerOptionsProvider.Options;
            LoggerFactory = loggerFactory;
            Logger = LoggerFactory.CreateLogger<HypeTrainApi>();
            HttpClient = httpClient;
            Events = hypeTrainApiEvents;
            OauthTokenProvider = oauthTokenProvider;
        }

        partial void FormatGetHypeTrainEvents(ref string broadcasterId, ref Option<int> first, ref Option<string> after);

        /// <summary>
        /// Validates the request parameters
        /// </summary>
        /// <param name="broadcasterId"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        private void ValidateGetHypeTrainEvents(string broadcasterId, Option<string> after)
        {
            if (broadcasterId == null)
                throw new ArgumentNullException(nameof(broadcasterId));

            if (after.IsSet && after.Value == null)
                throw new ArgumentNullException(nameof(after));
        }

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="broadcasterId"></param>
        /// <param name="first"></param>
        /// <param name="after"></param>
        private void AfterGetHypeTrainEventsDefaultImplementation(IGetHypeTrainEventsApiResponse apiResponseLocalVar, string broadcasterId, Option<int> first, Option<string> after)
        {
            bool suppressDefaultLog = false;
            AfterGetHypeTrainEvents(ref suppressDefaultLog, apiResponseLocalVar, broadcasterId, first, after);
            if (!suppressDefaultLog)
                Logger.LogInformation("{0,-9} | {1} | {3}", (apiResponseLocalVar.DownloadedAt - apiResponseLocalVar.RequestedAt).TotalSeconds, apiResponseLocalVar.StatusCode, apiResponseLocalVar.Path);
        }

        /// <summary>
        /// Processes the server response
        /// </summary>
        /// <param name="suppressDefaultLog"></param>
        /// <param name="apiResponseLocalVar"></param>
        /// <param name="broadcasterId"></param>
        /// <param name="first"></param>
        /// <param name="after"></param>
        partial void AfterGetHypeTrainEvents(ref bool suppressDefaultLog, IGetHypeTrainEventsApiResponse apiResponseLocalVar, string broadcasterId, Option<int> first, Option<string> after);

        /// <summary>
        /// Logs exceptions that occur while retrieving the server response
        /// </summary>
        /// <param name="exceptionLocalVar"></param>
        /// <param name="pathFormatLocalVar"></param>
        /// <param name="pathLocalVar"></param>
        /// <param name="broadcasterId"></param>
        /// <param name="first"></param>
        /// <param name="after"></param>
        private void OnErrorGetHypeTrainEventsDefaultImplementation(Exception exceptionLocalVar, string pathFormatLocalVar, string pathLocalVar, string broadcasterId, Option<int> first, Option<string> after)
        {
            bool suppressDefaultLogLocalVar = false;
            OnErrorGetHypeTrainEvents(ref suppressDefaultLogLocalVar, exceptionLocalVar, pathFormatLocalVar, pathLocalVar, broadcasterId, first, after);
            if (!suppressDefaultLogLocalVar)
                Logger.LogError(exceptionLocalVar, "An error occurred while sending the request to the server.");
        }

        /// <summary>
        /// A partial method that gives developers a way to provide customized exception handling
        /// </summary>
        /// <param name="suppressDefaultLogLocalVar"></param>
        /// <param name="exceptionLocalVar"></param>
        /// <param name="pathFormatLocalVar"></param>
        /// <param name="pathLocalVar"></param>
        /// <param name="broadcasterId"></param>
        /// <param name="first"></param>
        /// <param name="after"></param>
        partial void OnErrorGetHypeTrainEvents(ref bool suppressDefaultLogLocalVar, Exception exceptionLocalVar, string pathFormatLocalVar, string pathLocalVar, string broadcasterId, Option<int> first, Option<string> after);

        /// <summary>
        /// Gets information about the broadcaster’s current or most recent Hype Train event. Gets information about the broadcaster’s current or most recent Hype Train event.  Instead of polling for events, consider [subscribing](https://dev.twitch.tv/docs/eventsub/manage-subscriptions) to Hype Train events ([Begin](https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#channelhype%5Ftrainbegin), [Progress](https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#channelhype%5Ftrainprogress), [End](https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#channelhype%5Ftrainend)).  __Authorization:__  Requires a [user access token](https://dev.twitch.tv/docs/authentication#user-access-tokens) that includes the **channel:read:hype\\_train** scope.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that’s running the Hype Train. This ID must match the User ID in the user access token.</param>
        /// <param name="first">The maximum number of items to return per page in the response. The minimum page size is 1 item per page and the maximum is 100 items per page. The default is 1. (optional)</param>
        /// <param name="after">The cursor used to get the next page of results. The **Pagination** object in the response contains the cursor’s value. [Read More](https://dev.twitch.tv/docs/api/guide#pagination) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="IGetHypeTrainEventsApiResponse"/>&gt;</returns>
        public async Task<IGetHypeTrainEventsApiResponse?> GetHypeTrainEventsOrDefaultAsync(string broadcasterId, Option<int> first = default, Option<string> after = default, System.Threading.CancellationToken cancellationToken = default)
        {
            try
            {
                return await GetHypeTrainEventsAsync(broadcasterId, first, after, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets information about the broadcaster’s current or most recent Hype Train event. Gets information about the broadcaster’s current or most recent Hype Train event.  Instead of polling for events, consider [subscribing](https://dev.twitch.tv/docs/eventsub/manage-subscriptions) to Hype Train events ([Begin](https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#channelhype%5Ftrainbegin), [Progress](https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#channelhype%5Ftrainprogress), [End](https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#channelhype%5Ftrainend)).  __Authorization:__  Requires a [user access token](https://dev.twitch.tv/docs/authentication#user-access-tokens) that includes the **channel:read:hype\\_train** scope.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="broadcasterId">The ID of the broadcaster that’s running the Hype Train. This ID must match the User ID in the user access token.</param>
        /// <param name="first">The maximum number of items to return per page in the response. The minimum page size is 1 item per page and the maximum is 100 items per page. The default is 1. (optional)</param>
        /// <param name="after">The cursor used to get the next page of results. The **Pagination** object in the response contains the cursor’s value. [Read More](https://dev.twitch.tv/docs/api/guide#pagination) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="IGetHypeTrainEventsApiResponse"/>&gt;</returns>
        public async Task<IGetHypeTrainEventsApiResponse> GetHypeTrainEventsAsync(string broadcasterId, Option<int> first = default, Option<string> after = default, System.Threading.CancellationToken cancellationToken = default)
        {
            UriBuilder uriBuilderLocalVar = new UriBuilder();

            try
            {
                ValidateGetHypeTrainEvents(broadcasterId, after);

                FormatGetHypeTrainEvents(ref broadcasterId, ref first, ref after);

                using (HttpRequestMessage httpRequestMessageLocalVar = new HttpRequestMessage())
                {
                    uriBuilderLocalVar.Host = HttpClient.BaseAddress!.Host;
                    uriBuilderLocalVar.Port = HttpClient.BaseAddress.Port;
                    uriBuilderLocalVar.Scheme = HttpClient.BaseAddress.Scheme;
                    uriBuilderLocalVar.Path = HttpClient.BaseAddress.AbsolutePath == "/"
                        ? "/hypetrain/events"
                        : string.Concat(HttpClient.BaseAddress.AbsolutePath, "/hypetrain/events");

                    System.Collections.Specialized.NameValueCollection parseQueryStringLocalVar = System.Web.HttpUtility.ParseQueryString(string.Empty);

                    parseQueryStringLocalVar["broadcaster_id"] = ClientUtils.ParameterToString(broadcasterId);

                    if (first.IsSet)
                        parseQueryStringLocalVar["first"] = ClientUtils.ParameterToString(first.Value);

                    if (after.IsSet)
                        parseQueryStringLocalVar["after"] = ClientUtils.ParameterToString(after.Value);

                    uriBuilderLocalVar.Query = parseQueryStringLocalVar.ToString();

                    List<TokenBase> tokenBaseLocalVars = new List<TokenBase>();
                    httpRequestMessageLocalVar.RequestUri = uriBuilderLocalVar.Uri;

                    OAuthToken oauthTokenLocalVar1 = (OAuthToken) await OauthTokenProvider.GetAsync(cancellation: cancellationToken).ConfigureAwait(false);

                    tokenBaseLocalVars.Add(oauthTokenLocalVar1);

                    oauthTokenLocalVar1.UseInHeader(httpRequestMessageLocalVar, "");

                    string[] acceptLocalVars = new string[] {
                        "application/json"
                    };

                    string? acceptLocalVar = ClientUtils.SelectHeaderAccept(acceptLocalVars);

                    if (acceptLocalVar != null)
                        httpRequestMessageLocalVar.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptLocalVar));

                    httpRequestMessageLocalVar.Method = HttpMethod.Get;

                    DateTime requestedAtLocalVar = DateTime.UtcNow;

                    using (HttpResponseMessage httpResponseMessageLocalVar = await HttpClient.SendAsync(httpRequestMessageLocalVar, cancellationToken).ConfigureAwait(false))
                    {
                        string responseContentLocalVar = await httpResponseMessageLocalVar.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

                        ILogger<GetHypeTrainEventsApiResponse> apiResponseLoggerLocalVar = LoggerFactory.CreateLogger<GetHypeTrainEventsApiResponse>();

                        GetHypeTrainEventsApiResponse apiResponseLocalVar = new(apiResponseLoggerLocalVar, httpRequestMessageLocalVar, httpResponseMessageLocalVar, responseContentLocalVar, "/hypetrain/events", requestedAtLocalVar, _jsonSerializerOptions);

                        AfterGetHypeTrainEventsDefaultImplementation(apiResponseLocalVar, broadcasterId, first, after);

                        Events.ExecuteOnGetHypeTrainEvents(apiResponseLocalVar);

                        if (apiResponseLocalVar.StatusCode == (HttpStatusCode) 429)
                            foreach(TokenBase tokenBaseLocalVar in tokenBaseLocalVars)
                                tokenBaseLocalVar.BeginRateLimit();

                        return apiResponseLocalVar;
                    }
                }
            }
            catch(Exception e)
            {
                OnErrorGetHypeTrainEventsDefaultImplementation(e, "/hypetrain/events", uriBuilderLocalVar.Path, broadcasterId, first, after);
                Events.ExecuteOnErrorGetHypeTrainEvents(e);
                throw;
            }
        }

        /// <summary>
        /// The <see cref="GetHypeTrainEventsApiResponse"/>
        /// </summary>
        public partial class GetHypeTrainEventsApiResponse : TwistedTaleweaver.Integration.Twitch.Helix.Client.ApiResponse, IGetHypeTrainEventsApiResponse
        {
            /// <summary>
            /// The logger
            /// </summary>
            public ILogger<GetHypeTrainEventsApiResponse> Logger { get; }

            /// <summary>
            /// The <see cref="GetHypeTrainEventsApiResponse"/>
            /// </summary>
            /// <param name="logger"></param>
            /// <param name="httpRequestMessage"></param>
            /// <param name="httpResponseMessage"></param>
            /// <param name="rawContent"></param>
            /// <param name="path"></param>
            /// <param name="requestedAt"></param>
            /// <param name="jsonSerializerOptions"></param>
            public GetHypeTrainEventsApiResponse(ILogger<GetHypeTrainEventsApiResponse> logger, System.Net.Http.HttpRequestMessage httpRequestMessage, System.Net.Http.HttpResponseMessage httpResponseMessage, string rawContent, string path, DateTime requestedAt, System.Text.Json.JsonSerializerOptions jsonSerializerOptions) : base(httpRequestMessage, httpResponseMessage, rawContent, path, requestedAt, jsonSerializerOptions)
            {
                Logger = logger;
                OnCreated(httpRequestMessage, httpResponseMessage);
            }

            partial void OnCreated(global::System.Net.Http.HttpRequestMessage httpRequestMessage, System.Net.Http.HttpResponseMessage httpResponseMessage);

            /// <summary>
            /// Returns true if the response is 200 Ok
            /// </summary>
            /// <returns></returns>
            public bool IsOk => 200 == (int)StatusCode;

            /// <summary>
            /// Deserializes the response if the response is 200 Ok
            /// </summary>
            /// <returns></returns>
            public TwistedTaleweaver.Integration.Twitch.Helix.Model.GetHypeTrainEventsResponse? Ok()
            {
                // This logic may be modified with the AsModel.mustache template
                return IsOk
                    ? System.Text.Json.JsonSerializer.Deserialize<TwistedTaleweaver.Integration.Twitch.Helix.Model.GetHypeTrainEventsResponse>(RawContent, _jsonSerializerOptions)
                    : null;
            }

            /// <summary>
            /// Returns true if the response is 200 Ok and the deserialized response is not null
            /// </summary>
            /// <param name="result"></param>
            /// <returns></returns>
            public bool TryOk([NotNullWhen(true)]out TwistedTaleweaver.Integration.Twitch.Helix.Model.GetHypeTrainEventsResponse? result)
            {
                result = null;

                try
                {
                    result = Ok();
                } catch (Exception e)
                {
                    OnDeserializationErrorDefaultImplementation(e, (HttpStatusCode)200);
                }

                return result != null;
            }

            /// <summary>
            /// Returns true if the response is 401 Unauthorized
            /// </summary>
            /// <returns></returns>
            public bool IsUnauthorized => 401 == (int)StatusCode;

            private void OnDeserializationErrorDefaultImplementation(Exception exception, HttpStatusCode httpStatusCode)
            {
                bool suppressDefaultLog = false;
                OnDeserializationError(ref suppressDefaultLog, exception, httpStatusCode);
                if (!suppressDefaultLog)
                    Logger.LogError(exception, "An error occurred while deserializing the {code} response.", httpStatusCode);
            }

            partial void OnDeserializationError(ref bool suppressDefaultLog, Exception exception, HttpStatusCode httpStatusCode);
        }
    }
}
