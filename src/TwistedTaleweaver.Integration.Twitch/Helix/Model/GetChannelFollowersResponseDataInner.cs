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
    /// GetChannelFollowersResponseDataInner
    /// </summary>
    public partial class GetChannelFollowersResponseDataInner : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetChannelFollowersResponseDataInner" /> class.
        /// </summary>
        /// <param name="followedAt">The UTC timestamp when the user started following the broadcaster.</param>
        /// <param name="userId">An ID that uniquely identifies the user that’s following the broadcaster.</param>
        /// <param name="userLogin">The user’s login name.</param>
        /// <param name="userName">The user’s display name.</param>
        [JsonConstructor]
        public GetChannelFollowersResponseDataInner(DateTime followedAt, string userId, string userLogin, string userName)
        {
            FollowedAt = followedAt;
            UserId = userId;
            UserLogin = userLogin;
            UserName = userName;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The UTC timestamp when the user started following the broadcaster.
        /// </summary>
        /// <value>The UTC timestamp when the user started following the broadcaster.</value>
        [JsonPropertyName("followed_at")]
        public DateTime FollowedAt { get; set; }

        /// <summary>
        /// An ID that uniquely identifies the user that’s following the broadcaster.
        /// </summary>
        /// <value>An ID that uniquely identifies the user that’s following the broadcaster.</value>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// The user’s login name.
        /// </summary>
        /// <value>The user’s login name.</value>
        [JsonPropertyName("user_login")]
        public string UserLogin { get; set; }

        /// <summary>
        /// The user’s display name.
        /// </summary>
        /// <value>The user’s display name.</value>
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetChannelFollowersResponseDataInner {\n");
            sb.Append("  FollowedAt: ").Append(FollowedAt).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  UserLogin: ").Append(UserLogin).Append("\n");
            sb.Append("  UserName: ").Append(UserName).Append("\n");
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
    /// A Json converter for type <see cref="GetChannelFollowersResponseDataInner" />
    /// </summary>
    public class GetChannelFollowersResponseDataInnerJsonConverter : JsonConverter<GetChannelFollowersResponseDataInner>
    {
        /// <summary>
        /// The format to use to serialize FollowedAt
        /// </summary>
        public static string FollowedAtFormat { get; set; } = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";

        /// <summary>
        /// Deserializes json to <see cref="GetChannelFollowersResponseDataInner" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetChannelFollowersResponseDataInner Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<DateTime?> followedAt = default;
            Option<string?> userId = default;
            Option<string?> userLogin = default;
            Option<string?> userName = default;

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
                        case "followed_at":
                            followedAt = new Option<DateTime?>(JsonSerializer.Deserialize<DateTime>(ref utf8JsonReader, jsonSerializerOptions));
                            break;
                        case "user_id":
                            userId = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "user_login":
                            userLogin = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "user_name":
                            userName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!followedAt.IsSet)
                throw new ArgumentException("Property is required for class GetChannelFollowersResponseDataInner.", nameof(followedAt));

            if (!userId.IsSet)
                throw new ArgumentException("Property is required for class GetChannelFollowersResponseDataInner.", nameof(userId));

            if (!userLogin.IsSet)
                throw new ArgumentException("Property is required for class GetChannelFollowersResponseDataInner.", nameof(userLogin));

            if (!userName.IsSet)
                throw new ArgumentException("Property is required for class GetChannelFollowersResponseDataInner.", nameof(userName));

            if (followedAt.IsSet && followedAt.Value == null)
                throw new ArgumentNullException(nameof(followedAt), "Property is not nullable for class GetChannelFollowersResponseDataInner.");

            if (userId.IsSet && userId.Value == null)
                throw new ArgumentNullException(nameof(userId), "Property is not nullable for class GetChannelFollowersResponseDataInner.");

            if (userLogin.IsSet && userLogin.Value == null)
                throw new ArgumentNullException(nameof(userLogin), "Property is not nullable for class GetChannelFollowersResponseDataInner.");

            if (userName.IsSet && userName.Value == null)
                throw new ArgumentNullException(nameof(userName), "Property is not nullable for class GetChannelFollowersResponseDataInner.");

            return new GetChannelFollowersResponseDataInner(followedAt.Value!.Value!, userId.Value!, userLogin.Value!, userName.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="GetChannelFollowersResponseDataInner" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getChannelFollowersResponseDataInner"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetChannelFollowersResponseDataInner getChannelFollowersResponseDataInner, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, getChannelFollowersResponseDataInner, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetChannelFollowersResponseDataInner" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getChannelFollowersResponseDataInner"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, GetChannelFollowersResponseDataInner getChannelFollowersResponseDataInner, JsonSerializerOptions jsonSerializerOptions)
        {
            if (getChannelFollowersResponseDataInner.UserId == null)
                throw new ArgumentNullException(nameof(getChannelFollowersResponseDataInner.UserId), "Property is required for class GetChannelFollowersResponseDataInner.");

            if (getChannelFollowersResponseDataInner.UserLogin == null)
                throw new ArgumentNullException(nameof(getChannelFollowersResponseDataInner.UserLogin), "Property is required for class GetChannelFollowersResponseDataInner.");

            if (getChannelFollowersResponseDataInner.UserName == null)
                throw new ArgumentNullException(nameof(getChannelFollowersResponseDataInner.UserName), "Property is required for class GetChannelFollowersResponseDataInner.");

            writer.WriteString("followed_at", getChannelFollowersResponseDataInner.FollowedAt.ToString(FollowedAtFormat));

            writer.WriteString("user_id", getChannelFollowersResponseDataInner.UserId);

            writer.WriteString("user_login", getChannelFollowersResponseDataInner.UserLogin);

            writer.WriteString("user_name", getChannelFollowersResponseDataInner.UserName);
        }
    }
}
