using TwistedTaleweaver.Common.Pagination.Entities;
using TwistedTaleweaver.Expeditions.Entities;
using TwistedTaleweaver.Gateway.Common.Pagination.Contracts;
using TwistedTaleweaver.Gateway.Common.Pagination.Mappers;
using ExpeditionContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.Expedition;
using ExpeditionParticipantContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.ExpeditionParticipant;
using ClerkUser = Clerk.Net.Client.Models.User;

namespace TwistedTaleweaver.Gateway.Expeditions.Mappers;

internal static class ExpeditionMapper
{
    public static ExpeditionContract ToContract(
        this Expedition source, Dictionary<string, ClerkUser> clerkUserByExternalId)
    {
        return new ExpeditionContract()
        {
            ExpeditionId =  source.ExpeditionId,
            Name =  source.Name,
            Description =  source.Description,
            Status =  source.Status,
            Difficulty =  source.Difficulty,
            MaxParticipants = source.MaxParticipants,
            Outcome = source.Outcome,
            Participants = source.Participants.ToContract(clerkUserByExternalId),
            CreatedAt = source.CreatedAt,
            UpdatedAt = source.UpdatedAt,
            StartedAt = source.StartedAt,
            CompletedAt = source.CompletedAt
        };
    }

    public static PaginatedResponse<ExpeditionContract> ToContract(
        this PaginatedResult<Expedition> source, Dictionary<string, ClerkUser> clerkUserByExternalId)
    {
        return source.ToContract(x => x.ToContract(clerkUserByExternalId));
    }

    private static ExpeditionParticipantContract ToContract(
        this ExpeditionParticipant source, Dictionary<string, ClerkUser> clerkUserByExternalId)
    {
        return new ExpeditionParticipantContract()
        {
            UserId = source.UserId,
            Name = clerkUserByExternalId[source.ExternalUserId].Username!
        };
    }

    public static List<ExpeditionParticipantContract> ToContract(
        this List<ExpeditionParticipant> source,
        Dictionary<string, ClerkUser> clerkUserByExternalId)
    {
        return source.Select(x => x.ToContract(clerkUserByExternalId)).ToList();
    }
}