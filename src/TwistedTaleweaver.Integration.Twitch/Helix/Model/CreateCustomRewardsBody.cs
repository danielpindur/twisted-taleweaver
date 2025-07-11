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
    /// CreateCustomRewardsBody
    /// </summary>
    public partial class CreateCustomRewardsBody : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCustomRewardsBody" /> class.
        /// </summary>
        /// <param name="title">The custom reward’s title. The title may contain a maximum of 45 characters and it must be unique amongst all of the broadcaster’s custom rewards.</param>
        /// <param name="cost">The cost of the reward, in Channel Points. The minimum is 1 point.</param>
        /// <param name="prompt">The prompt shown to the viewer when they redeem the reward. Specify a prompt if &#x60;is_user_input_required&#x60; is **true**. The prompt is limited to a maximum of 200 characters.</param>
        /// <param name="isEnabled">A Boolean value that determines whether the reward is enabled. Viewers see only enabled rewards. The default is **true**.</param>
        /// <param name="backgroundColor">The background color to use for the reward. Specify the color using Hex format (for example, #9147FF).</param>
        /// <param name="isUserInputRequired">A Boolean value that determines whether the user needs to enter information when redeeming the reward. See the &#x60;prompt&#x60; field. The default is **false**.</param>
        /// <param name="isMaxPerStreamEnabled">A Boolean value that determines whether to limit the maximum number of redemptions allowed per live stream (see the &#x60;max_per_stream&#x60; field). The default is **false**.</param>
        /// <param name="maxPerStream">The maximum number of redemptions allowed per live stream. Applied only if &#x60;is_max_per_stream_enabled&#x60; is **true**. The minimum value is 1.</param>
        /// <param name="isMaxPerUserPerStreamEnabled">A Boolean value that determines whether to limit the maximum number of redemptions allowed per user per stream (see the &#x60;max_per_user_per_stream&#x60; field). The default is **false**.</param>
        /// <param name="maxPerUserPerStream">The maximum number of redemptions allowed per user per stream. Applied only if &#x60;is_max_per_user_per_stream_enabled&#x60; is **true**. The minimum value is 1.</param>
        /// <param name="isGlobalCooldownEnabled">A Boolean value that determines whether to apply a cooldown period between redemptions (see the &#x60;global_cooldown_seconds&#x60; field for the duration of the cooldown period). The default is **false**.</param>
        /// <param name="globalCooldownSeconds">The cooldown period, in seconds. Applied only if the &#x60;is_global_cooldown_enabled&#x60; field is **true**. The minimum value is 1; however, the minimum value is 60 for it to be shown in the Twitch UX.</param>
        /// <param name="shouldRedemptionsSkipRequestQueue">A Boolean value that determines whether redemptions should be set to FULFILLED status immediately when a reward is redeemed. If **false**, status is set to UNFULFILLED and follows the normal request queue process. The default is **false**.</param>
        [JsonConstructor]
        public CreateCustomRewardsBody(string title, long cost, Option<string?> prompt = default, Option<bool?> isEnabled = default, Option<string?> backgroundColor = default, Option<bool?> isUserInputRequired = default, Option<bool?> isMaxPerStreamEnabled = default, Option<int?> maxPerStream = default, Option<bool?> isMaxPerUserPerStreamEnabled = default, Option<int?> maxPerUserPerStream = default, Option<bool?> isGlobalCooldownEnabled = default, Option<int?> globalCooldownSeconds = default, Option<bool?> shouldRedemptionsSkipRequestQueue = default)
        {
            Title = title;
            Cost = cost;
            PromptOption = prompt;
            IsEnabledOption = isEnabled;
            BackgroundColorOption = backgroundColor;
            IsUserInputRequiredOption = isUserInputRequired;
            IsMaxPerStreamEnabledOption = isMaxPerStreamEnabled;
            MaxPerStreamOption = maxPerStream;
            IsMaxPerUserPerStreamEnabledOption = isMaxPerUserPerStreamEnabled;
            MaxPerUserPerStreamOption = maxPerUserPerStream;
            IsGlobalCooldownEnabledOption = isGlobalCooldownEnabled;
            GlobalCooldownSecondsOption = globalCooldownSeconds;
            ShouldRedemptionsSkipRequestQueueOption = shouldRedemptionsSkipRequestQueue;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The custom reward’s title. The title may contain a maximum of 45 characters and it must be unique amongst all of the broadcaster’s custom rewards.
        /// </summary>
        /// <value>The custom reward’s title. The title may contain a maximum of 45 characters and it must be unique amongst all of the broadcaster’s custom rewards.</value>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// The cost of the reward, in Channel Points. The minimum is 1 point.
        /// </summary>
        /// <value>The cost of the reward, in Channel Points. The minimum is 1 point.</value>
        [JsonPropertyName("cost")]
        public long Cost { get; set; }

        /// <summary>
        /// Used to track the state of Prompt
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<string?> PromptOption { get; private set; }

        /// <summary>
        /// The prompt shown to the viewer when they redeem the reward. Specify a prompt if &#x60;is_user_input_required&#x60; is **true**. The prompt is limited to a maximum of 200 characters.
        /// </summary>
        /// <value>The prompt shown to the viewer when they redeem the reward. Specify a prompt if &#x60;is_user_input_required&#x60; is **true**. The prompt is limited to a maximum of 200 characters.</value>
        [JsonPropertyName("prompt")]
        public string? Prompt { get { return this.PromptOption; } set { this.PromptOption = new(value); } }

        /// <summary>
        /// Used to track the state of IsEnabled
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<bool?> IsEnabledOption { get; private set; }

        /// <summary>
        /// A Boolean value that determines whether the reward is enabled. Viewers see only enabled rewards. The default is **true**.
        /// </summary>
        /// <value>A Boolean value that determines whether the reward is enabled. Viewers see only enabled rewards. The default is **true**.</value>
        [JsonPropertyName("is_enabled")]
        public bool? IsEnabled { get { return this.IsEnabledOption; } set { this.IsEnabledOption = new(value); } }

        /// <summary>
        /// Used to track the state of BackgroundColor
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<string?> BackgroundColorOption { get; private set; }

        /// <summary>
        /// The background color to use for the reward. Specify the color using Hex format (for example, #9147FF).
        /// </summary>
        /// <value>The background color to use for the reward. Specify the color using Hex format (for example, #9147FF).</value>
        [JsonPropertyName("background_color")]
        public string? BackgroundColor { get { return this.BackgroundColorOption; } set { this.BackgroundColorOption = new(value); } }

        /// <summary>
        /// Used to track the state of IsUserInputRequired
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<bool?> IsUserInputRequiredOption { get; private set; }

        /// <summary>
        /// A Boolean value that determines whether the user needs to enter information when redeeming the reward. See the &#x60;prompt&#x60; field. The default is **false**.
        /// </summary>
        /// <value>A Boolean value that determines whether the user needs to enter information when redeeming the reward. See the &#x60;prompt&#x60; field. The default is **false**.</value>
        [JsonPropertyName("is_user_input_required")]
        public bool? IsUserInputRequired { get { return this.IsUserInputRequiredOption; } set { this.IsUserInputRequiredOption = new(value); } }

        /// <summary>
        /// Used to track the state of IsMaxPerStreamEnabled
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<bool?> IsMaxPerStreamEnabledOption { get; private set; }

        /// <summary>
        /// A Boolean value that determines whether to limit the maximum number of redemptions allowed per live stream (see the &#x60;max_per_stream&#x60; field). The default is **false**.
        /// </summary>
        /// <value>A Boolean value that determines whether to limit the maximum number of redemptions allowed per live stream (see the &#x60;max_per_stream&#x60; field). The default is **false**.</value>
        [JsonPropertyName("is_max_per_stream_enabled")]
        public bool? IsMaxPerStreamEnabled { get { return this.IsMaxPerStreamEnabledOption; } set { this.IsMaxPerStreamEnabledOption = new(value); } }

        /// <summary>
        /// Used to track the state of MaxPerStream
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<int?> MaxPerStreamOption { get; private set; }

        /// <summary>
        /// The maximum number of redemptions allowed per live stream. Applied only if &#x60;is_max_per_stream_enabled&#x60; is **true**. The minimum value is 1.
        /// </summary>
        /// <value>The maximum number of redemptions allowed per live stream. Applied only if &#x60;is_max_per_stream_enabled&#x60; is **true**. The minimum value is 1.</value>
        [JsonPropertyName("max_per_stream")]
        public int? MaxPerStream { get { return this.MaxPerStreamOption; } set { this.MaxPerStreamOption = new(value); } }

        /// <summary>
        /// Used to track the state of IsMaxPerUserPerStreamEnabled
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<bool?> IsMaxPerUserPerStreamEnabledOption { get; private set; }

        /// <summary>
        /// A Boolean value that determines whether to limit the maximum number of redemptions allowed per user per stream (see the &#x60;max_per_user_per_stream&#x60; field). The default is **false**.
        /// </summary>
        /// <value>A Boolean value that determines whether to limit the maximum number of redemptions allowed per user per stream (see the &#x60;max_per_user_per_stream&#x60; field). The default is **false**.</value>
        [JsonPropertyName("is_max_per_user_per_stream_enabled")]
        public bool? IsMaxPerUserPerStreamEnabled { get { return this.IsMaxPerUserPerStreamEnabledOption; } set { this.IsMaxPerUserPerStreamEnabledOption = new(value); } }

        /// <summary>
        /// Used to track the state of MaxPerUserPerStream
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<int?> MaxPerUserPerStreamOption { get; private set; }

        /// <summary>
        /// The maximum number of redemptions allowed per user per stream. Applied only if &#x60;is_max_per_user_per_stream_enabled&#x60; is **true**. The minimum value is 1.
        /// </summary>
        /// <value>The maximum number of redemptions allowed per user per stream. Applied only if &#x60;is_max_per_user_per_stream_enabled&#x60; is **true**. The minimum value is 1.</value>
        [JsonPropertyName("max_per_user_per_stream")]
        public int? MaxPerUserPerStream { get { return this.MaxPerUserPerStreamOption; } set { this.MaxPerUserPerStreamOption = new(value); } }

        /// <summary>
        /// Used to track the state of IsGlobalCooldownEnabled
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<bool?> IsGlobalCooldownEnabledOption { get; private set; }

        /// <summary>
        /// A Boolean value that determines whether to apply a cooldown period between redemptions (see the &#x60;global_cooldown_seconds&#x60; field for the duration of the cooldown period). The default is **false**.
        /// </summary>
        /// <value>A Boolean value that determines whether to apply a cooldown period between redemptions (see the &#x60;global_cooldown_seconds&#x60; field for the duration of the cooldown period). The default is **false**.</value>
        [JsonPropertyName("is_global_cooldown_enabled")]
        public bool? IsGlobalCooldownEnabled { get { return this.IsGlobalCooldownEnabledOption; } set { this.IsGlobalCooldownEnabledOption = new(value); } }

        /// <summary>
        /// Used to track the state of GlobalCooldownSeconds
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<int?> GlobalCooldownSecondsOption { get; private set; }

        /// <summary>
        /// The cooldown period, in seconds. Applied only if the &#x60;is_global_cooldown_enabled&#x60; field is **true**. The minimum value is 1; however, the minimum value is 60 for it to be shown in the Twitch UX.
        /// </summary>
        /// <value>The cooldown period, in seconds. Applied only if the &#x60;is_global_cooldown_enabled&#x60; field is **true**. The minimum value is 1; however, the minimum value is 60 for it to be shown in the Twitch UX.</value>
        [JsonPropertyName("global_cooldown_seconds")]
        public int? GlobalCooldownSeconds { get { return this.GlobalCooldownSecondsOption; } set { this.GlobalCooldownSecondsOption = new(value); } }

        /// <summary>
        /// Used to track the state of ShouldRedemptionsSkipRequestQueue
        /// </summary>
        [JsonIgnore]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Option<bool?> ShouldRedemptionsSkipRequestQueueOption { get; private set; }

        /// <summary>
        /// A Boolean value that determines whether redemptions should be set to FULFILLED status immediately when a reward is redeemed. If **false**, status is set to UNFULFILLED and follows the normal request queue process. The default is **false**.
        /// </summary>
        /// <value>A Boolean value that determines whether redemptions should be set to FULFILLED status immediately when a reward is redeemed. If **false**, status is set to UNFULFILLED and follows the normal request queue process. The default is **false**.</value>
        [JsonPropertyName("should_redemptions_skip_request_queue")]
        public bool? ShouldRedemptionsSkipRequestQueue { get { return this.ShouldRedemptionsSkipRequestQueueOption; } set { this.ShouldRedemptionsSkipRequestQueueOption = new(value); } }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CreateCustomRewardsBody {\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  Cost: ").Append(Cost).Append("\n");
            sb.Append("  Prompt: ").Append(Prompt).Append("\n");
            sb.Append("  IsEnabled: ").Append(IsEnabled).Append("\n");
            sb.Append("  BackgroundColor: ").Append(BackgroundColor).Append("\n");
            sb.Append("  IsUserInputRequired: ").Append(IsUserInputRequired).Append("\n");
            sb.Append("  IsMaxPerStreamEnabled: ").Append(IsMaxPerStreamEnabled).Append("\n");
            sb.Append("  MaxPerStream: ").Append(MaxPerStream).Append("\n");
            sb.Append("  IsMaxPerUserPerStreamEnabled: ").Append(IsMaxPerUserPerStreamEnabled).Append("\n");
            sb.Append("  MaxPerUserPerStream: ").Append(MaxPerUserPerStream).Append("\n");
            sb.Append("  IsGlobalCooldownEnabled: ").Append(IsGlobalCooldownEnabled).Append("\n");
            sb.Append("  GlobalCooldownSeconds: ").Append(GlobalCooldownSeconds).Append("\n");
            sb.Append("  ShouldRedemptionsSkipRequestQueue: ").Append(ShouldRedemptionsSkipRequestQueue).Append("\n");
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
    /// A Json converter for type <see cref="CreateCustomRewardsBody" />
    /// </summary>
    public class CreateCustomRewardsBodyJsonConverter : JsonConverter<CreateCustomRewardsBody>
    {
        /// <summary>
        /// Deserializes json to <see cref="CreateCustomRewardsBody" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override CreateCustomRewardsBody Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> title = default;
            Option<long?> cost = default;
            Option<string?> prompt = default;
            Option<bool?> isEnabled = default;
            Option<string?> backgroundColor = default;
            Option<bool?> isUserInputRequired = default;
            Option<bool?> isMaxPerStreamEnabled = default;
            Option<int?> maxPerStream = default;
            Option<bool?> isMaxPerUserPerStreamEnabled = default;
            Option<int?> maxPerUserPerStream = default;
            Option<bool?> isGlobalCooldownEnabled = default;
            Option<int?> globalCooldownSeconds = default;
            Option<bool?> shouldRedemptionsSkipRequestQueue = default;

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
                        case "title":
                            title = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "cost":
                            cost = new Option<long?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (long?)null : utf8JsonReader.GetInt64());
                            break;
                        case "prompt":
                            prompt = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "is_enabled":
                            isEnabled = new Option<bool?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (bool?)null : utf8JsonReader.GetBoolean());
                            break;
                        case "background_color":
                            backgroundColor = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "is_user_input_required":
                            isUserInputRequired = new Option<bool?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (bool?)null : utf8JsonReader.GetBoolean());
                            break;
                        case "is_max_per_stream_enabled":
                            isMaxPerStreamEnabled = new Option<bool?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (bool?)null : utf8JsonReader.GetBoolean());
                            break;
                        case "max_per_stream":
                            maxPerStream = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "is_max_per_user_per_stream_enabled":
                            isMaxPerUserPerStreamEnabled = new Option<bool?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (bool?)null : utf8JsonReader.GetBoolean());
                            break;
                        case "max_per_user_per_stream":
                            maxPerUserPerStream = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "is_global_cooldown_enabled":
                            isGlobalCooldownEnabled = new Option<bool?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (bool?)null : utf8JsonReader.GetBoolean());
                            break;
                        case "global_cooldown_seconds":
                            globalCooldownSeconds = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "should_redemptions_skip_request_queue":
                            shouldRedemptionsSkipRequestQueue = new Option<bool?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (bool?)null : utf8JsonReader.GetBoolean());
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!title.IsSet)
                throw new ArgumentException("Property is required for class CreateCustomRewardsBody.", nameof(title));

            if (!cost.IsSet)
                throw new ArgumentException("Property is required for class CreateCustomRewardsBody.", nameof(cost));

            if (title.IsSet && title.Value == null)
                throw new ArgumentNullException(nameof(title), "Property is not nullable for class CreateCustomRewardsBody.");

            if (cost.IsSet && cost.Value == null)
                throw new ArgumentNullException(nameof(cost), "Property is not nullable for class CreateCustomRewardsBody.");

            if (prompt.IsSet && prompt.Value == null)
                throw new ArgumentNullException(nameof(prompt), "Property is not nullable for class CreateCustomRewardsBody.");

            if (isEnabled.IsSet && isEnabled.Value == null)
                throw new ArgumentNullException(nameof(isEnabled), "Property is not nullable for class CreateCustomRewardsBody.");

            if (backgroundColor.IsSet && backgroundColor.Value == null)
                throw new ArgumentNullException(nameof(backgroundColor), "Property is not nullable for class CreateCustomRewardsBody.");

            if (isUserInputRequired.IsSet && isUserInputRequired.Value == null)
                throw new ArgumentNullException(nameof(isUserInputRequired), "Property is not nullable for class CreateCustomRewardsBody.");

            if (isMaxPerStreamEnabled.IsSet && isMaxPerStreamEnabled.Value == null)
                throw new ArgumentNullException(nameof(isMaxPerStreamEnabled), "Property is not nullable for class CreateCustomRewardsBody.");

            if (maxPerStream.IsSet && maxPerStream.Value == null)
                throw new ArgumentNullException(nameof(maxPerStream), "Property is not nullable for class CreateCustomRewardsBody.");

            if (isMaxPerUserPerStreamEnabled.IsSet && isMaxPerUserPerStreamEnabled.Value == null)
                throw new ArgumentNullException(nameof(isMaxPerUserPerStreamEnabled), "Property is not nullable for class CreateCustomRewardsBody.");

            if (maxPerUserPerStream.IsSet && maxPerUserPerStream.Value == null)
                throw new ArgumentNullException(nameof(maxPerUserPerStream), "Property is not nullable for class CreateCustomRewardsBody.");

            if (isGlobalCooldownEnabled.IsSet && isGlobalCooldownEnabled.Value == null)
                throw new ArgumentNullException(nameof(isGlobalCooldownEnabled), "Property is not nullable for class CreateCustomRewardsBody.");

            if (globalCooldownSeconds.IsSet && globalCooldownSeconds.Value == null)
                throw new ArgumentNullException(nameof(globalCooldownSeconds), "Property is not nullable for class CreateCustomRewardsBody.");

            if (shouldRedemptionsSkipRequestQueue.IsSet && shouldRedemptionsSkipRequestQueue.Value == null)
                throw new ArgumentNullException(nameof(shouldRedemptionsSkipRequestQueue), "Property is not nullable for class CreateCustomRewardsBody.");

            return new CreateCustomRewardsBody(title.Value!, cost.Value!.Value!, prompt, isEnabled, backgroundColor, isUserInputRequired, isMaxPerStreamEnabled, maxPerStream, isMaxPerUserPerStreamEnabled, maxPerUserPerStream, isGlobalCooldownEnabled, globalCooldownSeconds, shouldRedemptionsSkipRequestQueue);
        }

        /// <summary>
        /// Serializes a <see cref="CreateCustomRewardsBody" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="createCustomRewardsBody"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, CreateCustomRewardsBody createCustomRewardsBody, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, createCustomRewardsBody, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="CreateCustomRewardsBody" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="createCustomRewardsBody"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, CreateCustomRewardsBody createCustomRewardsBody, JsonSerializerOptions jsonSerializerOptions)
        {
            if (createCustomRewardsBody.Title == null)
                throw new ArgumentNullException(nameof(createCustomRewardsBody.Title), "Property is required for class CreateCustomRewardsBody.");

            if (createCustomRewardsBody.PromptOption.IsSet && createCustomRewardsBody.Prompt == null)
                throw new ArgumentNullException(nameof(createCustomRewardsBody.Prompt), "Property is required for class CreateCustomRewardsBody.");

            if (createCustomRewardsBody.BackgroundColorOption.IsSet && createCustomRewardsBody.BackgroundColor == null)
                throw new ArgumentNullException(nameof(createCustomRewardsBody.BackgroundColor), "Property is required for class CreateCustomRewardsBody.");

            writer.WriteString("title", createCustomRewardsBody.Title);

            writer.WriteNumber("cost", createCustomRewardsBody.Cost);

            if (createCustomRewardsBody.PromptOption.IsSet)
                writer.WriteString("prompt", createCustomRewardsBody.Prompt);

            if (createCustomRewardsBody.IsEnabledOption.IsSet)
                writer.WriteBoolean("is_enabled", createCustomRewardsBody.IsEnabledOption.Value!.Value);

            if (createCustomRewardsBody.BackgroundColorOption.IsSet)
                writer.WriteString("background_color", createCustomRewardsBody.BackgroundColor);

            if (createCustomRewardsBody.IsUserInputRequiredOption.IsSet)
                writer.WriteBoolean("is_user_input_required", createCustomRewardsBody.IsUserInputRequiredOption.Value!.Value);

            if (createCustomRewardsBody.IsMaxPerStreamEnabledOption.IsSet)
                writer.WriteBoolean("is_max_per_stream_enabled", createCustomRewardsBody.IsMaxPerStreamEnabledOption.Value!.Value);

            if (createCustomRewardsBody.MaxPerStreamOption.IsSet)
                writer.WriteNumber("max_per_stream", createCustomRewardsBody.MaxPerStreamOption.Value!.Value);

            if (createCustomRewardsBody.IsMaxPerUserPerStreamEnabledOption.IsSet)
                writer.WriteBoolean("is_max_per_user_per_stream_enabled", createCustomRewardsBody.IsMaxPerUserPerStreamEnabledOption.Value!.Value);

            if (createCustomRewardsBody.MaxPerUserPerStreamOption.IsSet)
                writer.WriteNumber("max_per_user_per_stream", createCustomRewardsBody.MaxPerUserPerStreamOption.Value!.Value);

            if (createCustomRewardsBody.IsGlobalCooldownEnabledOption.IsSet)
                writer.WriteBoolean("is_global_cooldown_enabled", createCustomRewardsBody.IsGlobalCooldownEnabledOption.Value!.Value);

            if (createCustomRewardsBody.GlobalCooldownSecondsOption.IsSet)
                writer.WriteNumber("global_cooldown_seconds", createCustomRewardsBody.GlobalCooldownSecondsOption.Value!.Value);

            if (createCustomRewardsBody.ShouldRedemptionsSkipRequestQueueOption.IsSet)
                writer.WriteBoolean("should_redemptions_skip_request_queue", createCustomRewardsBody.ShouldRedemptionsSkipRequestQueueOption.Value!.Value);
        }
    }
}
