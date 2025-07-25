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
    /// ExtensionBitsProduct
    /// </summary>
    public partial class ExtensionBitsProduct : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionBitsProduct" /> class.
        /// </summary>
        /// <param name="sku">The product&#39;s SKU. The SKU is unique across an extension&#39;s products.</param>
        /// <param name="cost">cost</param>
        /// <param name="inDevelopment">A Boolean value that indicates whether the product is in development. If **true**, the product is not available for public use.</param>
        /// <param name="displayName">The product&#39;s name as displayed in the extension.</param>
        /// <param name="expiration">The date and time, in RFC3339 format, when the product expires.</param>
        /// <param name="isBroadcast">A Boolean value that determines whether Bits product purchase events are broadcast to all instances of an extension on a channel. The events are broadcast via the &#x60;onTransactionComplete&#x60; helper callback. Is **true** if the event is broadcast to all instances.</param>
        [JsonConstructor]
        public ExtensionBitsProduct(string sku, ExtensionBitsProductCost cost, bool inDevelopment, string displayName, DateTime expiration, bool isBroadcast)
        {
            Sku = sku;
            Cost = cost;
            InDevelopment = inDevelopment;
            DisplayName = displayName;
            Expiration = expiration;
            IsBroadcast = isBroadcast;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The product&#39;s SKU. The SKU is unique across an extension&#39;s products.
        /// </summary>
        /// <value>The product&#39;s SKU. The SKU is unique across an extension&#39;s products.</value>
        [JsonPropertyName("sku")]
        public string Sku { get; set; }

        /// <summary>
        /// Gets or Sets Cost
        /// </summary>
        [JsonPropertyName("cost")]
        public ExtensionBitsProductCost Cost { get; set; }

        /// <summary>
        /// A Boolean value that indicates whether the product is in development. If **true**, the product is not available for public use.
        /// </summary>
        /// <value>A Boolean value that indicates whether the product is in development. If **true**, the product is not available for public use.</value>
        [JsonPropertyName("in_development")]
        public bool InDevelopment { get; set; }

        /// <summary>
        /// The product&#39;s name as displayed in the extension.
        /// </summary>
        /// <value>The product&#39;s name as displayed in the extension.</value>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The date and time, in RFC3339 format, when the product expires.
        /// </summary>
        /// <value>The date and time, in RFC3339 format, when the product expires.</value>
        [JsonPropertyName("expiration")]
        public DateTime Expiration { get; set; }

        /// <summary>
        /// A Boolean value that determines whether Bits product purchase events are broadcast to all instances of an extension on a channel. The events are broadcast via the &#x60;onTransactionComplete&#x60; helper callback. Is **true** if the event is broadcast to all instances.
        /// </summary>
        /// <value>A Boolean value that determines whether Bits product purchase events are broadcast to all instances of an extension on a channel. The events are broadcast via the &#x60;onTransactionComplete&#x60; helper callback. Is **true** if the event is broadcast to all instances.</value>
        [JsonPropertyName("is_broadcast")]
        public bool IsBroadcast { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ExtensionBitsProduct {\n");
            sb.Append("  Sku: ").Append(Sku).Append("\n");
            sb.Append("  Cost: ").Append(Cost).Append("\n");
            sb.Append("  InDevelopment: ").Append(InDevelopment).Append("\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("  Expiration: ").Append(Expiration).Append("\n");
            sb.Append("  IsBroadcast: ").Append(IsBroadcast).Append("\n");
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
    /// A Json converter for type <see cref="ExtensionBitsProduct" />
    /// </summary>
    public class ExtensionBitsProductJsonConverter : JsonConverter<ExtensionBitsProduct>
    {
        /// <summary>
        /// The format to use to serialize Expiration
        /// </summary>
        public static string ExpirationFormat { get; set; } = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";

        /// <summary>
        /// Deserializes json to <see cref="ExtensionBitsProduct" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override ExtensionBitsProduct Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> sku = default;
            Option<ExtensionBitsProductCost?> cost = default;
            Option<bool?> inDevelopment = default;
            Option<string?> displayName = default;
            Option<DateTime?> expiration = default;
            Option<bool?> isBroadcast = default;

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
                        case "sku":
                            sku = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "cost":
                            cost = new Option<ExtensionBitsProductCost?>(JsonSerializer.Deserialize<ExtensionBitsProductCost>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        case "in_development":
                            inDevelopment = new Option<bool?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (bool?)null : utf8JsonReader.GetBoolean());
                            break;
                        case "display_name":
                            displayName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "expiration":
                            expiration = new Option<DateTime?>(JsonSerializer.Deserialize<DateTime>(ref utf8JsonReader, jsonSerializerOptions));
                            break;
                        case "is_broadcast":
                            isBroadcast = new Option<bool?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (bool?)null : utf8JsonReader.GetBoolean());
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!sku.IsSet)
                throw new ArgumentException("Property is required for class ExtensionBitsProduct.", nameof(sku));

            if (!cost.IsSet)
                throw new ArgumentException("Property is required for class ExtensionBitsProduct.", nameof(cost));

            if (!inDevelopment.IsSet)
                throw new ArgumentException("Property is required for class ExtensionBitsProduct.", nameof(inDevelopment));

            if (!displayName.IsSet)
                throw new ArgumentException("Property is required for class ExtensionBitsProduct.", nameof(displayName));

            if (!expiration.IsSet)
                throw new ArgumentException("Property is required for class ExtensionBitsProduct.", nameof(expiration));

            if (!isBroadcast.IsSet)
                throw new ArgumentException("Property is required for class ExtensionBitsProduct.", nameof(isBroadcast));

            if (sku.IsSet && sku.Value == null)
                throw new ArgumentNullException(nameof(sku), "Property is not nullable for class ExtensionBitsProduct.");

            if (cost.IsSet && cost.Value == null)
                throw new ArgumentNullException(nameof(cost), "Property is not nullable for class ExtensionBitsProduct.");

            if (inDevelopment.IsSet && inDevelopment.Value == null)
                throw new ArgumentNullException(nameof(inDevelopment), "Property is not nullable for class ExtensionBitsProduct.");

            if (displayName.IsSet && displayName.Value == null)
                throw new ArgumentNullException(nameof(displayName), "Property is not nullable for class ExtensionBitsProduct.");

            if (expiration.IsSet && expiration.Value == null)
                throw new ArgumentNullException(nameof(expiration), "Property is not nullable for class ExtensionBitsProduct.");

            if (isBroadcast.IsSet && isBroadcast.Value == null)
                throw new ArgumentNullException(nameof(isBroadcast), "Property is not nullable for class ExtensionBitsProduct.");

            return new ExtensionBitsProduct(sku.Value!, cost.Value!, inDevelopment.Value!.Value!, displayName.Value!, expiration.Value!.Value!, isBroadcast.Value!.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="ExtensionBitsProduct" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="extensionBitsProduct"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, ExtensionBitsProduct extensionBitsProduct, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, extensionBitsProduct, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="ExtensionBitsProduct" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="extensionBitsProduct"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, ExtensionBitsProduct extensionBitsProduct, JsonSerializerOptions jsonSerializerOptions)
        {
            if (extensionBitsProduct.Sku == null)
                throw new ArgumentNullException(nameof(extensionBitsProduct.Sku), "Property is required for class ExtensionBitsProduct.");

            if (extensionBitsProduct.Cost == null)
                throw new ArgumentNullException(nameof(extensionBitsProduct.Cost), "Property is required for class ExtensionBitsProduct.");

            if (extensionBitsProduct.DisplayName == null)
                throw new ArgumentNullException(nameof(extensionBitsProduct.DisplayName), "Property is required for class ExtensionBitsProduct.");

            writer.WriteString("sku", extensionBitsProduct.Sku);

            writer.WritePropertyName("cost");
            JsonSerializer.Serialize(writer, extensionBitsProduct.Cost, jsonSerializerOptions);
            writer.WriteBoolean("in_development", extensionBitsProduct.InDevelopment);

            writer.WriteString("display_name", extensionBitsProduct.DisplayName);

            writer.WriteString("expiration", extensionBitsProduct.Expiration.ToString(ExpirationFormat));

            writer.WriteBoolean("is_broadcast", extensionBitsProduct.IsBroadcast);
        }
    }
}
