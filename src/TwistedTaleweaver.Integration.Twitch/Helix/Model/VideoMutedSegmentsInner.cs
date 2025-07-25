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
    /// VideoMutedSegmentsInner
    /// </summary>
    public partial class VideoMutedSegmentsInner : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoMutedSegmentsInner" /> class.
        /// </summary>
        /// <param name="duration">The duration of the muted segment, in seconds.</param>
        /// <param name="offset">The offset, in seconds, from the beginning of the video to where the muted segment begins.</param>
        [JsonConstructor]
        public VideoMutedSegmentsInner(int duration, int offset)
        {
            Duration = duration;
            Offset = offset;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The duration of the muted segment, in seconds.
        /// </summary>
        /// <value>The duration of the muted segment, in seconds.</value>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        /// <summary>
        /// The offset, in seconds, from the beginning of the video to where the muted segment begins.
        /// </summary>
        /// <value>The offset, in seconds, from the beginning of the video to where the muted segment begins.</value>
        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class VideoMutedSegmentsInner {\n");
            sb.Append("  Duration: ").Append(Duration).Append("\n");
            sb.Append("  Offset: ").Append(Offset).Append("\n");
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
    /// A Json converter for type <see cref="VideoMutedSegmentsInner" />
    /// </summary>
    public class VideoMutedSegmentsInnerJsonConverter : JsonConverter<VideoMutedSegmentsInner>
    {
        /// <summary>
        /// Deserializes json to <see cref="VideoMutedSegmentsInner" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override VideoMutedSegmentsInner Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<int?> duration = default;
            Option<int?> offset = default;

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
                        case "duration":
                            duration = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "offset":
                            offset = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!duration.IsSet)
                throw new ArgumentException("Property is required for class VideoMutedSegmentsInner.", nameof(duration));

            if (!offset.IsSet)
                throw new ArgumentException("Property is required for class VideoMutedSegmentsInner.", nameof(offset));

            if (duration.IsSet && duration.Value == null)
                throw new ArgumentNullException(nameof(duration), "Property is not nullable for class VideoMutedSegmentsInner.");

            if (offset.IsSet && offset.Value == null)
                throw new ArgumentNullException(nameof(offset), "Property is not nullable for class VideoMutedSegmentsInner.");

            return new VideoMutedSegmentsInner(duration.Value!.Value!, offset.Value!.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="VideoMutedSegmentsInner" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="videoMutedSegmentsInner"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, VideoMutedSegmentsInner videoMutedSegmentsInner, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, videoMutedSegmentsInner, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="VideoMutedSegmentsInner" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="videoMutedSegmentsInner"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, VideoMutedSegmentsInner videoMutedSegmentsInner, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteNumber("duration", videoMutedSegmentsInner.Duration);

            writer.WriteNumber("offset", videoMutedSegmentsInner.Offset);
        }
    }
}
