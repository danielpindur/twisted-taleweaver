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
    /// UpdateConduitShardsResponse
    /// </summary>
    public partial class UpdateConduitShardsResponse : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateConduitShardsResponse" /> class.
        /// </summary>
        /// <param name="data">List of successful shard updates.</param>
        /// <param name="errors">List of unsuccessful updates.</param>
        [JsonConstructor]
        public UpdateConduitShardsResponse(List<GetConduitShardsResponseDataInner> data, List<UpdateConduitShardsResponseErrorsInner> errors)
        {
            Data = data;
            Errors = errors;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// List of successful shard updates.
        /// </summary>
        /// <value>List of successful shard updates.</value>
        [JsonPropertyName("data")]
        public List<GetConduitShardsResponseDataInner> Data { get; set; }

        /// <summary>
        /// List of unsuccessful updates.
        /// </summary>
        /// <value>List of unsuccessful updates.</value>
        [JsonPropertyName("errors")]
        public List<UpdateConduitShardsResponseErrorsInner> Errors { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UpdateConduitShardsResponse {\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
            sb.Append("  Errors: ").Append(Errors).Append("\n");
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
    /// A Json converter for type <see cref="UpdateConduitShardsResponse" />
    /// </summary>
    public class UpdateConduitShardsResponseJsonConverter : JsonConverter<UpdateConduitShardsResponse>
    {
        /// <summary>
        /// Deserializes json to <see cref="UpdateConduitShardsResponse" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override UpdateConduitShardsResponse Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<List<GetConduitShardsResponseDataInner>?> data = default;
            Option<List<UpdateConduitShardsResponseErrorsInner>?> errors = default;

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
                        case "data":
                            data = new Option<List<GetConduitShardsResponseDataInner>?>(JsonSerializer.Deserialize<List<GetConduitShardsResponseDataInner>>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        case "errors":
                            errors = new Option<List<UpdateConduitShardsResponseErrorsInner>?>(JsonSerializer.Deserialize<List<UpdateConduitShardsResponseErrorsInner>>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!data.IsSet)
                throw new ArgumentException("Property is required for class UpdateConduitShardsResponse.", nameof(data));

            if (!errors.IsSet)
                throw new ArgumentException("Property is required for class UpdateConduitShardsResponse.", nameof(errors));

            if (data.IsSet && data.Value == null)
                throw new ArgumentNullException(nameof(data), "Property is not nullable for class UpdateConduitShardsResponse.");

            if (errors.IsSet && errors.Value == null)
                throw new ArgumentNullException(nameof(errors), "Property is not nullable for class UpdateConduitShardsResponse.");

            return new UpdateConduitShardsResponse(data.Value!, errors.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="UpdateConduitShardsResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="updateConduitShardsResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, UpdateConduitShardsResponse updateConduitShardsResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, updateConduitShardsResponse, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="UpdateConduitShardsResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="updateConduitShardsResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, UpdateConduitShardsResponse updateConduitShardsResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            if (updateConduitShardsResponse.Data == null)
                throw new ArgumentNullException(nameof(updateConduitShardsResponse.Data), "Property is required for class UpdateConduitShardsResponse.");

            if (updateConduitShardsResponse.Errors == null)
                throw new ArgumentNullException(nameof(updateConduitShardsResponse.Errors), "Property is required for class UpdateConduitShardsResponse.");

            writer.WritePropertyName("data");
            JsonSerializer.Serialize(writer, updateConduitShardsResponse.Data, jsonSerializerOptions);
            writer.WritePropertyName("errors");
            JsonSerializer.Serialize(writer, updateConduitShardsResponse.Errors, jsonSerializerOptions);
        }
    }
}
