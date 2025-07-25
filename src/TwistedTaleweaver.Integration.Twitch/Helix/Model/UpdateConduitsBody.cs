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
    /// UpdateConduitsBody
    /// </summary>
    public partial class UpdateConduitsBody : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateConduitsBody" /> class.
        /// </summary>
        /// <param name="id">Conduit ID.</param>
        /// <param name="shardCount">The new number of shards for this conduit.</param>
        [JsonConstructor]
        public UpdateConduitsBody(string id, int shardCount)
        {
            Id = id;
            ShardCount = shardCount;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// Conduit ID.
        /// </summary>
        /// <value>Conduit ID.</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The new number of shards for this conduit.
        /// </summary>
        /// <value>The new number of shards for this conduit.</value>
        [JsonPropertyName("shard_count")]
        public int ShardCount { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UpdateConduitsBody {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  ShardCount: ").Append(ShardCount).Append("\n");
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
    /// A Json converter for type <see cref="UpdateConduitsBody" />
    /// </summary>
    public class UpdateConduitsBodyJsonConverter : JsonConverter<UpdateConduitsBody>
    {
        /// <summary>
        /// Deserializes json to <see cref="UpdateConduitsBody" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override UpdateConduitsBody Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> id = default;
            Option<int?> shardCount = default;

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
                        case "shard_count":
                            shardCount = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!id.IsSet)
                throw new ArgumentException("Property is required for class UpdateConduitsBody.", nameof(id));

            if (!shardCount.IsSet)
                throw new ArgumentException("Property is required for class UpdateConduitsBody.", nameof(shardCount));

            if (id.IsSet && id.Value == null)
                throw new ArgumentNullException(nameof(id), "Property is not nullable for class UpdateConduitsBody.");

            if (shardCount.IsSet && shardCount.Value == null)
                throw new ArgumentNullException(nameof(shardCount), "Property is not nullable for class UpdateConduitsBody.");

            return new UpdateConduitsBody(id.Value!, shardCount.Value!.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="UpdateConduitsBody" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="updateConduitsBody"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, UpdateConduitsBody updateConduitsBody, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, updateConduitsBody, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="UpdateConduitsBody" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="updateConduitsBody"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, UpdateConduitsBody updateConduitsBody, JsonSerializerOptions jsonSerializerOptions)
        {
            if (updateConduitsBody.Id == null)
                throw new ArgumentNullException(nameof(updateConduitsBody.Id), "Property is required for class UpdateConduitsBody.");

            writer.WriteString("id", updateConduitsBody.Id);

            writer.WriteNumber("shard_count", updateConduitsBody.ShardCount);
        }
    }
}
