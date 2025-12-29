using TwistedTaleweaver.Gateway.Users.Contracts;
using TwistedTaleweaver.Gateway.Users.DataLoaders;

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
}