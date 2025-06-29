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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using TwistedTaleweaver.Integration.Twitch.Helix.Client;

namespace TwistedTaleweaver.Integration.Twitch.Helix.Model
{
    /// <summary>
    /// The broadcaster’s streaming schedule.
    /// </summary>
    public partial class GetChannelStreamScheduleResponseData : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetChannelStreamScheduleResponseData" /> class.
        /// </summary>
        /// <param name="segments">The list of broadcasts in the broadcaster’s streaming schedule.</param>
        /// <param name="broadcasterId">The ID of the broadcaster that owns the broadcast schedule.</param>
        /// <param name="broadcasterName">The broadcaster’s display name.</param>
        /// <param name="broadcasterLogin">The broadcaster’s login name.</param>
        /// <param name="vacation">vacation</param>
        /// <param name="pagination">pagination</param>
        [JsonConstructor]
        public GetChannelStreamScheduleResponseData(List<ChannelStreamScheduleSegment> segments, string broadcasterId, string broadcasterName, string broadcasterLogin, GetChannelStreamScheduleResponseDataVacation vacation, Option<GetChannelStreamScheduleResponseDataPagination?> pagination = default)
        {
            Segments = segments;
            BroadcasterId = broadcasterId;
            BroadcasterName = broadcasterName;
            BroadcasterLogin = broadcasterLogin;
            Vacation = vacation;
            PaginationOption = pagination;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The list of broadcasts in the broadcaster’s streaming schedule.
        /// </summary>
        /// <value>The list of broadcasts in the broadcaster’s streaming schedule.</value>
        [JsonPropertyName("segments")]
        public List<ChannelStreamScheduleSegment> Segments { get; set; }

        /// <summary>
        /// The ID of the broadcaster that owns the broadcast schedule.
        /// </summary>
        /// <value>The ID of the broadcaster that owns the broadcast schedule.</value>
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; set; }

        /// <summary>
        /// The broadcaster’s display name.
        /// </summary>
        /// <value>The broadcaster’s display name.</value>
        [JsonPropertyName("broadcaster_name")]
        public string BroadcasterName { get; set; }

        /// <summary>
        /// The broadcaster’s login name.
        /// </summary>
        /// <value>The broadcaster’s login name.</value>
        [JsonPropertyName("broadcaster_login")]
        public string BroadcasterLogin { get; set; }

        /// <summary>
        /// Gets or Sets Vacation
        /// </summary>
        [JsonPropertyName("vacation")]
        public GetChannelStreamScheduleResponseDataVacation Vacation { get; set; }

        /// <summary>
        /// Used to track the state of Pagination
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<GetChannelStreamScheduleResponseDataPagination?> PaginationOption { get; private set; }

