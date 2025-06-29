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
    /// GetChannelEmotesResponse
    /// </summary>
    public partial class GetChannelEmotesResponse : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetChannelEmotesResponse" /> class.
        /// </summary>
        /// <param name="data">The list of emotes that the specified broadcaster created. If the broadcaster hasn&#39;t created custom emotes, the list is empty.</param>
        /// <param name="template">A templated URL. Use the values from the &#x60;id&#x60;, &#x60;format&#x60;, &#x60;scale&#x60;, and &#x60;theme_mode&#x60; fields to replace the like-named placeholder strings in the templated URL to create a CDN (content delivery network) URL that you use to fetch the emote. For information about what the template looks like and how to use it to fetch emotes, see [Emote CDN URL format](https://dev.twitch.tv/docs/irc/emotes#cdn-template). You should use this template instead of using the URLs in the &#x60;images&#x60; object.</param>
        [JsonConstructor]
        public GetChannelEmotesResponse(List<ChannelEmote> data, string template)
        {
            Data = data;
            Template = template;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The list of emotes that the specified broadcaster created. If the broadcaster hasn&#39;t created custom emotes, the list is empty.
        /// </summary>
        /// <value>The list of emotes that the specified broadcaster created. If the broadcaster hasn&#39;t created custom emotes, the list is empty.</value>
        [JsonPropertyName("data")]
        public List<ChannelEmote> Data { get; set; }

        /// <summary>
        /// A templated URL. Use the values from the &#x60;id&#x60;, &#x60;format&#x60;, &#x60;scale&#x60;, and &#x60;theme_mode&#x60; fields to replace the like-named placeholder strings in the templated URL to create a CDN (content delivery network) URL that you use to fetch the emote. For information about what the template looks like and how to use it to fetch emotes, see [Emote CDN URL format](https://dev.twitch.tv/docs/irc/emotes#cdn-template). You should use this template instead of using the URLs in the &#x60;images&#x60; object.
        /// </summary>
        /// <value>A templated URL. Use the values from the &#x60;id&#x60;, &#x60;format&#x60;, &#x60;scale&#x60;, and &#x60;theme_mode&#x60; fields to replace the like-named placeholder strings in the templated URL to create a CDN (content delivery network) URL that you use to fetch the emote. For information about what the template looks like and how to use it to fetch emotes, see [Emote CDN URL format](https://dev.twitch.tv/docs/irc/emotes#cdn-template). You should use this template instead of using the URLs in the &#x60;images&#x60; object.</value>
        [JsonPropertyName("template")]
        public string Template { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetChannelEmotesResponse {\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
            sb.Append("  Template: ").Append(Template).Append("\n");
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
    /// A Json converter for type <see cref="GetChannelEmotesResponse" />
    /// </summary>
    public class GetChannelEmotesResponseJsonConverter : JsonConverter<GetChannelEmotesResponse>
    {
        /// <summary>
        /// Deserializes json to <see cref="GetChannelEmotesResponse" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override GetChannelEmotesResponse Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<List<ChannelEmote>?> data = default;
            Option<string?> template = default;

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
                            data = new Option<List<ChannelEmote>?>(JsonSerializer.Deserialize<List<ChannelEmote>>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        case "template":
                            template = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!data.IsSet)
                throw new ArgumentException("Property is required for class GetChannelEmotesResponse.", nameof(data));

            if (!template.IsSet)
                throw new ArgumentException("Property is required for class GetChannelEmotesResponse.", nameof(template));

            if (data.IsSet && data.Value == null)
                throw new ArgumentNullException(nameof(data), "Property is not nullable for class GetChannelEmotesResponse.");

            if (template.IsSet && template.Value == null)
                throw new ArgumentNullException(nameof(template), "Property is not nullable for class GetChannelEmotesResponse.");

            return new GetChannelEmotesResponse(data.Value!, template.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="GetChannelEmotesResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getChannelEmotesResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, GetChannelEmotesResponse getChannelEmotesResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, getChannelEmotesResponse, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="GetChannelEmotesResponse" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="getChannelEmotesResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, GetChannelEmotesResponse getChannelEmotesResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            if (getChannelEmotesResponse.Data == null)
                throw new ArgumentNullException(nameof(getChannelEmotesResponse.Data), "Property is required for class GetChannelEmotesResponse.");

            if (getChannelEmotesResponse.Template == null)
                throw new ArgumentNullException(nameof(getChannelEmotesResponse.Template), "Property is required for class GetChannelEmotesResponse.");

            writer.WritePropertyName("data");
            JsonSerializer.Serialize(writer, getChannelEmotesResponse.Data, jsonSerializerOptions);
            writer.WriteString("template", getChannelEmotesResponse.Template);
        }
    }
}
