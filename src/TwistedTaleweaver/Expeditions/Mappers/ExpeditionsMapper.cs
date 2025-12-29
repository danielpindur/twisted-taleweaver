using TwistedTaleweaver.DataAccess.Expeditions.Dtos;
using TwistedTaleweaver.Domain.Creatures;
using TwistedTaleweaver.Domain.Creatures.Enums;
using TwistedTaleweaver.Domain.Expeditions;

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
            Outcome = source.Outcome,
            CreatedByUserId = source.CreatedByUserId
        };
    }

    public static List<ExpeditionSlim> ToExpeditionSlims(this IEnumerable<ExpeditionDto> source)
    {
        return source.Select(ToExpeditionSlim).ToList();
    }

    public static Expedition ToExpedition(
        this ExpeditionDto source,
        List<ExpeditionParticipant> participants)
    {
        return new Expedition()
        {
            ExpeditionId = source.ExpeditionId,
            Name = source.Name,
            Description = source.Description,
            Difficulty = source.Difficulty,
            MinParticipants = source.MinParticipants,
            MaxParticipants = source.MaxParticipants,
            CurrentParticipants = source.CurrentParticipants,
            Outcome = source.Outcome,
            CreatedByUserId = source.CreatedByUserId,
            Participants = participants,
            State = new ExpeditionState()
            {
                Status = source.Status,
                /*Participants = participants
                    .Where(x => x.Character is not null)
                    .Select(x => new CombatCreatureState()
                    {
                        CreatureId = x.Character!.CharacterId,
                        Status = CreatureStatus.Alive,
                        CurrentHealth = x.Character!.MaxHealth
                    }).ToList()*/
            }
        };
    }
}