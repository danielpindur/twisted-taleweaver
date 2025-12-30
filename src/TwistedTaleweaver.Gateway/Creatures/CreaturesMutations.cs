using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Authorization;
using TwistedTaleweaver.Api.Creatures.Commands;
using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Gateway.Auth;
using TwistedTaleweaver.Gateway.Creatures.Contracts;
using TwistedTaleweaver.Gateway.Creatures.Mappers;
using TwistedTaleweaver.Gateway.Expeditions.Helpers;
using TwistedTaleweaver.Gateway.Expeditions.Mappers;
using AttributeScores = TwistedTaleweaver.Api.Creatures.Entities.AttributeScores;

namespace TwistedTaleweaver.Gateway.Creatures;

[MutationType]
public static class CreaturesMutations
{
    [GraphQLDescription("Creates a new character.")]
    [Authorize]
    public static async Task<Character> AddCharacterAsync(
        AddCharacterInput input,
        [Service] IUserProvider userProvider,
        [Service] ICreateCharacterCommandHandler createCharacterCommandHandler,
        [Service] ICharactersQueryHandler charactersQueryHandler,
        [Service] IExpeditionByIdQueryHandler expeditionByIdQueryHandler,
        [Service] ITopicEventSender topicEventSender)
    {
        var userId = await userProvider.GetUserIdAsync();

        var creationResult = await createCharacterCommandHandler.ExecuteAsync(
            new CreateCharacterCommand()
            {
                Name = input.Name,
                Description = input.Description,
                ExpeditionId = input.ExpeditionId,
                CharacterClass = input.Class,
                Alignment = input.Alignment,
                CreatureType = input.CreatureType,
                CreatureGender = input.Gender,
                AttributeScores = new AttributeScores()
                {
                    Strength = input.AttributeScores.Strength,
                    Charisma = input.AttributeScores.Charisma,
                    Constitution = input.AttributeScores.Constitution,
                    Dexterity = input.AttributeScores.Dexterity,
                    Intelligence = input.AttributeScores.Intelligence,
                    Wisdom = input.AttributeScores.Wisdom,
                },
                UserId = userId
            });

        var character = await charactersQueryHandler.ExecuteAsync(
            new CharacterByIdQuery() { CharacterId = creationResult.CharacterId });

        var expedition = await expeditionByIdQueryHandler.ExecuteAsync(
            new ExpeditionByIdQuery() { ExpeditionId = input.ExpeditionId });

        await topicEventSender.PublishParticipantsChanged(expedition!.ToContract());
        
        return character!.ToContract();
    }
}