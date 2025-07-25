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
    /// Channel
    /// </summary>
    public partial class Channel : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Channel" /> class.
        /// </summary>
        /// <param name="broadcasterLanguage">The ISO 639-1 two-letter language code of the language used by the broadcaster. For example, _en_ for English. If the broadcaster uses a language not in the list of [supported stream languages](https://help.twitch.tv/s/article/languages-on-twitch#streamlang), the value is _other_.</param>
        /// <param name="broadcasterLogin">The broadcaster’s login name.</param>
        /// <param name="displayName">The broadcaster’s display name.</param>
        /// <param name="gameId">The ID of the game that the broadcaster is playing or last played.</param>
        /// <param name="gameName">The name of the game that the broadcaster is playing or last played.</param>
        /// <param name="id">An ID that uniquely identifies the channel (this is the broadcaster’s ID).</param>
        /// <param name="isLive">A Boolean value that determines whether the broadcaster is streaming live. Is **true** if the broadcaster is streaming live; otherwise, **false**.</param>
        /// <param name="tags">The tags applied to the channel.</param>
        /// <param name="thumbnailUrl">A URL to a thumbnail of the broadcaster’s profile image.</param>
        /// <param name="title">The stream’s title. Is an empty string if the broadcaster didn’t set it.</param>
        /// <param name="startedAt">The UTC date and time (in RFC3339 format) of when the broadcaster started streaming. The string is empty if the broadcaster is not streaming live.</param>
        [JsonConstructor]
        public Channel(string broadcasterLanguage, string broadcasterLogin, string displayName, string gameId, string gameName, string id, bool isLive, List<string> tags, string thumbnailUrl, string title, DateTime startedAt)
        {
            BroadcasterLanguage = broadcasterLanguage;
            BroadcasterLogin = broadcasterLogin;
            DisplayName = displayName;
            GameId = gameId;
            GameName = gameName;
            Id = id;
            IsLive = isLive;
            Tags = tags;
            ThumbnailUrl = thumbnailUrl;
            Title = title;
            StartedAt = startedAt;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The ISO 639-1 two-letter language code of the language used by the broadcaster. For example, _en_ for English. If the broadcaster uses a language not in the list of [supported stream languages](https://help.twitch.tv/s/article/languages-on-twitch#streamlang), the value is _other_.
        /// </summary>
        /// <value>The ISO 639-1 two-letter language code of the language used by the broadcaster. For example, _en_ for English. If the broadcaster uses a language not in the list of [supported stream languages](https://help.twitch.tv/s/article/languages-on-twitch#streamlang), the value is _other_.</value>
        [JsonPropertyName("broadcaster_language")]
        public string BroadcasterLanguage { get; set; }

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
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The ID of the game that the broadcaster is playing or last played.
        /// </summary>
        /// <value>The ID of the game that the broadcaster is playing or last played.</value>
        [JsonPropertyName("game_id")]
        public string GameId { get; set; }

        /// <summary>
        /// The name of the game that the broadcaster is playing or last played.
        /// </summary>
        /// <value>The name of the game that the broadcaster is playing or last played.</value>
        [JsonPropertyName("game_name")]
        public string GameName { get; set; }

        /// <summary>
        /// An ID that uniquely identifies the channel (this is the broadcaster’s ID).
        /// </summary>
        /// <value>An ID that uniquely identifies the channel (this is the broadcaster’s ID).</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// A Boolean value that determines whether the broadcaster is streaming live. Is **true** if the broadcaster is streaming live; otherwise, **false**.
        /// </summary>
        /// <value>A Boolean value that determines whether the broadcaster is streaming live. Is **true** if the broadcaster is streaming live; otherwise, **false**.</value>
        [JsonPropertyName("is_live")]
        public bool IsLive { get; set; }

        /// <summary>
        /// The tags applied to the channel.
        /// </summary>
        /// <value>The tags applied to the channel.</value>
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// A URL to a thumbnail of the broadcaster’s profile image.
        /// </summary>
        /// <value>A URL to a thumbnail of the broadcaster’s profile image.</value>
        [JsonPropertyName("thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// The stream’s title. Is an empty string if the broadcaster didn’t set it.
        /// </summary>
        /// <value>The stream’s title. Is an empty string if the broadcaster didn’t set it.</value>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// The UTC date and time (in RFC3339 format) of when the broadcaster started streaming. The string is empty if the broadcaster is not streaming live.
        /// </summary>
        /// <value>The UTC date and time (in RFC3339 format) of when the broadcaster started streaming. The string is empty if the broadcaster is not streaming live.</value>
        [JsonPropertyName("started_at")]
        public DateTime StartedAt { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Channel {\n");
            sb.Append("  BroadcasterLanguage: ").Append(BroadcasterLanguage).Append("\n");
            sb.Append("  BroadcasterLogin: ").Append(BroadcasterLogin).Append("\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("  GameId: ").Append(GameId).Append("\n");
            sb.Append("  GameName: ").Append(GameName).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  IsLive: ").Append(IsLive).Append("\n");
            sb.Append("  Tags: ").Append(Tags).Append("\n");
            sb.Append("  ThumbnailUrl: ").Append(ThumbnailUrl).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  StartedAt: ").Append(StartedAt).Append("\n");
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
    /// A Json converter for type <see cref="Channel" />
    /// </summary>
    public class ChannelJsonConverter : JsonConverter<Channel>
    {
        /// <summary>
        /// The format to use to serialize StartedAt
        /// </summary>
        public static string StartedAtFormat { get; set; } = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";

        /// <summary>
        /// Deserializes json to <see cref="Channel" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override Channel Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> broadcasterLanguage = default;
            Option<string?> broadcasterLogin = default;
            Option<string?> displayName = default;
            Option<string?> gameId = default;
            Option<string?> gameName = default;
            Option<string?> id = default;
            Option<bool?> isLive = default;
            Option<List<string>?> tags = default;
            Option<string?> thumbnailUrl = default;
            Option<string?> title = default;
            Option<DateTime?> startedAt = default;

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
                        case "broadcaster_language":
                            broadcasterLanguage = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "broadcaster_login":
                            broadcasterLogin = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "display_name":
                            displayName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "game_id":
                            gameId = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "game_name":
                            gameName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "id":
                            id = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "is_live":
                            isLive = new Option<bool?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (bool?)null : utf8JsonReader.GetBoolean());
                            break;
                        case "tags":
                            tags = new Option<List<string>?>(JsonSerializer.Deserialize<List<string>>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        case "thumbnail_url":
                            thumbnailUrl = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "title":
                            title = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "started_at":
                            startedAt = new Option<DateTime?>(JsonSerializer.Deserialize<DateTime>(ref utf8JsonReader, jsonSerializerOptions));
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!broadcasterLanguage.IsSet)
                throw new ArgumentException("Property is required for class Channel.", nameof(broadcasterLanguage));

            if (!broadcasterLogin.IsSet)
                throw new ArgumentException("Property is required for class Channel.", nameof(broadcasterLogin));

            if (!displayName.IsSet)
                throw new ArgumentException("Property is required for class Channel.", nameof(displayName));

            if (!gameId.IsSet)
                throw new ArgumentException("Property is required for class Channel.", nameof(gameId));

            if (!gameName.IsSet)
                throw new ArgumentException("Property is required for class Channel.", nameof(gameName));

            if (!id.IsSet)
                throw new ArgumentException("Property is required for class Channel.", nameof(id));

            if (!isLive.IsSet)
                throw new ArgumentException("Property is required for class Channel.", nameof(isLive));

            if (!tags.IsSet)
                throw new ArgumentException("Property is required for class Channel.", nameof(tags));

            if (!thumbnailUrl.IsSet)
                throw new ArgumentException("Property is required for class Channel.", nameof(thumbnailUrl));

            if (!title.IsSet)
                throw new ArgumentException("Property is required for class Channel.", nameof(title));

            if (!startedAt.IsSet)
                throw new ArgumentException("Property is required for class Channel.", nameof(startedAt));

            if (broadcasterLanguage.IsSet && broadcasterLanguage.Value == null)
                throw new ArgumentNullException(nameof(broadcasterLanguage), "Property is not nullable for class Channel.");

            if (broadcasterLogin.IsSet && broadcasterLogin.Value == null)
                throw new ArgumentNullException(nameof(broadcasterLogin), "Property is not nullable for class Channel.");

            if (displayName.IsSet && displayName.Value == null)
                throw new ArgumentNullException(nameof(displayName), "Property is not nullable for class Channel.");

            if (gameId.IsSet && gameId.Value == null)
                throw new ArgumentNullException(nameof(gameId), "Property is not nullable for class Channel.");

            if (gameName.IsSet && gameName.Value == null)
                throw new ArgumentNullException(nameof(gameName), "Property is not nullable for class Channel.");

            if (id.IsSet && id.Value == null)
                throw new ArgumentNullException(nameof(id), "Property is not nullable for class Channel.");

            if (isLive.IsSet && isLive.Value == null)
                throw new ArgumentNullException(nameof(isLive), "Property is not nullable for class Channel.");

            if (tags.IsSet && tags.Value == null)
                throw new ArgumentNullException(nameof(tags), "Property is not nullable for class Channel.");

            if (thumbnailUrl.IsSet && thumbnailUrl.Value == null)
                throw new ArgumentNullException(nameof(thumbnailUrl), "Property is not nullable for class Channel.");

            if (title.IsSet && title.Value == null)
                throw new ArgumentNullException(nameof(title), "Property is not nullable for class Channel.");

            if (startedAt.IsSet && startedAt.Value == null)
                throw new ArgumentNullException(nameof(startedAt), "Property is not nullable for class Channel.");

            return new Channel(broadcasterLanguage.Value!, broadcasterLogin.Value!, displayName.Value!, gameId.Value!, gameName.Value!, id.Value!, isLive.Value!.Value!, tags.Value!, thumbnailUrl.Value!, title.Value!, startedAt.Value!.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="Channel" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="channel"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, Channel channel, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, channel, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="Channel" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="channel"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, Channel channel, JsonSerializerOptions jsonSerializerOptions)
        {
            if (channel.BroadcasterLanguage == null)
                throw new ArgumentNullException(nameof(channel.BroadcasterLanguage), "Property is required for class Channel.");

            if (channel.BroadcasterLogin == null)
                throw new ArgumentNullException(nameof(channel.BroadcasterLogin), "Property is required for class Channel.");

            if (channel.DisplayName == null)
                throw new ArgumentNullException(nameof(channel.DisplayName), "Property is required for class Channel.");

            if (channel.GameId == null)
                throw new ArgumentNullException(nameof(channel.GameId), "Property is required for class Channel.");

            if (channel.GameName == null)
                throw new ArgumentNullException(nameof(channel.GameName), "Property is required for class Channel.");

            if (channel.Id == null)
                throw new ArgumentNullException(nameof(channel.Id), "Property is required for class Channel.");

            if (channel.Tags == null)
                throw new ArgumentNullException(nameof(channel.Tags), "Property is required for class Channel.");

            if (channel.ThumbnailUrl == null)
                throw new ArgumentNullException(nameof(channel.ThumbnailUrl), "Property is required for class Channel.");

            if (channel.Title == null)
                throw new ArgumentNullException(nameof(channel.Title), "Property is required for class Channel.");

            writer.WriteString("broadcaster_language", channel.BroadcasterLanguage);

            writer.WriteString("broadcaster_login", channel.BroadcasterLogin);

            writer.WriteString("display_name", channel.DisplayName);

            writer.WriteString("game_id", channel.GameId);

            writer.WriteString("game_name", channel.GameName);

            writer.WriteString("id", channel.Id);

            writer.WriteBoolean("is_live", channel.IsLive);

            writer.WritePropertyName("tags");
            JsonSerializer.Serialize(writer, channel.Tags, jsonSerializerOptions);
            writer.WriteString("thumbnail_url", channel.ThumbnailUrl);

            writer.WriteString("title", channel.Title);

            writer.WriteString("started_at", channel.StartedAt.ToString(StartedAtFormat));
        }
    }
}
