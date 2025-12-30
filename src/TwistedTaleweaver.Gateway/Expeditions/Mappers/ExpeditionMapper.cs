using TwistedTaleweaver.Api.Expeditions.Entities;
using ExpeditionContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.Expedition;
using ExpeditionParticipantContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.ExpeditionParticipant;
using ExpeditionStateContract = TwistedTaleweaver.Gateway.Expeditions.Contracts.ExpeditionState;

namespace TwistedTaleweaver.Gateway.Expeditions.Mappers;

internal static class ExpeditionMapper
{
    public static ExpeditionContract? ToNullableContract(this Expedition? source)
    {
        if (source is null)
        {
            return null;
        }

        return source.ToContract();
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
            CreatedByUserId = source.CreatedByUserId
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
        };
    }
}