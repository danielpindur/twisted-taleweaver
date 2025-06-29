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
    /// GetFollowedChannelsResponseDataInner
    /// </summary>
    public partial class GetFollowedChannelsResponseDataInner : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFollowedChannelsResponseDataInner" /> class.
        /// </summary>
        /// <param name="broadcasterId">An ID that uniquely identifies the broadcaster that this user is following.</param>
        /// <param name="broadcasterLogin">The broadcaster’s login name.</param>
        /// <param name="broadcasterName">The broadcaster’s display name.</param>
        /// <param name="followedAt">The UTC timestamp when the user started following the broadcaster.</param>
        [JsonConstructor]
        public GetFollowedChannelsResponseDataInner(string broadcasterId, string broadcasterLogin, string broadcasterName, DateTime followedAt)
        {
            BroadcasterId = broadcasterId;
            BroadcasterLogin = broadcasterLogin;
            BroadcasterName = broadcasterName;
            FollowedAt = followedAt;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// An ID that uniquely identifies the broadcaster that this user is following.
        /// </summary>
        /// <value>An ID that uniquely identifies the broadcaster that this user is following.</value>
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; set; }

        /// <summary>
        /// The broadcaster’s login name.
        /// </summary>
        /// <value>The broadcaster’s login name.</value>
        [JsonPropertyName("broadcaster_login")]
        public string BroadcasterLogin { get; set; }

        /// <summary>
        /// The broadcaster’s display name.
        /// </summary>
        /// <value>The broadcaster’s display name.</value>
        [JsonPropertyName("broadcaster_name")]
        public string BroadcasterName { get; set; }

        /// <summary>
        /// The UTC timestamp when the user started following the broadcaster.
        /// </summary>
        /// <value>The UTC timestamp when the user started following the broadcaster.</value>
        [JsonPropertyName("followed_at")]
        public DateTime FollowedAt { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetFollowedChannelsResponseDataInner {\n");
            sb.Append("  BroadcasterId: ").Append(BroadcasterId).Append("\n");
            sb.Append("  BroadcasterLogin: ").Append(BroadcasterLogin).Append("\n");
            sb.Append("  BroadcasterName: ").Append(BroadcasterName).Append("\n");
            sb.Append("  FollowedAt: ").Append(FollowedAt).Append("\n");
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
    /// A Json converter for type <see cref="GetFollowedChannelsResponseDataInner" />
    /// </summary>
    public class GetFollowedChannelsResponseDataInnerJsonConverter : JsonConverter<GetFollowedChannelsResponseDataInner>
    {
        /// <summary>
        /// The format to use to serialize FollowedAt
        /// </summary>
        public static string FollowedAtFormat { get; set; } = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";

        /// <summary>
        /// Deserializes json to <see cref="GetFollowedChannelsResponseDataInner" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetFollowedChannelsResponseDataInner Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> broadcasterId = default;
            Option<string?> broadcasterLogin = default;
            Option<string?> broadcasterName = default;
            Option<DateTime?> followedAt = default;

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
                        case "broadcaster_id":
                            broadcasterId = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "broadcaster_login":
                            broadcasterLogin = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "broadcaster_name":
                            broadcasterName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "followed_at":
                            followedAt = new Option<DateTime?>(JsonSerializer.Deserialize<DateTime>(ref utf8JsonReader, jsonSerializerOptions));
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!broadcasterId.IsSet)
                throw new ArgumentException("Property is required for class GetFollowedChannelsResponseDataInner.", nameof(broadcasterId));

            if (!broadcasterLogin.IsSet)
                throw new ArgumentException("Property is required for class GetFollowedChannelsResponseDataInner.", nameof(broadcasterLogin));

            if (!broadcasterName.IsSet)
                throw new ArgumentException("Property is required for class GetFollowedChannelsResponseDataInner.", nameof(broadcasterName));

            if (!followedAt.IsSet)
                throw new ArgumentException("Property is required for class GetFollowedChannelsResponseDataInner.", nameof(followedAt));

            if (broadcasterId.IsSet && broadcasterId.Value == null)
                throw new ArgumentNullException(nameof(broadcasterId), "Property is not nullable for class GetFollowedChannelsResponseDataInner.");

            if (broadcasterLogin.IsSet && broadcasterLogin.Value == null)
                throw new ArgumentNullException(nameof(broadcasterLogin), "Property is not nullable for class GetFollowedChannelsResponseDataInner.");

            if (broadcasterName.IsSet && broadcasterName.Value == null)
                throw new ArgumentNullException(nameof(broadcasterName), "Property is not nullable for class GetFollowedChannelsResponseDataInner.");

            if (followedAt.IsSet && followedAt.Value == null)
                throw new ArgumentNullException(nameof(followedAt), "Property is not nullable for class GetFollowedChannelsResponseDataInner.");

            return new GetFollowedChannelsResponseDataInner(broadcasterId.Value!, broadcasterLogin.Value!, broadcasterName.Value!, followedAt.Value!.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="GetFollowedChannelsResponseDataInner" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getFollowedChannelsResponseDataInner"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetFollowedChannelsResponseDataInner getFollowedChannelsResponseDataInner, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, getFollowedChannelsResponseDataInner, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetFollowedChannelsResponseDataInner" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getFollowedChannelsResponseDataInner"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, GetFollowedChannelsResponseDataInner getFollowedChannelsResponseDataInner, JsonSerializerOptions jsonSerializerOptions)
        {
            if (getFollowedChannelsResponseDataInner.BroadcasterId == null)
                throw new ArgumentNullException(nameof(getFollowedChannelsResponseDataInner.BroadcasterId), "Property is required for class GetFollowedChannelsResponseDataInner.");

            if (getFollowedChannelsResponseDataInner.BroadcasterLogin == null)
                throw new ArgumentNullException(nameof(getFollowedChannelsResponseDataInner.BroadcasterLogin), "Property is required for class GetFollowedChannelsResponseDataInner.");

            if (getFollowedChannelsResponseDataInner.BroadcasterName == null)
                throw new ArgumentNullException(nameof(getFollowedChannelsResponseDataInner.BroadcasterName), "Property is required for class GetFollowedChannelsResponseDataInner.");

            writer.WriteString("broadcaster_id", getFollowedChannelsResponseDataInner.BroadcasterId);

            writer.WriteString("broadcaster_login", getFollowedChannelsResponseDataInner.BroadcasterLogin);

            writer.WriteString("broadcaster_name", getFollowedChannelsResponseDataInner.BroadcasterName);

            writer.WriteString("followed_at", getFollowedChannelsResponseDataInner.FollowedAt.ToString(FollowedAtFormat));
        }
    }
}
