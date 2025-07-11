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
    /// ExtensionSecret
    /// </summary>
    public partial class ExtensionSecret : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionSecret" /> class.
        /// </summary>
        /// <param name="formatVersion">The version number that identifies this definition of the secret’s data.</param>
        /// <param name="secrets">The list of secrets.</param>
        [JsonConstructor]
        public ExtensionSecret(int formatVersion, List<ExtensionSecretSecretsInner> secrets)
        {
            FormatVersion = formatVersion;
            Secrets = secrets;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The version number that identifies this definition of the secret’s data.
        /// </summary>
        /// <value>The version number that identifies this definition of the secret’s data.</value>
        [JsonPropertyName("format_version")]
        public int FormatVersion { get; set; }

        /// <summary>
        /// The list of secrets.
        /// </summary>
        /// <value>The list of secrets.</value>
        [JsonPropertyName("secrets")]
        public List<ExtensionSecretSecretsInner> Secrets { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ExtensionSecret {\n");
            sb.Append("  FormatVersion: ").Append(FormatVersion).Append("\n");
            sb.Append("  Secrets: ").Append(Secrets).Append("\n");
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
    /// A Json converter for type <see cref="ExtensionSecret" />
    /// </summary>
    public class ExtensionSecretJsonConverter : JsonConverter<ExtensionSecret>
    {
        /// <summary>
        /// Deserializes json to <see cref="ExtensionSecret" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override ExtensionSecret Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<int?> formatVersion = default;
            Option<List<ExtensionSecretSecretsInner>?> secrets = default;

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
                        case "format_version":
                            formatVersion = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "secrets":
                            secrets = new Option<List<ExtensionSecretSecretsInner>?>(JsonSerializer.Deserialize<List<ExtensionSecretSecretsInner>>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!formatVersion.IsSet)
                throw new ArgumentException("Property is required for class ExtensionSecret.", nameof(formatVersion));

            if (!secrets.IsSet)
                throw new ArgumentException("Property is required for class ExtensionSecret.", nameof(secrets));

            if (formatVersion.IsSet && formatVersion.Value == null)
                throw new ArgumentNullException(nameof(formatVersion), "Property is not nullable for class ExtensionSecret.");

            if (secrets.IsSet && secrets.Value == null)
                throw new ArgumentNullException(nameof(secrets), "Property is not nullable for class ExtensionSecret.");

            return new ExtensionSecret(formatVersion.Value!.Value!, secrets.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="ExtensionSecret" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="extensionSecret"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, ExtensionSecret extensionSecret, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, extensionSecret, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="ExtensionSecret" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="extensionSecret"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, ExtensionSecret extensionSecret, JsonSerializerOptions jsonSerializerOptions)
        {
            if (extensionSecret.Secrets == null)
                throw new ArgumentNullException(nameof(extensionSecret.Secrets), "Property is required for class ExtensionSecret.");

            writer.WriteNumber("format_version", extensionSecret.FormatVersion);

            writer.WritePropertyName("secrets");
            JsonSerializer.Serialize(writer, extensionSecret.Secrets, jsonSerializerOptions);
        }
    }
}
