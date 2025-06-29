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
    /// StreamMarkerCreated
    /// </summary>
    public partial class StreamMarkerCreated : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StreamMarkerCreated" /> class.
        /// </summary>
        /// <param name="id">An ID that identifies this marker.</param>
        /// <param name="createdAt">The UTC date and time (in RFC3339 format) of when the user created the marker.</param>
        /// <param name="positionSeconds">The relative offset (in seconds) of the marker from the beginning of the stream.</param>
        /// <param name="description">A description that the user gave the marker to help them remember why they marked the location.</param>
        [JsonConstructor]
        public StreamMarkerCreated(string id, DateTime createdAt, int positionSeconds, string description)
        {
            Id = id;
            CreatedAt = createdAt;
            PositionSeconds = positionSeconds;
            Description = description;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// An ID that identifies this marker.
        /// </summary>
        /// <value>An ID that identifies this marker.</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The UTC date and time (in RFC3339 format) of when the user created the marker.
        /// </summary>
        /// <value>The UTC date and time (in RFC3339 format) of when the user created the marker.</value>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The relative offset (in seconds) of the marker from the beginning of the stream.
        /// </summary>
        /// <value>The relative offset (in seconds) of the marker from the beginning of the stream.</value>
        [JsonPropertyName("position_seconds")]
        public int PositionSeconds { get; set; }

        /// <summary>
        /// A description that the user gave the marker to help them remember why they marked the location.
        /// </summary>
        /// <value>A description that the user gave the marker to help them remember why they marked the location.</value>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class StreamMarkerCreated {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
            sb.Append("  PositionSeconds: ").Append(PositionSeconds).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
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
    /// A Json converter for type <see cref="StreamMarkerCreated" />
    /// </summary>
    public class StreamMarkerCreatedJsonConverter : JsonConverter<StreamMarkerCreated>
    {
        /// <summary>
        /// The format to use to serialize CreatedAt
        /// </summary>
        public static string CreatedAtFormat { get; set; } = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";

        /// <summary>
        /// Deserializes json to <see cref="StreamMarkerCreated" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override StreamMarkerCreated Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> id = default;
            Option<DateTime?> createdAt = default;
            Option<int?> positionSeconds = default;
            Option<string?> description = default;

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
                        case "id":
                            id = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "created_at":
                            createdAt = new Option<DateTime?>(JsonSerializer.Deserialize<DateTime>(ref utf8JsonReader, jsonSerializerOptions));
                            break;
                        case "position_seconds":
                            positionSeconds = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "description":
                            description = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!id.IsSet)
                throw new ArgumentException("Property is required for class StreamMarkerCreated.", nameof(id));

            if (!createdAt.IsSet)
                throw new ArgumentException("Property is required for class StreamMarkerCreated.", nameof(createdAt));

            if (!positionSeconds.IsSet)
                throw new ArgumentException("Property is required for class StreamMarkerCreated.", nameof(positionSeconds));

            if (!description.IsSet)
                throw new ArgumentException("Property is required for class StreamMarkerCreated.", nameof(description));

            if (id.IsSet && id.Value == null)
                throw new ArgumentNullException(nameof(id), "Property is not nullable for class StreamMarkerCreated.");

            if (createdAt.IsSet && createdAt.Value == null)
                throw new ArgumentNullException(nameof(createdAt), "Property is not nullable for class StreamMarkerCreated.");

            if (positionSeconds.IsSet && positionSeconds.Value == null)
                throw new ArgumentNullException(nameof(positionSeconds), "Property is not nullable for class StreamMarkerCreated.");

            if (description.IsSet && description.Value == null)
                throw new ArgumentNullException(nameof(description), "Property is not nullable for class StreamMarkerCreated.");

            return new StreamMarkerCreated(id.Value!, createdAt.Value!.Value!, positionSeconds.Value!.Value!, description.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="StreamMarkerCreated" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="streamMarkerCreated"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, StreamMarkerCreated streamMarkerCreated, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, streamMarkerCreated, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="StreamMarkerCreated" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="streamMarkerCreated"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, StreamMarkerCreated streamMarkerCreated, JsonSerializerOptions jsonSerializerOptions)
        {
            if (streamMarkerCreated.Id == null)
                throw new ArgumentNullException(nameof(streamMarkerCreated.Id), "Property is required for class StreamMarkerCreated.");

            if (streamMarkerCreated.Description == null)
                throw new ArgumentNullException(nameof(streamMarkerCreated.Description), "Property is required for class StreamMarkerCreated.");

            writer.WriteString("id", streamMarkerCreated.Id);

            writer.WriteString("created_at", streamMarkerCreated.CreatedAt.ToString(CreatedAtFormat));

            writer.WriteNumber("position_seconds", streamMarkerCreated.PositionSeconds);

            writer.WriteString("description", streamMarkerCreated.Description);
        }
    }
}
