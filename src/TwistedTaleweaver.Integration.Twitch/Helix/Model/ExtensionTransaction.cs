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
    /// ExtensionTransaction
    /// </summary>
    public partial class ExtensionTransaction : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionTransaction" /> class.
        /// </summary>
        /// <param name="id">An ID that identifies the transaction.</param>
        /// <param name="timestamp">The UTC date and time (in RFC3339 format) of the transaction.</param>
        /// <param name="broadcasterId">The ID of the broadcaster that owns the channel where the transaction occurred.</param>
        /// <param name="broadcasterLogin">The broadcaster’s login name.</param>
        /// <param name="broadcasterName">The broadcaster’s display name.</param>
        /// <param name="userId">The ID of the user that purchased the digital product.</param>
        /// <param name="userLogin">The user’s login name.</param>
        /// <param name="userName">The user’s display name.</param>
        /// <param name="productType">The type of transaction. Possible values are:      * BITS\\_IN\\_EXTENSION</param>
        /// <param name="productData">productData</param>
        [JsonConstructor]
        public ExtensionTransaction(string id, DateTime timestamp, string broadcasterId, string broadcasterLogin, string broadcasterName, string userId, string userLogin, string userName, ProductTypeEnum productType, ExtensionTransactionProductData productData)
        {
            Id = id;
            Timestamp = timestamp;
            BroadcasterId = broadcasterId;
            BroadcasterLogin = broadcasterLogin;
            BroadcasterName = broadcasterName;
            UserId = userId;
            UserLogin = userLogin;
            UserName = userName;
            ProductType = productType;
            ProductData = productData;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The type of transaction. Possible values are:      * BITS\\_IN\\_EXTENSION
        /// </summary>
        /// <value>The type of transaction. Possible values are:      * BITS\\_IN\\_EXTENSION</value>
        public enum ProductTypeEnum
        {
            /// <summary>
            /// Enum BITSINEXTENSION for value: BITS_IN_EXTENSION
            /// </summary>
            BITSINEXTENSION = 1
        }

        /// <summary>
        /// Returns a <see cref="ProductTypeEnum"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ProductTypeEnum ProductTypeEnumFromString(string value)
        {
            if (value.Equals("BITS_IN_EXTENSION"))
                return ProductTypeEnum.BITSINEXTENSION;

            throw new NotImplementedException($"Could not convert value to type ProductTypeEnum: '{value}'");
        }

        /// <summary>
        /// Returns a <see cref="ProductTypeEnum"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ProductTypeEnum? ProductTypeEnumFromStringOrDefault(string value)
        {
            if (value.Equals("BITS_IN_EXTENSION"))
                return ProductTypeEnum.BITSINEXTENSION;

            return null;
        }

        /// <summary>
        /// Converts the <see cref="ProductTypeEnum"/> to the json value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string ProductTypeEnumToJsonValue(ProductTypeEnum value)
        {
            if (value == ProductTypeEnum.BITSINEXTENSION)
                return "BITS_IN_EXTENSION";

            throw new NotImplementedException($"Value could not be handled: '{value}'");
        }

        /// <summary>
        /// The type of transaction. Possible values are:      * BITS\\_IN\\_EXTENSION
        /// </summary>
        /// <value>The type of transaction. Possible values are:      * BITS\\_IN\\_EXTENSION</value>
        [JsonPropertyName("product_type")]
        public ProductTypeEnum ProductType { get; set; }

        /// <summary>
        /// An ID that identifies the transaction.
        /// </summary>
        /// <value>An ID that identifies the transaction.</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The UTC date and time (in RFC3339 format) of the transaction.
        /// </summary>
        /// <value>The UTC date and time (in RFC3339 format) of the transaction.</value>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The ID of the broadcaster that owns the channel where the transaction occurred.
        /// </summary>
        /// <value>The ID of the broadcaster that owns the channel where the transaction occurred.</value>
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; set; }

        /// <summary>
        /// The broadcaster’s login name.
        /// </summary>
        /// <value>The broadcaster’s login name.</value>
        [JsonPropertyName("broadcaster_login")]
        public string BroadcasterLogin { get; set; }

        /// <summary>
        /// The broadcaster’s display name.
        /// </summary>
        /// <value>The broadcaster’s display name.</value>
        [JsonPropertyName("broadcaster_name")]
        public string BroadcasterName { get; set; }

        /// <summary>
        /// The ID of the user that purchased the digital product.
        /// </summary>
        /// <value>The ID of the user that purchased the digital product.</value>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// The user’s login name.
        /// </summary>
        /// <value>The user’s login name.</value>
        [JsonPropertyName("user_login")]
        public string UserLogin { get; set; }

        /// <summary>
        /// The user’s display name.
        /// </summary>
        /// <value>The user’s display name.</value>
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or Sets ProductData
        /// </summary>
        [JsonPropertyName("product_data")]
        public ExtensionTransactionProductData ProductData { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ExtensionTransaction {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Timestamp: ").Append(Timestamp).Append("\n");
            sb.Append("  BroadcasterId: ").Append(BroadcasterId).Append("\n");
            sb.Append("  BroadcasterLogin: ").Append(BroadcasterLogin).Append("\n");
            sb.Append("  BroadcasterName: ").Append(BroadcasterName).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  UserLogin: ").Append(UserLogin).Append("\n");
            sb.Append("  UserName: ").Append(UserName).Append("\n");
            sb.Append("  ProductType: ").Append(ProductType).Append("\n");
            sb.Append("  ProductData: ").Append(ProductData).Append("\n");
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
    /// A Json converter for type <see cref="ExtensionTransaction" />
    /// </summary>
    public class ExtensionTransactionJsonConverter : JsonConverter<ExtensionTransaction>
    {
        /// <summary>
        /// The format to use to serialize Timestamp
        /// </summary>
        public static string TimestampFormat { get; set; } = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";

        /// <summary>
        /// Deserializes json to <see cref="ExtensionTransaction" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override ExtensionTransaction Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> id = default;
            Option<DateTime?> timestamp = default;
            Option<string?> broadcasterId = default;
            Option<string?> broadcasterLogin = default;
            Option<string?> broadcasterName = default;
            Option<string?> userId = default;
            Option<string?> userLogin = default;
            Option<string?> userName = default;
            Option<ExtensionTransaction.ProductTypeEnum?> productType = default;
            Option<ExtensionTransactionProductData?> productData = default;

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
                        case "timestamp":
                            timestamp = new Option<DateTime?>(JsonSerializer.Deserialize<DateTime>(ref utf8JsonReader, jsonSerializerOptions));
                            break;
                        case "broadcaster_id":
                            broadcasterId = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "broadcaster_login":
                            broadcasterLogin = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "broadcaster_name":
                            broadcasterName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "user_id":
                            userId = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "user_login":
                            userLogin = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "user_name":
                            userName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "product_type":
                            string? productTypeRawValue = utf8JsonReader.GetString();
                            if (productTypeRawValue != null)
                                productType = new Option<ExtensionTransaction.ProductTypeEnum?>(ExtensionTransaction.ProductTypeEnumFromStringOrDefault(productTypeRawValue));
                            break;
                        case "product_data":
                            productData = new Option<ExtensionTransactionProductData?>(JsonSerializer.Deserialize<ExtensionTransactionProductData>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!id.IsSet)
                throw new ArgumentException("Property is required for class ExtensionTransaction.", nameof(id));

            if (!timestamp.IsSet)
                throw new ArgumentException("Property is required for class ExtensionTransaction.", nameof(timestamp));

            if (!broadcasterId.IsSet)
                throw new ArgumentException("Property is required for class ExtensionTransaction.", nameof(broadcasterId));

            if (!broadcasterLogin.IsSet)
                throw new ArgumentException("Property is required for class ExtensionTransaction.", nameof(broadcasterLogin));

            if (!broadcasterName.IsSet)
                throw new ArgumentException("Property is required for class ExtensionTransaction.", nameof(broadcasterName));

            if (!userId.IsSet)
                throw new ArgumentException("Property is required for class ExtensionTransaction.", nameof(userId));

            if (!userLogin.IsSet)
                throw new ArgumentException("Property is required for class ExtensionTransaction.", nameof(userLogin));

            if (!userName.IsSet)
                throw new ArgumentException("Property is required for class ExtensionTransaction.", nameof(userName));

            if (!productType.IsSet)
                throw new ArgumentException("Property is required for class ExtensionTransaction.", nameof(productType));

            if (!productData.IsSet)
                throw new ArgumentException("Property is required for class ExtensionTransaction.", nameof(productData));

            if (id.IsSet && id.Value == null)
                throw new ArgumentNullException(nameof(id), "Property is not nullable for class ExtensionTransaction.");

            if (timestamp.IsSet && timestamp.Value == null)
                throw new ArgumentNullException(nameof(timestamp), "Property is not nullable for class ExtensionTransaction.");

            if (broadcasterId.IsSet && broadcasterId.Value == null)
                throw new ArgumentNullException(nameof(broadcasterId), "Property is not nullable for class ExtensionTransaction.");

            if (broadcasterLogin.IsSet && broadcasterLogin.Value == null)
                throw new ArgumentNullException(nameof(broadcasterLogin), "Property is not nullable for class ExtensionTransaction.");

            if (broadcasterName.IsSet && broadcasterName.Value == null)
                throw new ArgumentNullException(nameof(broadcasterName), "Property is not nullable for class ExtensionTransaction.");

            if (userId.IsSet && userId.Value == null)
                throw new ArgumentNullException(nameof(userId), "Property is not nullable for class ExtensionTransaction.");

            if (userLogin.IsSet && userLogin.Value == null)
                throw new ArgumentNullException(nameof(userLogin), "Property is not nullable for class ExtensionTransaction.");

            if (userName.IsSet && userName.Value == null)
                throw new ArgumentNullException(nameof(userName), "Property is not nullable for class ExtensionTransaction.");

            if (productType.IsSet && productType.Value == null)
                throw new ArgumentNullException(nameof(productType), "Property is not nullable for class ExtensionTransaction.");

            if (productData.IsSet && productData.Value == null)
                throw new ArgumentNullException(nameof(productData), "Property is not nullable for class ExtensionTransaction.");

            return new ExtensionTransaction(id.Value!, timestamp.Value!.Value!, broadcasterId.Value!, broadcasterLogin.Value!, broadcasterName.Value!, userId.Value!, userLogin.Value!, userName.Value!, productType.Value!.Value!, productData.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="ExtensionTransaction" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="extensionTransaction"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, ExtensionTransaction extensionTransaction, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, extensionTransaction, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="ExtensionTransaction" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="extensionTransaction"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, ExtensionTransaction extensionTransaction, JsonSerializerOptions jsonSerializerOptions)
        {
            if (extensionTransaction.Id == null)
                throw new ArgumentNullException(nameof(extensionTransaction.Id), "Property is required for class ExtensionTransaction.");

            if (extensionTransaction.BroadcasterId == null)
                throw new ArgumentNullException(nameof(extensionTransaction.BroadcasterId), "Property is required for class ExtensionTransaction.");

            if (extensionTransaction.BroadcasterLogin == null)
                throw new ArgumentNullException(nameof(extensionTransaction.BroadcasterLogin), "Property is required for class ExtensionTransaction.");

            if (extensionTransaction.BroadcasterName == null)
                throw new ArgumentNullException(nameof(extensionTransaction.BroadcasterName), "Property is required for class ExtensionTransaction.");

            if (extensionTransaction.UserId == null)
                throw new ArgumentNullException(nameof(extensionTransaction.UserId), "Property is required for class ExtensionTransaction.");

            if (extensionTransaction.UserLogin == null)
                throw new ArgumentNullException(nameof(extensionTransaction.UserLogin), "Property is required for class ExtensionTransaction.");

            if (extensionTransaction.UserName == null)
                throw new ArgumentNullException(nameof(extensionTransaction.UserName), "Property is required for class ExtensionTransaction.");

            if (extensionTransaction.ProductData == null)
                throw new ArgumentNullException(nameof(extensionTransaction.ProductData), "Property is required for class ExtensionTransaction.");

            writer.WriteString("id", extensionTransaction.Id);

            writer.WriteString("timestamp", extensionTransaction.Timestamp.ToString(TimestampFormat));

            writer.WriteString("broadcaster_id", extensionTransaction.BroadcasterId);

            writer.WriteString("broadcaster_login", extensionTransaction.BroadcasterLogin);

            writer.WriteString("broadcaster_name", extensionTransaction.BroadcasterName);

            writer.WriteString("user_id", extensionTransaction.UserId);

            writer.WriteString("user_login", extensionTransaction.UserLogin);

            writer.WriteString("user_name", extensionTransaction.UserName);

            var productTypeRawValue = ExtensionTransaction.ProductTypeEnumToJsonValue(extensionTransaction.ProductType);
            writer.WriteString("product_type", productTypeRawValue);
            writer.WritePropertyName("product_data");
            JsonSerializer.Serialize(writer, extensionTransaction.ProductData, jsonSerializerOptions);
        }
    }
}
