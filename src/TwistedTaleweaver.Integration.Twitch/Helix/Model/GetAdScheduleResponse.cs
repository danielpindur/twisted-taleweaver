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
    /// GetAdScheduleResponse
    /// </summary>
    public partial class GetAdScheduleResponse : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAdScheduleResponse" /> class.
        /// </summary>
        /// <param name="data">A list that contains information related to the channel’s ad schedule.</param>
        [JsonConstructor]
        public GetAdScheduleResponse(List<GetAdScheduleResponseDataInner> data)
        {
            Data = data;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// A list that contains information related to the channel’s ad schedule.
        /// </summary>
        /// <value>A list that contains information related to the channel’s ad schedule.</value>
        [JsonPropertyName("data")]
        public List<GetAdScheduleResponseDataInner> Data { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetAdScheduleResponse {\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
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
    /// A Json converter for type <see cref="GetAdScheduleResponse" />
    /// </summary>
    public class GetAdScheduleResponseJsonConverter : JsonConverter<GetAdScheduleResponse>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetAdScheduleResponse" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetAdScheduleResponse Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<List<GetAdScheduleResponseDataInner>?> data = default;

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
                            data = new Option<List<GetAdScheduleResponseDataInner>?>(JsonSerializer.Deserialize<List<GetAdScheduleResponseDataInner>>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!data.IsSet)
                throw new ArgumentException("Property is required for class GetAdScheduleResponse.", nameof(data));

            if (data.IsSet && data.Value == null)
                throw new ArgumentNullException(nameof(data), "Property is not nullable for class GetAdScheduleResponse.");

            return new GetAdScheduleResponse(data.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="GetAdScheduleResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getAdScheduleResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetAdScheduleResponse getAdScheduleResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, getAdScheduleResponse, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetAdScheduleResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getAdScheduleResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, GetAdScheduleResponse getAdScheduleResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            if (getAdScheduleResponse.Data == null)
                throw new ArgumentNullException(nameof(getAdScheduleResponse.Data), "Property is required for class GetAdScheduleResponse.");

            writer.WritePropertyName("data");
            JsonSerializer.Serialize(writer, getAdScheduleResponse.Data, jsonSerializerOptions);
        }
    }
}
