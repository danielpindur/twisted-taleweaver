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
    /// BannedUser
    /// </summary>
    public partial class BannedUser : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BannedUser" /> class.
        /// </summary>
        /// <param name="userId">The ID of the banned user.</param>
        /// <param name="userLogin">The banned user’s login name.</param>
        /// <param name="userName">The banned user’s display name.</param>
        /// <param name="expiresAt">The UTC date and time (in RFC3339 format) of when the timeout expires, or an empty string if the user is permanently banned.</param>
        /// <param name="createdAt">The UTC date and time (in RFC3339 format) of when the user was banned.</param>
        /// <param name="reason">The reason the user was banned or put in a timeout if the moderator provided one.</param>
        /// <param name="moderatorId">The ID of the moderator that banned the user or put them in a timeout.</param>
        /// <param name="moderatorLogin">The moderator’s login name.</param>
        /// <param name="moderatorName">The moderator’s display name.</param>
        [JsonConstructor]
        public BannedUser(string userId, string userLogin, string userName, DateTime expiresAt, DateTime createdAt, string reason, string moderatorId, string moderatorLogin, string moderatorName)
        {
            UserId = userId;
            UserLogin = userLogin;
            UserName = userName;
            ExpiresAt = expiresAt;
            CreatedAt = createdAt;
            Reason = reason;
            ModeratorId = moderatorId;
            ModeratorLogin = moderatorLogin;
            ModeratorName = moderatorName;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The ID of the banned user.
        /// </summary>
        /// <value>The ID of the banned user.</value>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// The banned user’s login name.
        /// </summary>
        /// <value>The banned user’s login name.</value>
        [JsonPropertyName("user_login")]
        public string UserLogin { get; set; }

        /// <summary>
        /// The banned user’s display name.
        /// </summary>
        /// <value>The banned user’s display name.</value>
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// The UTC date and time (in RFC3339 format) of when the timeout expires, or an empty string if the user is permanently banned.
        /// </summary>
        /// <value>The UTC date and time (in RFC3339 format) of when the timeout expires, or an empty string if the user is permanently banned.</value>
        [JsonPropertyName("expires_at")]
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// The UTC date and time (in RFC3339 format) of when the user was banned.
        /// </summary>
        /// <value>The UTC date and time (in RFC3339 format) of when the user was banned.</value>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The reason the user was banned or put in a timeout if the moderator provided one.
        /// </summary>
        /// <value>The reason the user was banned or put in a timeout if the moderator provided one.</value>
        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// The ID of the moderator that banned the user or put them in a timeout.
        /// </summary>
        /// <value>The ID of the moderator that banned the user or put them in a timeout.</value>
        [JsonPropertyName("moderator_id")]
        public string ModeratorId { get; set; }

        /// <summary>
        /// The moderator’s login name.
        /// </summary>
        /// <value>The moderator’s login name.</value>
        [JsonPropertyName("moderator_login")]
        public string ModeratorLogin { get; set; }

        /// <summary>
        /// The moderator’s display name.
        /// </summary>
        /// <value>The moderator’s display name.</value>
        [JsonPropertyName("moderator_name")]
        public string ModeratorName { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class BannedUser {\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  UserLogin: ").Append(UserLogin).Append("\n");
            sb.Append("  UserName: ").Append(UserName).Append("\n");
            sb.Append("  ExpiresAt: ").Append(ExpiresAt).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
            sb.Append("  Reason: ").Append(Reason).Append("\n");
            sb.Append("  ModeratorId: ").Append(ModeratorId).Append("\n");
            sb.Append("  ModeratorLogin: ").Append(ModeratorLogin).Append("\n");
            sb.Append("  ModeratorName: ").Append(ModeratorName).Append("\n");
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
    /// A Json converter for type <see cref="BannedUser" />
    /// </summary>
    public class BannedUserJsonConverter : JsonConverter<BannedUser>
    {
        /// <summary>
        /// The format to use to serialize ExpiresAt
        /// </summary>
        public static string ExpiresAtFormat { get; set; } = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";

        /// <summary>
        /// The format to use to serialize CreatedAt
        /// </summary>
        public static string CreatedAtFormat { get; set; } = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";

        /// <summary>
        /// Deserializes json to <see cref="BannedUser" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override BannedUser Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> userId = default;
            Option<string?> userLogin = default;
            Option<string?> userName = default;
            Option<DateTime?> expiresAt = default;
            Option<DateTime?> createdAt = default;
            Option<string?> reason = default;
            Option<string?> moderatorId = default;
            Option<string?> moderatorLogin = default;
            Option<string?> moderatorName = default;

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
                        case "user_id":
                            userId = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "user_login":
                            userLogin = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "user_name":
                            userName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "expires_at":
                            expiresAt = new Option<DateTime?>(JsonSerializer.Deserialize<DateTime>(ref utf8JsonReader, jsonSerializerOptions));
                            break;
                        case "created_at":
                            createdAt = new Option<DateTime?>(JsonSerializer.Deserialize<DateTime>(ref utf8JsonReader, jsonSerializerOptions));
                            break;
                        case "reason":
                            reason = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "moderator_id":
                            moderatorId = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "moderator_login":
                            moderatorLogin = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "moderator_name":
                            moderatorName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!userId.IsSet)
                throw new ArgumentException("Property is required for class BannedUser.", nameof(userId));

            if (!userLogin.IsSet)
                throw new ArgumentException("Property is required for class BannedUser.", nameof(userLogin));

            if (!userName.IsSet)
                throw new ArgumentException("Property is required for class BannedUser.", nameof(userName));

            if (!expiresAt.IsSet)
                throw new ArgumentException("Property is required for class BannedUser.", nameof(expiresAt));

            if (!createdAt.IsSet)
                throw new ArgumentException("Property is required for class BannedUser.", nameof(createdAt));

            if (!reason.IsSet)
                throw new ArgumentException("Property is required for class BannedUser.", nameof(reason));

            if (!moderatorId.IsSet)
                throw new ArgumentException("Property is required for class BannedUser.", nameof(moderatorId));

            if (!moderatorLogin.IsSet)
                throw new ArgumentException("Property is required for class BannedUser.", nameof(moderatorLogin));

            if (!moderatorName.IsSet)
                throw new ArgumentException("Property is required for class BannedUser.", nameof(moderatorName));

            if (userId.IsSet && userId.Value == null)
                throw new ArgumentNullException(nameof(userId), "Property is not nullable for class BannedUser.");

            if (userLogin.IsSet && userLogin.Value == null)
                throw new ArgumentNullException(nameof(userLogin), "Property is not nullable for class BannedUser.");

            if (userName.IsSet && userName.Value == null)
                throw new ArgumentNullException(nameof(userName), "Property is not nullable for class BannedUser.");

            if (expiresAt.IsSet && expiresAt.Value == null)
                throw new ArgumentNullException(nameof(expiresAt), "Property is not nullable for class BannedUser.");

            if (createdAt.IsSet && createdAt.Value == null)
                throw new ArgumentNullException(nameof(createdAt), "Property is not nullable for class BannedUser.");

            if (reason.IsSet && reason.Value == null)
                throw new ArgumentNullException(nameof(reason), "Property is not nullable for class BannedUser.");

            if (moderatorId.IsSet && moderatorId.Value == null)
                throw new ArgumentNullException(nameof(moderatorId), "Property is not nullable for class BannedUser.");

            if (moderatorLogin.IsSet && moderatorLogin.Value == null)
                throw new ArgumentNullException(nameof(moderatorLogin), "Property is not nullable for class BannedUser.");

            if (moderatorName.IsSet && moderatorName.Value == null)
                throw new ArgumentNullException(nameof(moderatorName), "Property is not nullable for class BannedUser.");

            return new BannedUser(userId.Value!, userLogin.Value!, userName.Value!, expiresAt.Value!.Value!, createdAt.Value!.Value!, reason.Value!, moderatorId.Value!, moderatorLogin.Value!, moderatorName.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="BannedUser" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="bannedUser"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, BannedUser bannedUser, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, bannedUser, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="BannedUser" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="bannedUser"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, BannedUser bannedUser, JsonSerializerOptions jsonSerializerOptions)
        {
            if (bannedUser.UserId == null)
                throw new ArgumentNullException(nameof(bannedUser.UserId), "Property is required for class BannedUser.");

            if (bannedUser.UserLogin == null)
                throw new ArgumentNullException(nameof(bannedUser.UserLogin), "Property is required for class BannedUser.");

            if (bannedUser.UserName == null)
                throw new ArgumentNullException(nameof(bannedUser.UserName), "Property is required for class BannedUser.");

            if (bannedUser.Reason == null)
                throw new ArgumentNullException(nameof(bannedUser.Reason), "Property is required for class BannedUser.");

            if (bannedUser.ModeratorId == null)
                throw new ArgumentNullException(nameof(bannedUser.ModeratorId), "Property is required for class BannedUser.");

            if (bannedUser.ModeratorLogin == null)
                throw new ArgumentNullException(nameof(bannedUser.ModeratorLogin), "Property is required for class BannedUser.");

            if (bannedUser.ModeratorName == null)
                throw new ArgumentNullException(nameof(bannedUser.ModeratorName), "Property is required for class BannedUser.");

            writer.WriteString("user_id", bannedUser.UserId);

            writer.WriteString("user_login", bannedUser.UserLogin);

            writer.WriteString("user_name", bannedUser.UserName);

            writer.WriteString("expires_at", bannedUser.ExpiresAt.ToString(ExpiresAtFormat));

            writer.WriteString("created_at", bannedUser.CreatedAt.ToString(CreatedAtFormat));

            writer.WriteString("reason", bannedUser.Reason);

            writer.WriteString("moderator_id", bannedUser.ModeratorId);

            writer.WriteString("moderator_login", bannedUser.ModeratorLogin);

            writer.WriteString("moderator_name", bannedUser.ModeratorName);
        }
    }
}
