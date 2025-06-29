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
    /// StartCommercialResponseDataInner
    /// </summary>
    public partial class StartCommercialResponseDataInner : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartCommercialResponseDataInner" /> class.
        /// </summary>
        /// <param name="length">The length of the commercial you requested. If you request a commercial that’s longer than 180 seconds, the API uses 180 seconds.</param>
        /// <param name="message">A message that indicates whether Twitch was able to serve an ad.</param>
        /// <param name="retryAfter">The number of seconds you must wait before running another commercial.</param>
        [JsonConstructor]
        public StartCommercialResponseDataInner(int length, string message, int retryAfter)
        {
            Length = length;
            Message = message;
            RetryAfter = retryAfter;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The length of the commercial you requested. If you request a commercial that’s longer than 180 seconds, the API uses 180 seconds.
        /// </summary>
        /// <value>The length of the commercial you requested. If you request a commercial that’s longer than 180 seconds, the API uses 180 seconds.</value>
        [JsonPropertyName("length")]
        public int Length { get; set; }

        /// <summary>
        /// A message that indicates whether Twitch was able to serve an ad.
        /// </summary>
        /// <value>A message that indicates whether Twitch was able to serve an ad.</value>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// The number of seconds you must wait before running another commercial.
        /// </summary>
        /// <value>The number of seconds you must wait before running another commercial.</value>
        [JsonPropertyName("retry_after")]
        public int RetryAfter { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class StartCommercialResponseDataInner {\n");
            sb.Append("  Length: ").Append(Length).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  RetryAfter: ").Append(RetryAfter).Append("\n");
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
    /// A Json converter for type <see cref="StartCommercialResponseDataInner" />
    /// </summary>
    public class StartCommercialResponseDataInnerJsonConverter : JsonConverter<StartCommercialResponseDataInner>
    {
        /// <summary>
        /// Deserializes json to <see cref="StartCommercialResponseDataInner" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override StartCommercialResponseDataInner Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<int?> length = default;
            Option<string?> message = default;
            Option<int?> retryAfter = default;

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
                        case "length":
                            length = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "message":
                            message = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "retry_after":
                            retryAfter = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!length.IsSet)
                throw new ArgumentException("Property is required for class StartCommercialResponseDataInner.", nameof(length));

            if (!message.IsSet)
                throw new ArgumentException("Property is required for class StartCommercialResponseDataInner.", nameof(message));

            if (!retryAfter.IsSet)
                throw new ArgumentException("Property is required for class StartCommercialResponseDataInner.", nameof(retryAfter));

            if (length.IsSet && length.Value == null)
                throw new ArgumentNullException(nameof(length), "Property is not nullable for class StartCommercialResponseDataInner.");

            if (message.IsSet && message.Value == null)
                throw new ArgumentNullException(nameof(message), "Property is not nullable for class StartCommercialResponseDataInner.");

            if (retryAfter.IsSet && retryAfter.Value == null)
                throw new ArgumentNullException(nameof(retryAfter), "Property is not nullable for class StartCommercialResponseDataInner.");

            return new StartCommercialResponseDataInner(length.Value!.Value!, message.Value!, retryAfter.Value!.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="StartCommercialResponseDataInner" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="startCommercialResponseDataInner"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, StartCommercialResponseDataInner startCommercialResponseDataInner, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, startCommercialResponseDataInner, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="StartCommercialResponseDataInner" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="startCommercialResponseDataInner"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, StartCommercialResponseDataInner startCommercialResponseDataInner, JsonSerializerOptions jsonSerializerOptions)
        {
            if (startCommercialResponseDataInner.Message == null)
                throw new ArgumentNullException(nameof(startCommercialResponseDataInner.Message), "Property is required for class StartCommercialResponseDataInner.");

            writer.WriteNumber("length", startCommercialResponseDataInner.Length);

            writer.WriteString("message", startCommercialResponseDataInner.Message);

            writer.WriteNumber("retry_after", startCommercialResponseDataInner.RetryAfter);
        }
    }
}
