using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Creatures.Dtos;

namespace TwistedTaleweaver.DataAccess.Creatures.Readers;

public interface ICreaturesReader
{
    Task<IEnumerable<CharacterDto>> GetCharactersAsync(List<Guid> characterIds);

    Task<IEnumerable<CreatureTraitDto>> GetTraitsAsync(Guid creatureId);
}

internal class CreaturesReader(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), ICreaturesReader
{
    public Task<IEnumerable<CharacterDto>> GetCharactersAsync(List<Guid> characterIds)
    {
        const string sql = @"
            SELECT 
                c.creature_id,
                c.name,
                c.context_keywords,
                c.type_id AS creature_type,
                c.alignment_id AS alignment,
                c.gender_id AS gender,
                c.strength,
                c.dexterity,
                c.intelligence,
                c.wisdom,
                c.constitution,
                c.charisma,
                cc.max_health,
                cc.armor_class,
                cc.walking_speed,
                cc.flying_speed,
                cc.swimming_speed,
                cc.climbing_speed,
                cc.burrowing_speed,
                cc.teleportation_speed,
                ch.character_class_type_id AS class
            FROM characters ch
            INNER JOIN combat_creatures cc ON cc.creature_id = ch.creature_id
            INNER JOIN creatures c ON c.creature_id = ch.creature_id
            WHERE ch.creature_id = ANY(@CharacterId)";

        return QueryAsync<CharacterDto>(sql, new
        {
            CharacterIds = characterIds
        });
    }

    public Task<IEnumerable<CreatureTraitDto>> GetTraitsAsync(Guid creatureId)
    {
        const string sql = @"
            SELECT 
                creature_id, 
                trait_name,
                effect,
                condition_description
            FROM creature_traits ct
            WHERE ct.creature_id = @CreatureId";

        return QueryAsync<CreatureTraitDto>(sql, new
        {
            CreatureId = creatureId
        });
    }
}