        /// <summary>
        /// Gets or Sets Pagination
        /// </summary>
        [JsonPropertyName("pagination")]
        public GetChannelStreamScheduleResponseDataPagination? Pagination { get { return this.PaginationOption; } set { this.PaginationOption = new(value); } }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetChannelStreamScheduleResponseData {\n");
            sb.Append("  Segments: ").Append(Segments).Append("\n");
            sb.Append("  BroadcasterId: ").Append(BroadcasterId).Append("\n");
            sb.Append("  BroadcasterName: ").Append(BroadcasterName).Append("\n");
            sb.Append("  BroadcasterLogin: ").Append(BroadcasterLogin).Append("\n");
            sb.Append("  Vacation: ").Append(Vacation).Append("\n");
            sb.Append("  Pagination: ").Append(Pagination).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

    /// <summary>
    /// A Json converter for type <see cref="GetChannelStreamScheduleResponseData" />
    /// </summary>
    public class GetChannelStreamScheduleResponseDataJsonConverter : JsonConverter<GetChannelStreamScheduleResponseData>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetChannelStreamScheduleResponseData" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetChannelStreamScheduleResponseData Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<List<ChannelStreamScheduleSegment>?> segments = default;
            Option<string?> broadcasterId = default;
            Option<string?> broadcasterName = default;
            Option<string?> broadcasterLogin = default;
            Option<GetChannelStreamScheduleResponseDataVacation?> vacation = default;
            Option<GetChannelStreamScheduleResponseDataPagination?> pagination = default;

            while (utf8JsonReader.Read())
            {
                if (startingTokenType == JsonTokenType.StartObject && utf8JsonReader.TokenType == JsonTokenType.EndObject && currentDepth == utf8JsonReader.CurrentDepth)
                    break;

                if (startingTokenType == JsonTokenType.StartArray && utf8JsonReader.TokenType == JsonTokenType.EndArray && currentDepth == utf8JsonReader.CurrentDepth)
                    break;

                if (utf8JsonReader.TokenType == JsonTokenType.PropertyName && currentDepth == utf8JsonReader.CurrentDepth - 1)
                {
                    string? localVarJsonPropertyName = utf8JsonReader.GetString();
                    utf8JsonReader.Read();

                    switch (localVarJsonPropertyName)
                    {
                        case "segments":
                            segments = new Option<List<ChannelStreamScheduleSegment>?>(JsonSerializer.Deserialize<List<ChannelStreamScheduleSegment>>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        case "broadcaster_id":
                            broadcasterId = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "broadcaster_name":
                            broadcasterName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "broadcaster_login":
                            broadcasterLogin = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "vacation":
                            vacation = new Option<GetChannelStreamScheduleResponseDataVacation?>(JsonSerializer.Deserialize<GetChannelStreamScheduleResponseDataVacation>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        case "pagination":
                            pagination = new Option<GetChannelStreamScheduleResponseDataPagination?>(JsonSerializer.Deserialize<GetChannelStreamScheduleResponseDataPagination>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!segments.IsSet)
                throw new ArgumentException("Property is required for class GetChannelStreamScheduleResponseData.", nameof(segments));

            if (!broadcasterId.IsSet)
                throw new ArgumentException("Property is required for class GetChannelStreamScheduleResponseData.", nameof(broadcasterId));

            if (!broadcasterName.IsSet)
                throw new ArgumentException("Property is required for class GetChannelStreamScheduleResponseData.", nameof(broadcasterName));

            if (!broadcasterLogin.IsSet)
                throw new ArgumentException("Property is required for class GetChannelStreamScheduleResponseData.", nameof(broadcasterLogin));

            if (!vacation.IsSet)
                throw new ArgumentException("Property is required for class GetChannelStreamScheduleResponseData.", nameof(vacation));

            if (segments.IsSet && segments.Value == null)
                throw new ArgumentNullException(nameof(segments), "Property is not nullable for class GetChannelStreamScheduleResponseData.");

            if (broadcasterId.IsSet && broadcasterId.Value == null)
                throw new ArgumentNullException(nameof(broadcasterId), "Property is not nullable for class GetChannelStreamScheduleResponseData.");

            if (broadcasterName.IsSet && broadcasterName.Value == null)
                throw new ArgumentNullException(nameof(broadcasterName), "Property is not nullable for class GetChannelStreamScheduleResponseData.");

            if (broadcasterLogin.IsSet && broadcasterLogin.Value == null)
                throw new ArgumentNullException(nameof(broadcasterLogin), "Property is not nullable for class GetChannelStreamScheduleResponseData.");

            if (vacation.IsSet && vacation.Value == null)
                throw new ArgumentNullException(nameof(vacation), "Property is not nullable for class GetChannelStreamScheduleResponseData.");

            if (pagination.IsSet && pagination.Value == null)
                throw new ArgumentNullException(nameof(pagination), "Property is not nullable for class GetChannelStreamScheduleResponseData.");

            return new GetChannelStreamScheduleResponseData(segments.Value!, broadcasterId.Value!, broadcasterName.Value!, broadcasterLogin.Value!, vacation.Value!, pagination);
        }

        /// <summary>
        /// Serializes a <see cref="GetChannelStreamScheduleResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getChannelStreamScheduleResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetChannelStreamScheduleResponseData getChannelStreamScheduleResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, getChannelStreamScheduleResponseData, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetChannelStreamScheduleResponseData" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getChannelStreamScheduleResponseData"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, GetChannelStreamScheduleResponseData getChannelStreamScheduleResponseData, JsonSerializerOptions jsonSerializerOptions)
        {
            if (getChannelStreamScheduleResponseData.Segments == null)
                throw new ArgumentNullException(nameof(getChannelStreamScheduleResponseData.Segments), "Property is required for class GetChannelStreamScheduleResponseData.");

            if (getChannelStreamScheduleResponseData.BroadcasterId == null)
                throw new ArgumentNullException(nameof(getChannelStreamScheduleResponseData.BroadcasterId), "Property is required for class GetChannelStreamScheduleResponseData.");

            if (getChannelStreamScheduleResponseData.BroadcasterName == null)
                throw new ArgumentNullException(nameof(getChannelStreamScheduleResponseData.BroadcasterName), "Property is required for class GetChannelStreamScheduleResponseData.");

            if (getChannelStreamScheduleResponseData.BroadcasterLogin == null)
                throw new ArgumentNullException(nameof(getChannelStreamScheduleResponseData.BroadcasterLogin), "Property is required for class GetChannelStreamScheduleResponseData.");

            if (getChannelStreamScheduleResponseData.Vacation == null)
                throw new ArgumentNullException(nameof(getChannelStreamScheduleResponseData.Vacation), "Property is required for class GetChannelStreamScheduleResponseData.");

            if (getChannelStreamScheduleResponseData.PaginationOption.IsSet && getChannelStreamScheduleResponseData.Pagination == null)
                throw new ArgumentNullException(nameof(getChannelStreamScheduleResponseData.Pagination), "Property is required for class GetChannelStreamScheduleResponseData.");

            writer.WritePropertyName("segments");
            JsonSerializer.Serialize(writer, getChannelStreamScheduleResponseData.Segments, jsonSerializerOptions);
            writer.WriteString("broadcaster_id", getChannelStreamScheduleResponseData.BroadcasterId);

            writer.WriteString("broadcaster_name", getChannelStreamScheduleResponseData.BroadcasterName);

            writer.WriteString("broadcaster_login", getChannelStreamScheduleResponseData.BroadcasterLogin);

            writer.WritePropertyName("vacation");
            JsonSerializer.Serialize(writer, getChannelStreamScheduleResponseData.Vacation, jsonSerializerOptions);
            if (getChannelStreamScheduleResponseData.PaginationOption.IsSet)
            {
                writer.WritePropertyName("pagination");
                JsonSerializer.Serialize(writer, getChannelStreamScheduleResponseData.Pagination, jsonSerializerOptions);
            }
        }
    }
}
