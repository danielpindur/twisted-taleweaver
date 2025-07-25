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
    /// Contains the information used to page through the list of results. The object is empty if there are no more pages left to page through. [Read More](https://dev.twitch.tv/docs/api/guide#pagination)
    /// </summary>
    public partial class GetPollsResponsePagination : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPollsResponsePagination" /> class.
        /// </summary>
        /// <param name="cursor">The cursor used to get the next page of results. Use the cursor to set the request&#39;s _after_ query parameter.</param>
        [JsonConstructor]
        public GetPollsResponsePagination(Option<string?> cursor = default)
        {
            CursorOption = cursor;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// Used to track the state of Cursor
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<string?> CursorOption { get; private set; }

        /// <summary>
        /// The cursor used to get the next page of results. Use the cursor to set the request&#39;s _after_ query parameter.
        /// </summary>
        /// <value>The cursor used to get the next page of results. Use the cursor to set the request&#39;s _after_ query parameter.</value>
        [JsonPropertyName("cursor")]
        public string? Cursor { get { return this.CursorOption; } set { this.CursorOption = new(value); } }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetPollsResponsePagination {\n");
            sb.Append("  Cursor: ").Append(Cursor).Append("\n");
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
    /// A Json converter for type <see cref="GetPollsResponsePagination" />
    /// </summary>
    public class GetPollsResponsePaginationJsonConverter : JsonConverter<GetPollsResponsePagination>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetPollsResponsePagination" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetPollsResponsePagination Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> cursor = default;

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
                        case "cursor":
                            cursor = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (cursor.IsSet && cursor.Value == null)
                throw new ArgumentNullException(nameof(cursor), "Property is not nullable for class GetPollsResponsePagination.");

            return new GetPollsResponsePagination(cursor);
        }

        /// <summary>
        /// Serializes a <see cref="GetPollsResponsePagination" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getPollsResponsePagination"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetPollsResponsePagination getPollsResponsePagination, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, getPollsResponsePagination, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetPollsResponsePagination" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getPollsResponsePagination"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, GetPollsResponsePagination getPollsResponsePagination, JsonSerializerOptions jsonSerializerOptions)
        {
            if (getPollsResponsePagination.CursorOption.IsSet && getPollsResponsePagination.Cursor == null)
                throw new ArgumentNullException(nameof(getPollsResponsePagination.Cursor), "Property is required for class GetPollsResponsePagination.");

            if (getPollsResponsePagination.CursorOption.IsSet)
                writer.WriteString("cursor", getPollsResponsePagination.Cursor);
        }
    }
}
