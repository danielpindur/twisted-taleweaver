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
    /// AutoModSettings
    /// </summary>
    public partial class AutoModSettings : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoModSettings" /> class.
        /// </summary>
        /// <param name="broadcasterId">The broadcaster’s ID.</param>
        /// <param name="moderatorId">The moderator’s ID.</param>
        /// <param name="disability">The Automod level for discrimination against disability.</param>
        /// <param name="aggression">The Automod level for hostility involving aggression.</param>
        /// <param name="sexualitySexOrGender">The AutoMod level for discrimination based on sexuality, sex, or gender.</param>
        /// <param name="misogyny">The Automod level for discrimination against women.</param>
        /// <param name="bullying">The Automod level for hostility involving name calling or insults.</param>
        /// <param name="swearing">The Automod level for profanity.</param>
        /// <param name="raceEthnicityOrReligion">The Automod level for racial discrimination.</param>
        /// <param name="sexBasedTerms">The Automod level for sexual content.</param>
        /// <param name="overallLevel">The default AutoMod level for the broadcaster. This field is **null** if the broadcaster has set one or more of the individual settings.</param>
        [JsonConstructor]
        public AutoModSettings(string broadcasterId, string moderatorId, int disability, int aggression, int sexualitySexOrGender, int misogyny, int bullying, int swearing, int raceEthnicityOrReligion, int sexBasedTerms, int? overallLevel = default)
        {
            BroadcasterId = broadcasterId;
            ModeratorId = moderatorId;
            Disability = disability;
            Aggression = aggression;
            SexualitySexOrGender = sexualitySexOrGender;
            Misogyny = misogyny;
            Bullying = bullying;
            Swearing = swearing;
            RaceEthnicityOrReligion = raceEthnicityOrReligion;
            SexBasedTerms = sexBasedTerms;
            OverallLevel = overallLevel;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The broadcaster’s ID.
        /// </summary>
        /// <value>The broadcaster’s ID.</value>
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; set; }

        /// <summary>
        /// The moderator’s ID.
        /// </summary>
        /// <value>The moderator’s ID.</value>
        [JsonPropertyName("moderator_id")]
        public string ModeratorId { get; set; }

        /// <summary>
        /// The Automod level for discrimination against disability.
        /// </summary>
        /// <value>The Automod level for discrimination against disability.</value>
        [JsonPropertyName("disability")]
        public int Disability { get; set; }

        /// <summary>
        /// The Automod level for hostility involving aggression.
        /// </summary>
        /// <value>The Automod level for hostility involving aggression.</value>
        [JsonPropertyName("aggression")]
        public int Aggression { get; set; }

        /// <summary>
        /// The AutoMod level for discrimination based on sexuality, sex, or gender.
        /// </summary>
        /// <value>The AutoMod level for discrimination based on sexuality, sex, or gender.</value>
        [JsonPropertyName("sexuality_sex_or_gender")]
        public int SexualitySexOrGender { get; set; }

        /// <summary>
        /// The Automod level for discrimination against women.
        /// </summary>
        /// <value>The Automod level for discrimination against women.</value>
        [JsonPropertyName("misogyny")]
        public int Misogyny { get; set; }

        /// <summary>
        /// The Automod level for hostility involving name calling or insults.
        /// </summary>
        /// <value>The Automod level for hostility involving name calling or insults.</value>
        [JsonPropertyName("bullying")]
        public int Bullying { get; set; }

        /// <summary>
        /// The Automod level for profanity.
        /// </summary>
        /// <value>The Automod level for profanity.</value>
        [JsonPropertyName("swearing")]
        public int Swearing { get; set; }

        /// <summary>
        /// The Automod level for racial discrimination.
        /// </summary>
        /// <value>The Automod level for racial discrimination.</value>
        [JsonPropertyName("race_ethnicity_or_religion")]
        public int RaceEthnicityOrReligion { get; set; }

        /// <summary>
        /// The Automod level for sexual content.
        /// </summary>
        /// <value>The Automod level for sexual content.</value>
        [JsonPropertyName("sex_based_terms")]
        public int SexBasedTerms { get; set; }

        /// <summary>
        /// The default AutoMod level for the broadcaster. This field is **null** if the broadcaster has set one or more of the individual settings.
        /// </summary>
        /// <value>The default AutoMod level for the broadcaster. This field is **null** if the broadcaster has set one or more of the individual settings.</value>
        [JsonPropertyName("overall_level")]
        public int? OverallLevel { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class AutoModSettings {\n");
            sb.Append("  BroadcasterId: ").Append(BroadcasterId).Append("\n");
            sb.Append("  ModeratorId: ").Append(ModeratorId).Append("\n");
            sb.Append("  Disability: ").Append(Disability).Append("\n");
            sb.Append("  Aggression: ").Append(Aggression).Append("\n");
            sb.Append("  SexualitySexOrGender: ").Append(SexualitySexOrGender).Append("\n");
            sb.Append("  Misogyny: ").Append(Misogyny).Append("\n");
            sb.Append("  Bullying: ").Append(Bullying).Append("\n");
            sb.Append("  Swearing: ").Append(Swearing).Append("\n");
            sb.Append("  RaceEthnicityOrReligion: ").Append(RaceEthnicityOrReligion).Append("\n");
            sb.Append("  SexBasedTerms: ").Append(SexBasedTerms).Append("\n");
            sb.Append("  OverallLevel: ").Append(OverallLevel).Append("\n");
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
    /// A Json converter for type <see cref="AutoModSettings" />
    /// </summary>
    public class AutoModSettingsJsonConverter : JsonConverter<AutoModSettings>
    {
        /// <summary>
        /// Deserializes json to <see cref="AutoModSettings" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override AutoModSettings Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> broadcasterId = default;
            Option<string?> moderatorId = default;
            Option<int?> disability = default;
            Option<int?> aggression = default;
            Option<int?> sexualitySexOrGender = default;
            Option<int?> misogyny = default;
            Option<int?> bullying = default;
            Option<int?> swearing = default;
            Option<int?> raceEthnicityOrReligion = default;
            Option<int?> sexBasedTerms = default;
            Option<int?> overallLevel = default;

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
                        case "broadcaster_id":
                            broadcasterId = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "moderator_id":
                            moderatorId = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "disability":
                            disability = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "aggression":
                            aggression = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "sexuality_sex_or_gender":
                            sexualitySexOrGender = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "misogyny":
                            misogyny = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "bullying":
                            bullying = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "swearing":
                            swearing = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "race_ethnicity_or_religion":
                            raceEthnicityOrReligion = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "sex_based_terms":
                            sexBasedTerms = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "overall_level":
                            overallLevel = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!broadcasterId.IsSet)
                throw new ArgumentException("Property is required for class AutoModSettings.", nameof(broadcasterId));

            if (!moderatorId.IsSet)
                throw new ArgumentException("Property is required for class AutoModSettings.", nameof(moderatorId));

            if (!disability.IsSet)
                throw new ArgumentException("Property is required for class AutoModSettings.", nameof(disability));

            if (!aggression.IsSet)
                throw new ArgumentException("Property is required for class AutoModSettings.", nameof(aggression));

            if (!sexualitySexOrGender.IsSet)
                throw new ArgumentException("Property is required for class AutoModSettings.", nameof(sexualitySexOrGender));

            if (!misogyny.IsSet)
                throw new ArgumentException("Property is required for class AutoModSettings.", nameof(misogyny));

            if (!bullying.IsSet)
                throw new ArgumentException("Property is required for class AutoModSettings.", nameof(bullying));

            if (!swearing.IsSet)
                throw new ArgumentException("Property is required for class AutoModSettings.", nameof(swearing));

            if (!raceEthnicityOrReligion.IsSet)
                throw new ArgumentException("Property is required for class AutoModSettings.", nameof(raceEthnicityOrReligion));

            if (!sexBasedTerms.IsSet)
                throw new ArgumentException("Property is required for class AutoModSettings.", nameof(sexBasedTerms));

            if (!overallLevel.IsSet)
                throw new ArgumentException("Property is required for class AutoModSettings.", nameof(overallLevel));

            if (broadcasterId.IsSet && broadcasterId.Value == null)
                throw new ArgumentNullException(nameof(broadcasterId), "Property is not nullable for class AutoModSettings.");

            if (moderatorId.IsSet && moderatorId.Value == null)
                throw new ArgumentNullException(nameof(moderatorId), "Property is not nullable for class AutoModSettings.");

            if (disability.IsSet && disability.Value == null)
                throw new ArgumentNullException(nameof(disability), "Property is not nullable for class AutoModSettings.");

            if (aggression.IsSet && aggression.Value == null)
                throw new ArgumentNullException(nameof(aggression), "Property is not nullable for class AutoModSettings.");

            if (sexualitySexOrGender.IsSet && sexualitySexOrGender.Value == null)
                throw new ArgumentNullException(nameof(sexualitySexOrGender), "Property is not nullable for class AutoModSettings.");

            if (misogyny.IsSet && misogyny.Value == null)
                throw new ArgumentNullException(nameof(misogyny), "Property is not nullable for class AutoModSettings.");

            if (bullying.IsSet && bullying.Value == null)
                throw new ArgumentNullException(nameof(bullying), "Property is not nullable for class AutoModSettings.");

            if (swearing.IsSet && swearing.Value == null)
                throw new ArgumentNullException(nameof(swearing), "Property is not nullable for class AutoModSettings.");

            if (raceEthnicityOrReligion.IsSet && raceEthnicityOrReligion.Value == null)
                throw new ArgumentNullException(nameof(raceEthnicityOrReligion), "Property is not nullable for class AutoModSettings.");

            if (sexBasedTerms.IsSet && sexBasedTerms.Value == null)
                throw new ArgumentNullException(nameof(sexBasedTerms), "Property is not nullable for class AutoModSettings.");

            return new AutoModSettings(broadcasterId.Value!, moderatorId.Value!, disability.Value!.Value!, aggression.Value!.Value!, sexualitySexOrGender.Value!.Value!, misogyny.Value!.Value!, bullying.Value!.Value!, swearing.Value!.Value!, raceEthnicityOrReligion.Value!.Value!, sexBasedTerms.Value!.Value!, overallLevel.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="AutoModSettings" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="autoModSettings"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, AutoModSettings autoModSettings, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, autoModSettings, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="AutoModSettings" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="autoModSettings"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, AutoModSettings autoModSettings, JsonSerializerOptions jsonSerializerOptions)
        {
            if (autoModSettings.BroadcasterId == null)
                throw new ArgumentNullException(nameof(autoModSettings.BroadcasterId), "Property is required for class AutoModSettings.");

            if (autoModSettings.ModeratorId == null)
                throw new ArgumentNullException(nameof(autoModSettings.ModeratorId), "Property is required for class AutoModSettings.");

            writer.WriteString("broadcaster_id", autoModSettings.BroadcasterId);

            writer.WriteString("moderator_id", autoModSettings.ModeratorId);

            writer.WriteNumber("disability", autoModSettings.Disability);

            writer.WriteNumber("aggression", autoModSettings.Aggression);

            writer.WriteNumber("sexuality_sex_or_gender", autoModSettings.SexualitySexOrGender);

            writer.WriteNumber("misogyny", autoModSettings.Misogyny);

            writer.WriteNumber("bullying", autoModSettings.Bullying);

            writer.WriteNumber("swearing", autoModSettings.Swearing);

            writer.WriteNumber("race_ethnicity_or_religion", autoModSettings.RaceEthnicityOrReligion);

            writer.WriteNumber("sex_based_terms", autoModSettings.SexBasedTerms);

            if (autoModSettings.OverallLevel != null)
                writer.WriteNumber("overall_level", autoModSettings.OverallLevel.Value);
            else
                writer.WriteNull("overall_level");
        }
    }
}
