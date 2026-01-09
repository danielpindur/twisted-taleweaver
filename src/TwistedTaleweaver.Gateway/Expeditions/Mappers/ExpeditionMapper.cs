using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Api.Expeditions.Events;
using TwistedTaleweaver.Core.Pagination;
using TwistedTaleweaver.Gateway.Creatures.Mappers;
using Expedition = TwistedTaleweaver.Api.Expeditions.Entities.Expedition;
using ExpeditionContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.Expedition;
using ExpeditionParticipant = TwistedTaleweaver.Api.Expeditions.Entities.ExpeditionParticipant;
using ExpeditionParticipantContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.ExpeditionParticipant;
using ExpeditionState = TwistedTaleweaver.Api.Expeditions.Entities.ExpeditionState;
using ExpeditionStateContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.ExpeditionState;
using ExpeditionStatisticsContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.ExpeditionStatistics;
using ExpeditionOffsetPage = TwistedTaleweaver.Gateway.Expeditions.Contracts.ExpeditionOffsetPage;
using ExpeditionSearchInput = TwistedTaleweaver.Gateway.Expeditions.Contracts.ExpeditionSearchInput;

namespace TwistedTaleweaver.Gateway.Expeditions.Mappers;

internal static class ExpeditionMapper
{
    public static ExpeditionContract? ToNullableContract(this Expedition? source)
    {
        return source?.ToContract();
    }
    
    public static ExpeditionContract ToContract(this Expedition source)
    {
        return new ExpeditionContract()
        {
            Id = source.ExpeditionId,
            Name = source.Name,
            Description = source.Description,
            Difficulty = source.Difficulty,
            MinParticipants = source.MinParticipants,
            MaxParticipants = source.MaxParticipants,
            CurrentParticipants = source.CurrentParticipants,
            CreatedByUserId = source.CreatedByUserId,
            Outcome = source.Outcome,
            LessonLearned = source.LessonLearned,
            CreatedAt = source.CreatedAt,
            CompletedAt = source.CompletedAt,
            StartedAt = source.StartedAt,
        };
    }

    public static Dictionary<Guid, List<ExpeditionParticipantContract>> ToContracts(
        this Dictionary<Guid, List<ExpeditionParticipant>> source)
    {
        var result = new Dictionary<Guid, List<ExpeditionParticipantContract>>();

        foreach (var kvp in source)
        {
            if (!result.ContainsKey(kvp.Key))
            {
                result.Add(kvp.Key, kvp.Value.ToContracts());
            }
        }
        
        return result;
    }

    private static ExpeditionParticipantContract ToContract(this ExpeditionParticipant source)
    {
        return new ExpeditionParticipantContract()
        {
            UserId = source.UserId,
            CharacterId = source.CharacterId
        };
    }

    private static List<ExpeditionParticipantContract> ToContracts(this List<ExpeditionParticipant> source)
    {
        return source.Select(ToContract).ToList();
    }

    public static ExpeditionStateContract ToContract(this ExpeditionState source, Guid expeditionId)
    {
        return new ExpeditionStateContract()
        {
            ExpeditionId = expeditionId,
            Status = source.Status,
            ActiveCreatureId = source.ActiveCreatureId,
            Monsters = source.Monsters.ToContracts()
        };
    }
    
    public static ExpeditionStateContract ToContract(this ExpeditionStateUpdatedEvent source)
    {
        return new ExpeditionStateContract()
        {
            ExpeditionId = source.ExpeditionId,
            Status = source.Status,
            ActiveCreatureId = source.ActiveCreatureId,
            Monsters = source.Monsters.ToContracts()
        };
    }

    public static ExpeditionOffsetPage ToContract(this PaginatedResult<Expedition> source, ExpeditionSearchInput input)
    {
        return new ExpeditionOffsetPage()
        {
            Items = source.Items.Select(ToContract).ToList(),
            Page = source.PageNumber,
            PageSize = source.PageSize,
            TotalCount = source.TotalItems,
            ExpeditionSearchInput = input
        };
    }

    public static ExpeditionStatisticsContract ToContract(this ExpeditionStatistics source)
    {
        return new ExpeditionStatisticsContract()
        {
            TotalExpeditions = source.TotalExpeditions,
            SuccessfulExpeditions = source.SuccessfulExpeditions,
            FailedExpeditions = source.FailedExpeditions,
        };
    }
}