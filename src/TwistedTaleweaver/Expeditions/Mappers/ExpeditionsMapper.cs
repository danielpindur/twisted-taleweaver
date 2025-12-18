using TwistedTaleweaver.DataAccess.Expeditions.Dtos;
using TwistedTaleweaver.Domain.Creatures;
using TwistedTaleweaver.Domain.Expeditions;
using TwistedTaleweaver.Domain.Users;
using TwistedTaleweaver.Users.Mappers;
using ClerkUser = Clerk.Net.Client.Models.User;

namespace TwistedTaleweaver.Expeditions.Mappers;

internal static class ExpeditionsMapper
{
    public static ExpeditionSlim ToExpeditionSlim(this ExpeditionDto source)
    {
        return new ExpeditionSlim()
        {
            ExpeditionId = source.ExpeditionId,
            Name = source.Name,
            Description = source.Description,
            Status = source.Status,
            Difficulty = source.Difficulty,
            MinParticipants = source.MinParticipants,
            MaxParticipants = source.MaxParticipants,
            CurrentParticipants = source.CurrentParticipants,
            Outcome = source.Outcome
        };
    }

    public static List<ExpeditionSlim> ToExpeditionSlims(this IEnumerable<ExpeditionDto> source)
    {
        return source.Select(ToExpeditionSlim).ToList();
    }
    
    public static Expedition ToExpedition(
        this ExpeditionDto source, 
        List<ExpeditionParticipant> participants,
        Dictionary<string, ClerkUser> userByExternalId)
    {
        return new Expedition()
        {
            ExpeditionId = source.ExpeditionId,
            Name = source.Name,
            Description = source.Description,
            Status = source.Status,
            Difficulty = source.Difficulty,
            MinParticipants = source.MinParticipants,
            MaxParticipants = source.MaxParticipants,
            CurrentParticipants = source.CurrentParticipants,
            Outcome = source.Outcome,
            CreatedByUser = new User()
            {
                UserId = source.CreatedByUserId,
                ExternalUserId = source.CreatedByExternalUserId,
                Name = userByExternalId[source.CreatedByExternalUserId].Username!
            },
            Participants = participants
        };
    }

    public static ExpeditionParticipant ToExpeditionParticipant(
        this ExpeditionParticipantDto source,
        Dictionary<string, ClerkUser> userByExternalId, 
        Dictionary<Guid, Character> charactersById)
    {
        return new ExpeditionParticipant()
        {
            User = source.ToUser(userByExternalId),
            Character = source.CharacterId.HasValue &&
                        charactersById.TryGetValue(source.CharacterId.Value, out var character)
                ? character
                : null
        };
    }

    public static List<ExpeditionParticipant> ToExpeditionParticipants(
        this IEnumerable<ExpeditionParticipantDto> source,
        Dictionary<string, ClerkUser> userByExternalId,
        Dictionary<Guid, Character> charactersById)
    {
        return source.Select(x => ToExpeditionParticipant(x, userByExternalId, charactersById)).ToList();
    }
}