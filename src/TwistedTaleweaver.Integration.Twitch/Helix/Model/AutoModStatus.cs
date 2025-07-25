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
    /// AutoModStatus
    /// </summary>
    public partial class AutoModStatus : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoModStatus" /> class.
        /// </summary>
        /// <param name="msgId">The caller-defined ID passed in the request.</param>
        /// <param name="isPermitted">A Boolean value that indicates whether Twitch would approve the message for chat or hold it for moderator review or block it from chat. Is **true** if Twitch would approve the message; otherwise, **false** if Twitch would hold the message for moderator review or block it from chat.</param>
        [JsonConstructor]
        public AutoModStatus(string msgId, bool isPermitted)
        {
            MsgId = msgId;
            IsPermitted = isPermitted;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The caller-defined ID passed in the request.
        /// </summary>
        /// <value>The caller-defined ID passed in the request.</value>
        [JsonPropertyName("msg_id")]
        public string MsgId { get; set; }

        /// <summary>
        /// A Boolean value that indicates whether Twitch would approve the message for chat or hold it for moderator review or block it from chat. Is **true** if Twitch would approve the message; otherwise, **false** if Twitch would hold the message for moderator review or block it from chat.
        /// </summary>
        /// <value>A Boolean value that indicates whether Twitch would approve the message for chat or hold it for moderator review or block it from chat. Is **true** if Twitch would approve the message; otherwise, **false** if Twitch would hold the message for moderator review or block it from chat.</value>
        [JsonPropertyName("is_permitted")]
        public bool IsPermitted { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class AutoModStatus {\n");
            sb.Append("  MsgId: ").Append(MsgId).Append("\n");
            sb.Append("  IsPermitted: ").Append(IsPermitted).Append("\n");
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
    /// A Json converter for type <see cref="AutoModStatus" />
    /// </summary>
    public class AutoModStatusJsonConverter : JsonConverter<AutoModStatus>
    {
        /// <summary>
        /// Deserializes json to <see cref="AutoModStatus" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override AutoModStatus Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> msgId = default;
            Option<bool?> isPermitted = default;

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
                        case "msg_id":
                            msgId = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "is_permitted":
                            isPermitted = new Option<bool?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (bool?)null : utf8JsonReader.GetBoolean());
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!msgId.IsSet)
                throw new ArgumentException("Property is required for class AutoModStatus.", nameof(msgId));

            if (!isPermitted.IsSet)
                throw new ArgumentException("Property is required for class AutoModStatus.", nameof(isPermitted));

            if (msgId.IsSet && msgId.Value == null)
                throw new ArgumentNullException(nameof(msgId), "Property is not nullable for class AutoModStatus.");

            if (isPermitted.IsSet && isPermitted.Value == null)
                throw new ArgumentNullException(nameof(isPermitted), "Property is not nullable for class AutoModStatus.");

            return new AutoModStatus(msgId.Value!, isPermitted.Value!.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="AutoModStatus" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="autoModStatus"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, AutoModStatus autoModStatus, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, autoModStatus, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="AutoModStatus" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="autoModStatus"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, AutoModStatus autoModStatus, JsonSerializerOptions jsonSerializerOptions)
        {
            if (autoModStatus.MsgId == null)
                throw new ArgumentNullException(nameof(autoModStatus.MsgId), "Property is required for class AutoModStatus.");

            writer.WriteString("msg_id", autoModStatus.MsgId);

            writer.WriteBoolean("is_permitted", autoModStatus.IsPermitted);
        }
    }
}
