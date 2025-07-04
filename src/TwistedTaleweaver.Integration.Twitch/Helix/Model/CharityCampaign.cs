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
    /// CharityCampaign
    /// </summary>
    public partial class CharityCampaign : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharityCampaign" /> class.
        /// </summary>
        /// <param name="id">An ID that identifies the charity campaign.</param>
        /// <param name="broadcasterId">An ID that identifies the broadcaster that’s running the campaign.</param>
        /// <param name="broadcasterLogin">The broadcaster’s login name.</param>
        /// <param name="broadcasterName">The broadcaster’s display name.</param>
        /// <param name="charityName">The charity’s name.</param>
        /// <param name="charityDescription">A description of the charity.</param>
        /// <param name="charityLogo">A URL to an image of the charity’s logo. The image’s type is PNG and its size is 100px X 100px.</param>
        /// <param name="charityWebsite">A URL to the charity’s website.</param>
        /// <param name="currentAmount">currentAmount</param>
        /// <param name="targetAmount">targetAmount</param>
        [JsonConstructor]
        public CharityCampaign(string id, string broadcasterId, string broadcasterLogin, string broadcasterName, string charityName, string charityDescription, string charityLogo, string charityWebsite, CharityCampaignCurrentAmount currentAmount, CharityCampaignTargetAmount targetAmount)
        {
            Id = id;
            BroadcasterId = broadcasterId;
            BroadcasterLogin = broadcasterLogin;
            BroadcasterName = broadcasterName;
            CharityName = charityName;
            CharityDescription = charityDescription;
            CharityLogo = charityLogo;
            CharityWebsite = charityWebsite;
            CurrentAmount = currentAmount;
            TargetAmount = targetAmount;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// An ID that identifies the charity campaign.
        /// </summary>
        /// <value>An ID that identifies the charity campaign.</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// An ID that identifies the broadcaster that’s running the campaign.
        /// </summary>
        /// <value>An ID that identifies the broadcaster that’s running the campaign.</value>
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
        /// The charity’s name.
        /// </summary>
        /// <value>The charity’s name.</value>
        [JsonPropertyName("charity_name")]
        public string CharityName { get; set; }

        /// <summary>
        /// A description of the charity.
        /// </summary>
        /// <value>A description of the charity.</value>
        [JsonPropertyName("charity_description")]
        public string CharityDescription { get; set; }

        /// <summary>
        /// A URL to an image of the charity’s logo. The image’s type is PNG and its size is 100px X 100px.
        /// </summary>
        /// <value>A URL to an image of the charity’s logo. The image’s type is PNG and its size is 100px X 100px.</value>
        [JsonPropertyName("charity_logo")]
        public string CharityLogo { get; set; }

        /// <summary>
        /// A URL to the charity’s website.
        /// </summary>
        /// <value>A URL to the charity’s website.</value>
        [JsonPropertyName("charity_website")]
        public string CharityWebsite { get; set; }

        /// <summary>
        /// Gets or Sets CurrentAmount
        /// </summary>
        [JsonPropertyName("current_amount")]
        public CharityCampaignCurrentAmount CurrentAmount { get; set; }

        /// <summary>
        /// Gets or Sets TargetAmount
        /// </summary>
        [JsonPropertyName("target_amount")]
        public CharityCampaignTargetAmount TargetAmount { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CharityCampaign {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  BroadcasterId: ").Append(BroadcasterId).Append("\n");
            sb.Append("  BroadcasterLogin: ").Append(BroadcasterLogin).Append("\n");
            sb.Append("  BroadcasterName: ").Append(BroadcasterName).Append("\n");
            sb.Append("  CharityName: ").Append(CharityName).Append("\n");
            sb.Append("  CharityDescription: ").Append(CharityDescription).Append("\n");
            sb.Append("  CharityLogo: ").Append(CharityLogo).Append("\n");
            sb.Append("  CharityWebsite: ").Append(CharityWebsite).Append("\n");
            sb.Append("  CurrentAmount: ").Append(CurrentAmount).Append("\n");
            sb.Append("  TargetAmount: ").Append(TargetAmount).Append("\n");
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
    /// A Json converter for type <see cref="CharityCampaign" />
    /// </summary>
    public class CharityCampaignJsonConverter : JsonConverter<CharityCampaign>
    {
        /// <summary>
        /// Deserializes json to <see cref="CharityCampaign" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override CharityCampaign Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> id = default;
            Option<string?> broadcasterId = default;
            Option<string?> broadcasterLogin = default;
            Option<string?> broadcasterName = default;
            Option<string?> charityName = default;
            Option<string?> charityDescription = default;
            Option<string?> charityLogo = default;
            Option<string?> charityWebsite = default;
            Option<CharityCampaignCurrentAmount?> currentAmount = default;
            Option<CharityCampaignTargetAmount?> targetAmount = default;

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
                        case "broadcaster_id":
                            broadcasterId = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "broadcaster_login":
                            broadcasterLogin = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "broadcaster_name":
                            broadcasterName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "charity_name":
                            charityName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "charity_description":
                            charityDescription = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "charity_logo":
                            charityLogo = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "charity_website":
                            charityWebsite = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "current_amount":
                            currentAmount = new Option<CharityCampaignCurrentAmount?>(JsonSerializer.Deserialize<CharityCampaignCurrentAmount>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        case "target_amount":
                            targetAmount = new Option<CharityCampaignTargetAmount?>(JsonSerializer.Deserialize<CharityCampaignTargetAmount>(ref utf8JsonReader, jsonSerializerOptions)!);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!id.IsSet)
                throw new ArgumentException("Property is required for class CharityCampaign.", nameof(id));

            if (!broadcasterId.IsSet)
                throw new ArgumentException("Property is required for class CharityCampaign.", nameof(broadcasterId));

            if (!broadcasterLogin.IsSet)
                throw new ArgumentException("Property is required for class CharityCampaign.", nameof(broadcasterLogin));

            if (!broadcasterName.IsSet)
                throw new ArgumentException("Property is required for class CharityCampaign.", nameof(broadcasterName));

            if (!charityName.IsSet)
                throw new ArgumentException("Property is required for class CharityCampaign.", nameof(charityName));

            if (!charityDescription.IsSet)
                throw new ArgumentException("Property is required for class CharityCampaign.", nameof(charityDescription));

            if (!charityLogo.IsSet)
                throw new ArgumentException("Property is required for class CharityCampaign.", nameof(charityLogo));

            if (!charityWebsite.IsSet)
                throw new ArgumentException("Property is required for class CharityCampaign.", nameof(charityWebsite));

            if (!currentAmount.IsSet)
                throw new ArgumentException("Property is required for class CharityCampaign.", nameof(currentAmount));

            if (!targetAmount.IsSet)
                throw new ArgumentException("Property is required for class CharityCampaign.", nameof(targetAmount));

            if (id.IsSet && id.Value == null)
                throw new ArgumentNullException(nameof(id), "Property is not nullable for class CharityCampaign.");

            if (broadcasterId.IsSet && broadcasterId.Value == null)
                throw new ArgumentNullException(nameof(broadcasterId), "Property is not nullable for class CharityCampaign.");

            if (broadcasterLogin.IsSet && broadcasterLogin.Value == null)
                throw new ArgumentNullException(nameof(broadcasterLogin), "Property is not nullable for class CharityCampaign.");

            if (broadcasterName.IsSet && broadcasterName.Value == null)
                throw new ArgumentNullException(nameof(broadcasterName), "Property is not nullable for class CharityCampaign.");

            if (charityName.IsSet && charityName.Value == null)
                throw new ArgumentNullException(nameof(charityName), "Property is not nullable for class CharityCampaign.");

            if (charityDescription.IsSet && charityDescription.Value == null)
                throw new ArgumentNullException(nameof(charityDescription), "Property is not nullable for class CharityCampaign.");

            if (charityLogo.IsSet && charityLogo.Value == null)
                throw new ArgumentNullException(nameof(charityLogo), "Property is not nullable for class CharityCampaign.");

            if (charityWebsite.IsSet && charityWebsite.Value == null)
                throw new ArgumentNullException(nameof(charityWebsite), "Property is not nullable for class CharityCampaign.");

            if (currentAmount.IsSet && currentAmount.Value == null)
                throw new ArgumentNullException(nameof(currentAmount), "Property is not nullable for class CharityCampaign.");

            if (targetAmount.IsSet && targetAmount.Value == null)
                throw new ArgumentNullException(nameof(targetAmount), "Property is not nullable for class CharityCampaign.");

            return new CharityCampaign(id.Value!, broadcasterId.Value!, broadcasterLogin.Value!, broadcasterName.Value!, charityName.Value!, charityDescription.Value!, charityLogo.Value!, charityWebsite.Value!, currentAmount.Value!, targetAmount.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="CharityCampaign" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="charityCampaign"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, CharityCampaign charityCampaign, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, charityCampaign, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="CharityCampaign" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="charityCampaign"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, CharityCampaign charityCampaign, JsonSerializerOptions jsonSerializerOptions)
        {
            if (charityCampaign.Id == null)
                throw new ArgumentNullException(nameof(charityCampaign.Id), "Property is required for class CharityCampaign.");

            if (charityCampaign.BroadcasterId == null)
                throw new ArgumentNullException(nameof(charityCampaign.BroadcasterId), "Property is required for class CharityCampaign.");

            if (charityCampaign.BroadcasterLogin == null)
                throw new ArgumentNullException(nameof(charityCampaign.BroadcasterLogin), "Property is required for class CharityCampaign.");

            if (charityCampaign.BroadcasterName == null)
                throw new ArgumentNullException(nameof(charityCampaign.BroadcasterName), "Property is required for class CharityCampaign.");

            if (charityCampaign.CharityName == null)
                throw new ArgumentNullException(nameof(charityCampaign.CharityName), "Property is required for class CharityCampaign.");

            if (charityCampaign.CharityDescription == null)
                throw new ArgumentNullException(nameof(charityCampaign.CharityDescription), "Property is required for class CharityCampaign.");

            if (charityCampaign.CharityLogo == null)
                throw new ArgumentNullException(nameof(charityCampaign.CharityLogo), "Property is required for class CharityCampaign.");

            if (charityCampaign.CharityWebsite == null)
                throw new ArgumentNullException(nameof(charityCampaign.CharityWebsite), "Property is required for class CharityCampaign.");

            if (charityCampaign.CurrentAmount == null)
                throw new ArgumentNullException(nameof(charityCampaign.CurrentAmount), "Property is required for class CharityCampaign.");

            if (charityCampaign.TargetAmount == null)
                throw new ArgumentNullException(nameof(charityCampaign.TargetAmount), "Property is required for class CharityCampaign.");

            writer.WriteString("id", charityCampaign.Id);

            writer.WriteString("broadcaster_id", charityCampaign.BroadcasterId);

            writer.WriteString("broadcaster_login", charityCampaign.BroadcasterLogin);

            writer.WriteString("broadcaster_name", charityCampaign.BroadcasterName);

            writer.WriteString("charity_name", charityCampaign.CharityName);

            writer.WriteString("charity_description", charityCampaign.CharityDescription);

            writer.WriteString("charity_logo", charityCampaign.CharityLogo);

            writer.WriteString("charity_website", charityCampaign.CharityWebsite);

            writer.WritePropertyName("current_amount");
            JsonSerializer.Serialize(writer, charityCampaign.CurrentAmount, jsonSerializerOptions);
            writer.WritePropertyName("target_amount");
            JsonSerializer.Serialize(writer, charityCampaign.TargetAmount, jsonSerializerOptions);
        }
    }
}
