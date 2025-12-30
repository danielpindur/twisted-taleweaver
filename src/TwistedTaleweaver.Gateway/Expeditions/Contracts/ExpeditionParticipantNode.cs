using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Gateway.Creatures.DataLoaders;
using TwistedTaleweaver.Gateway.Users.Contracts;
using TwistedTaleweaver.Gateway.Users.DataLoaders;
using Character = TwistedTaleweaver.Gateway.Creatures.Contracts.Character;

namespace TwistedTaleweaver.Gateway.Expeditions.Contracts;

[ObjectType<ExpeditionParticipant>]
public static partial class ExpeditionParticipantNode
{
    [GraphQLDescription("User of the participant.")]
    public static async Task<User> GetUserAsync(
        [Parent] ExpeditionParticipant participant,
        UserByIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var user = await dataLoader.LoadAsync(participant.UserId, cancellationToken);
        return user!;
    }
    
    [GraphQLDescription("Character of the participant.")]
    public static async Task<Character?> GetCharacterAsync(
        [Parent] ExpeditionParticipant participant,
        CharacterByIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        if (!participant.CharacterId.HasValue)
        {
            return null;
        }
        
        var character = await dataLoader.LoadAsync(participant.CharacterId.Value, cancellationToken);
        return character!;
    }
}