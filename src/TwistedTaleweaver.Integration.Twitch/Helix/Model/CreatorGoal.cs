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
    /// CreatorGoal
    /// </summary>
    public partial class CreatorGoal : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatorGoal" /> class.
        /// </summary>
        /// <param name="id">An ID that identifies this goal.</param>
        /// <param name="broadcasterId">An ID that identifies the broadcaster that created the goal.</param>
        /// <param name="broadcasterName">The broadcaster’s display name.</param>
        /// <param name="broadcasterLogin">The broadcaster’s login name.</param>
        /// <param name="type">The type of goal. Possible values are:       * follower — The goal is to increase followers. * subscription — The goal is to increase subscriptions. This type shows the net increase or decrease in tier points associated with the subscriptions. * subscription\\_count — The goal is to increase subscriptions. This type shows the net increase or decrease in the number of subscriptions. * new\\_subscription — The goal is to increase subscriptions. This type shows only the net increase in tier points associated with the subscriptions (it does not account for users that unsubscribed since the goal started). * new\\_subscription\\_count — The goal is to increase subscriptions. This type shows only the net increase in the number of subscriptions (it does not account for users that unsubscribed since the goal started).</param>
        /// <param name="description">A description of the goal. Is an empty string if not specified.</param>
        /// <param name="currentAmount">The goal’s current value.      The goal’s &#x60;type&#x60; determines how this value is increased or decreased.       * If &#x60;type&#x60; is follower, this field is set to the broadcaster&#39;s current number of followers. This number increases with new followers and decreases when users unfollow the broadcaster. * If &#x60;type&#x60; is subscription, this field is increased and decreased by the points value associated with the subscription tier. For example, if a tier-two subscription is worth 2 points, this field is increased or decreased by 2, not 1. * If &#x60;type&#x60; is subscription\\_count, this field is increased by 1 for each new subscription and decreased by 1 for each user that unsubscribes. * If &#x60;type&#x60; is new\\_subscription, this field is increased by the points value associated with the subscription tier. For example, if a tier-two subscription is worth 2 points, this field is increased by 2, not 1. * If &#x60;type&#x60; is new\\_subscription\\_count, this field is increased by 1 for each new subscription.</param>
        /// <param name="targetAmount">The goal’s target value. For example, if the broadcaster has 200 followers before creating the goal, and their goal is to double that number, this field is set to 400.</param>
        /// <param name="createdAt">The UTC date and time (in RFC3339 format) that the broadcaster created the goal.</param>
        [JsonConstructor]
        public CreatorGoal(string id, string broadcasterId, string broadcasterName, string broadcasterLogin, TypeEnum type, string description, int currentAmount, int targetAmount, DateTime createdAt)
        {
            Id = id;
            BroadcasterId = broadcasterId;
            BroadcasterName = broadcasterName;
            BroadcasterLogin = broadcasterLogin;
            Type = type;
            Description = description;
            CurrentAmount = currentAmount;
            TargetAmount = targetAmount;
            CreatedAt = createdAt;
            OnCreated();
        }

        partial void OnCreated();

        /// <summary>
        /// The type of goal. Possible values are:       * follower — The goal is to increase followers. * subscription — The goal is to increase subscriptions. This type shows the net increase or decrease in tier points associated with the subscriptions. * subscription\\_count — The goal is to increase subscriptions. This type shows the net increase or decrease in the number of subscriptions. * new\\_subscription — The goal is to increase subscriptions. This type shows only the net increase in tier points associated with the subscriptions (it does not account for users that unsubscribed since the goal started). * new\\_subscription\\_count — The goal is to increase subscriptions. This type shows only the net increase in the number of subscriptions (it does not account for users that unsubscribed since the goal started).
        /// </summary>
        /// <value>The type of goal. Possible values are:       * follower — The goal is to increase followers. * subscription — The goal is to increase subscriptions. This type shows the net increase or decrease in tier points associated with the subscriptions. * subscription\\_count — The goal is to increase subscriptions. This type shows the net increase or decrease in the number of subscriptions. * new\\_subscription — The goal is to increase subscriptions. This type shows only the net increase in tier points associated with the subscriptions (it does not account for users that unsubscribed since the goal started). * new\\_subscription\\_count — The goal is to increase subscriptions. This type shows only the net increase in the number of subscriptions (it does not account for users that unsubscribed since the goal started).</value>
        public enum TypeEnum
        {
            /// <summary>
            /// Enum Follower for value: follower
            /// </summary>
            Follower = 1,

            /// <summary>
            /// Enum Subscription for value: subscription
            /// </summary>
            Subscription = 2,

            /// <summary>
            /// Enum SubscriptionCount for value: subscription_count
            /// </summary>
            SubscriptionCount = 3,

            /// <summary>
            /// Enum NewSubscription for value: new_subscription
            /// </summary>
            NewSubscription = 4,

            /// <summary>
            /// Enum NewSubscriptionCount for value: new_subscription_count
            /// </summary>
            NewSubscriptionCount = 5
        }

        /// <summary>
        /// Returns a <see cref="TypeEnum"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static TypeEnum TypeEnumFromString(string value)
        {
            if (value.Equals("follower"))
                return TypeEnum.Follower;

            if (value.Equals("subscription"))
                return TypeEnum.Subscription;

            if (value.Equals("subscription_count"))
                return TypeEnum.SubscriptionCount;

            if (value.Equals("new_subscription"))
                return TypeEnum.NewSubscription;

            if (value.Equals("new_subscription_count"))
                return TypeEnum.NewSubscriptionCount;

            throw new NotImplementedException($"Could not convert value to type TypeEnum: '{value}'");
        }

        /// <summary>
        /// Returns a <see cref="TypeEnum"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TypeEnum? TypeEnumFromStringOrDefault(string value)
        {
            if (value.Equals("follower"))
                return TypeEnum.Follower;

            if (value.Equals("subscription"))
                return TypeEnum.Subscription;

            if (value.Equals("subscription_count"))
                return TypeEnum.SubscriptionCount;

            if (value.Equals("new_subscription"))
                return TypeEnum.NewSubscription;

            if (value.Equals("new_subscription_count"))
                return TypeEnum.NewSubscriptionCount;

            return null;
        }

        /// <summary>
        /// Converts the <see cref="TypeEnum"/> to the json value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string TypeEnumToJsonValue(TypeEnum value)
        {
            if (value == TypeEnum.Follower)
                return "follower";

            if (value == TypeEnum.Subscription)
                return "subscription";

            if (value == TypeEnum.SubscriptionCount)
                return "subscription_count";

            if (value == TypeEnum.NewSubscription)
                return "new_subscription";

            if (value == TypeEnum.NewSubscriptionCount)
                return "new_subscription_count";

            throw new NotImplementedException($"Value could not be handled: '{value}'");
        }

        /// <summary>
        /// The type of goal. Possible values are:       * follower — The goal is to increase followers. * subscription — The goal is to increase subscriptions. This type shows the net increase or decrease in tier points associated with the subscriptions. * subscription\\_count — The goal is to increase subscriptions. This type shows the net increase or decrease in the number of subscriptions. * new\\_subscription — The goal is to increase subscriptions. This type shows only the net increase in tier points associated with the subscriptions (it does not account for users that unsubscribed since the goal started). * new\\_subscription\\_count — The goal is to increase subscriptions. This type shows only the net increase in the number of subscriptions (it does not account for users that unsubscribed since the goal started).
        /// </summary>
        /// <value>The type of goal. Possible values are:       * follower — The goal is to increase followers. * subscription — The goal is to increase subscriptions. This type shows the net increase or decrease in tier points associated with the subscriptions. * subscription\\_count — The goal is to increase subscriptions. This type shows the net increase or decrease in the number of subscriptions. * new\\_subscription — The goal is to increase subscriptions. This type shows only the net increase in tier points associated with the subscriptions (it does not account for users that unsubscribed since the goal started). * new\\_subscription\\_count — The goal is to increase subscriptions. This type shows only the net increase in the number of subscriptions (it does not account for users that unsubscribed since the goal started).</value>
        [JsonPropertyName("type")]
        public TypeEnum Type { get; set; }

        /// <summary>
        /// An ID that identifies this goal.
        /// </summary>
        /// <value>An ID that identifies this goal.</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// An ID that identifies the broadcaster that created the goal.
        /// </summary>
        /// <value>An ID that identifies the broadcaster that created the goal.</value>
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; set; }

        /// <summary>
        /// The broadcaster’s display name.
        /// </summary>
        /// <value>The broadcaster’s display name.</value>
        [JsonPropertyName("broadcaster_name")]
        public string BroadcasterName { get; set; }

        /// <summary>
        /// The broadcaster’s login name.
        /// </summary>
        /// <value>The broadcaster’s login name.</value>
        [JsonPropertyName("broadcaster_login")]
        public string BroadcasterLogin { get; set; }

        /// <summary>
        /// A description of the goal. Is an empty string if not specified.
        /// </summary>
        /// <value>A description of the goal. Is an empty string if not specified.</value>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The goal’s current value.      The goal’s &#x60;type&#x60; determines how this value is increased or decreased.       * If &#x60;type&#x60; is follower, this field is set to the broadcaster&#39;s current number of followers. This number increases with new followers and decreases when users unfollow the broadcaster. * If &#x60;type&#x60; is subscription, this field is increased and decreased by the points value associated with the subscription tier. For example, if a tier-two subscription is worth 2 points, this field is increased or decreased by 2, not 1. * If &#x60;type&#x60; is subscription\\_count, this field is increased by 1 for each new subscription and decreased by 1 for each user that unsubscribes. * If &#x60;type&#x60; is new\\_subscription, this field is increased by the points value associated with the subscription tier. For example, if a tier-two subscription is worth 2 points, this field is increased by 2, not 1. * If &#x60;type&#x60; is new\\_subscription\\_count, this field is increased by 1 for each new subscription.
        /// </summary>
        /// <value>The goal’s current value.      The goal’s &#x60;type&#x60; determines how this value is increased or decreased.       * If &#x60;type&#x60; is follower, this field is set to the broadcaster&#39;s current number of followers. This number increases with new followers and decreases when users unfollow the broadcaster. * If &#x60;type&#x60; is subscription, this field is increased and decreased by the points value associated with the subscription tier. For example, if a tier-two subscription is worth 2 points, this field is increased or decreased by 2, not 1. * If &#x60;type&#x60; is subscription\\_count, this field is increased by 1 for each new subscription and decreased by 1 for each user that unsubscribes. * If &#x60;type&#x60; is new\\_subscription, this field is increased by the points value associated with the subscription tier. For example, if a tier-two subscription is worth 2 points, this field is increased by 2, not 1. * If &#x60;type&#x60; is new\\_subscription\\_count, this field is increased by 1 for each new subscription.</value>
        [JsonPropertyName("current_amount")]
        public int CurrentAmount { get; set; }

        /// <summary>
        /// The goal’s target value. For example, if the broadcaster has 200 followers before creating the goal, and their goal is to double that number, this field is set to 400.
        /// </summary>
        /// <value>The goal’s target value. For example, if the broadcaster has 200 followers before creating the goal, and their goal is to double that number, this field is set to 400.</value>
        [JsonPropertyName("target_amount")]
        public int TargetAmount { get; set; }

        /// <summary>
        /// The UTC date and time (in RFC3339 format) that the broadcaster created the goal.
        /// </summary>
        /// <value>The UTC date and time (in RFC3339 format) that the broadcaster created the goal.</value>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CreatorGoal {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  BroadcasterId: ").Append(BroadcasterId).Append("\n");
            sb.Append("  BroadcasterName: ").Append(BroadcasterName).Append("\n");
            sb.Append("  BroadcasterLogin: ").Append(BroadcasterLogin).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  CurrentAmount: ").Append(CurrentAmount).Append("\n");
            sb.Append("  TargetAmount: ").Append(TargetAmount).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
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
    /// A Json converter for type <see cref="CreatorGoal" />
    /// </summary>
    public class CreatorGoalJsonConverter : JsonConverter<CreatorGoal>
    {
        /// <summary>
        /// The format to use to serialize CreatedAt
        /// </summary>
        public static string CreatedAtFormat { get; set; } = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";

        /// <summary>
        /// Deserializes json to <see cref="CreatorGoal" />
        /// </summary>
        /// <param name="utf8JsonReader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override CreatorGoal Read(ref Utf8JsonReader utf8JsonReader, Type typeToConvert, JsonSerializerOptions jsonSerializerOptions)
        {
            int currentDepth = utf8JsonReader.CurrentDepth;

            if (utf8JsonReader.TokenType != JsonTokenType.StartObject && utf8JsonReader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            JsonTokenType startingTokenType = utf8JsonReader.TokenType;

            Option<string?> id = default;
            Option<string?> broadcasterId = default;
            Option<string?> broadcasterName = default;
            Option<string?> broadcasterLogin = default;
            Option<CreatorGoal.TypeEnum?> type = default;
            Option<string?> description = default;
            Option<int?> currentAmount = default;
            Option<int?> targetAmount = default;
            Option<DateTime?> createdAt = default;

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
                        case "broadcaster_name":
                            broadcasterName = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "broadcaster_login":
                            broadcasterLogin = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "type":
                            string? typeRawValue = utf8JsonReader.GetString();
                            if (typeRawValue != null)
                                type = new Option<CreatorGoal.TypeEnum?>(CreatorGoal.TypeEnumFromStringOrDefault(typeRawValue));
                            break;
                        case "description":
                            description = new Option<string?>(utf8JsonReader.GetString()!);
                            break;
                        case "current_amount":
                            currentAmount = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "target_amount":
                            targetAmount = new Option<int?>(utf8JsonReader.TokenType == JsonTokenType.Null ? (int?)null : utf8JsonReader.GetInt32());
                            break;
                        case "created_at":
                            createdAt = new Option<DateTime?>(JsonSerializer.Deserialize<DateTime>(ref utf8JsonReader, jsonSerializerOptions));
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!id.IsSet)
                throw new ArgumentException("Property is required for class CreatorGoal.", nameof(id));

            if (!broadcasterId.IsSet)
                throw new ArgumentException("Property is required for class CreatorGoal.", nameof(broadcasterId));

            if (!broadcasterName.IsSet)
                throw new ArgumentException("Property is required for class CreatorGoal.", nameof(broadcasterName));

            if (!broadcasterLogin.IsSet)
                throw new ArgumentException("Property is required for class CreatorGoal.", nameof(broadcasterLogin));

            if (!type.IsSet)
                throw new ArgumentException("Property is required for class CreatorGoal.", nameof(type));

            if (!description.IsSet)
                throw new ArgumentException("Property is required for class CreatorGoal.", nameof(description));

            if (!currentAmount.IsSet)
                throw new ArgumentException("Property is required for class CreatorGoal.", nameof(currentAmount));

            if (!targetAmount.IsSet)
                throw new ArgumentException("Property is required for class CreatorGoal.", nameof(targetAmount));

            if (!createdAt.IsSet)
                throw new ArgumentException("Property is required for class CreatorGoal.", nameof(createdAt));

            if (id.IsSet && id.Value == null)
                throw new ArgumentNullException(nameof(id), "Property is not nullable for class CreatorGoal.");

            if (broadcasterId.IsSet && broadcasterId.Value == null)
                throw new ArgumentNullException(nameof(broadcasterId), "Property is not nullable for class CreatorGoal.");

            if (broadcasterName.IsSet && broadcasterName.Value == null)
                throw new ArgumentNullException(nameof(broadcasterName), "Property is not nullable for class CreatorGoal.");

            if (broadcasterLogin.IsSet && broadcasterLogin.Value == null)
                throw new ArgumentNullException(nameof(broadcasterLogin), "Property is not nullable for class CreatorGoal.");

            if (type.IsSet && type.Value == null)
                throw new ArgumentNullException(nameof(type), "Property is not nullable for class CreatorGoal.");

            if (description.IsSet && description.Value == null)
                throw new ArgumentNullException(nameof(description), "Property is not nullable for class CreatorGoal.");

            if (currentAmount.IsSet && currentAmount.Value == null)
                throw new ArgumentNullException(nameof(currentAmount), "Property is not nullable for class CreatorGoal.");

            if (targetAmount.IsSet && targetAmount.Value == null)
                throw new ArgumentNullException(nameof(targetAmount), "Property is not nullable for class CreatorGoal.");

            if (createdAt.IsSet && createdAt.Value == null)
                throw new ArgumentNullException(nameof(createdAt), "Property is not nullable for class CreatorGoal.");

            return new CreatorGoal(id.Value!, broadcasterId.Value!, broadcasterName.Value!, broadcasterLogin.Value!, type.Value!.Value!, description.Value!, currentAmount.Value!.Value!, targetAmount.Value!.Value!, createdAt.Value!.Value!);
        }

        /// <summary>
        /// Serializes a <see cref="CreatorGoal" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="creatorGoal"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, CreatorGoal creatorGoal, JsonSerializerOptions jsonSerializerOptions)
        {
            writer.WriteStartObject();

            WriteProperties(writer, creatorGoal, jsonSerializerOptions);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes the properties of <see cref="CreatorGoal" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="creatorGoal"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void WriteProperties(Utf8JsonWriter writer, CreatorGoal creatorGoal, JsonSerializerOptions jsonSerializerOptions)
        {
            if (creatorGoal.Id == null)
                throw new ArgumentNullException(nameof(creatorGoal.Id), "Property is required for class CreatorGoal.");

            if (creatorGoal.BroadcasterId == null)
                throw new ArgumentNullException(nameof(creatorGoal.BroadcasterId), "Property is required for class CreatorGoal.");

            if (creatorGoal.BroadcasterName == null)
                throw new ArgumentNullException(nameof(creatorGoal.BroadcasterName), "Property is required for class CreatorGoal.");

            if (creatorGoal.BroadcasterLogin == null)
                throw new ArgumentNullException(nameof(creatorGoal.BroadcasterLogin), "Property is required for class CreatorGoal.");

            if (creatorGoal.Description == null)
                throw new ArgumentNullException(nameof(creatorGoal.Description), "Property is required for class CreatorGoal.");

            writer.WriteString("id", creatorGoal.Id);

            writer.WriteString("broadcaster_id", creatorGoal.BroadcasterId);

            writer.WriteString("broadcaster_name", creatorGoal.BroadcasterName);

            writer.WriteString("broadcaster_login", creatorGoal.BroadcasterLogin);

            var typeRawValue = CreatorGoal.TypeEnumToJsonValue(creatorGoal.Type);
            writer.WriteString("type", typeRawValue);
            writer.WriteString("description", creatorGoal.Description);

            writer.WriteNumber("current_amount", creatorGoal.CurrentAmount);

            writer.WriteNumber("target_amount", creatorGoal.TargetAmount);

            writer.WriteString("created_at", creatorGoal.CreatedAt.ToString(CreatedAtFormat));
        }
    }
}
