using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Creatures.Dtos;

namespace TwistedTaleweaver.DataAccess.Creatures.Readers;

public interface ICreaturesReader
{
    Task<IEnumerable<CharacterDto>> GetCharactersAsync(List<Guid> characterIds);

    Task<IEnumerable<CharacterDto>> GetCharactersAsync(Guid expeditionId);
    
    Task<IEnumerable<CreatureTraitDto>> GetTraitsAsync(List<Guid> creatureIds);
    
    Task<IEnumerable<CreatureStateDto>> GetCharacterStatesByExpeditionsAsync(List<Guid> expeditionIds);
    
    Task<IEnumerable<CreatureDto>> GetCreaturesAsync(List<Guid> creatureIds);
    
    Task<CharacterDto> GetCharacterAsync(Guid characterId);
}

internal class CreaturesReader(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), ICreaturesReader
{
    public Task<IEnumerable<CharacterDto>> GetCharactersAsync(List<Guid> characterIds)
    {
        const string sql = @"
            SELECT 
                c.creature_id,
                c.name,
                c.description,
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
            WHERE ch.creature_id = ANY(@CharacterIds)";

        return QueryAsync<CharacterDto>(sql, new
        {
            CharacterIds = characterIds
        });
    }
    
    public Task<IEnumerable<CharacterDto>> GetCharactersAsync(Guid expeditionId)
    {
        const string sql = @"
            SELECT 
                c.creature_id,
                c.name,
                c.description,
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
            FROM expeditions e
            INNER JOIN expedition_participants ep ON e.expedition_id = ep.expedition_id 
            INNER JOIN characters ch ON ch.creature_id = ep.character_id
            INNER JOIN combat_creatures cc ON cc.creature_id = ch.creature_id
            INNER JOIN creatures c ON c.creature_id = ch.creature_id
            WHERE e.expedition_id = @ExpeditionId";

        return QueryAsync<CharacterDto>(sql, new
        {
            ExpeditionId = expeditionId
        });
    }

    public Task<IEnumerable<CreatureTraitDto>> GetTraitsAsync(List<Guid> creatureIds)
    {
        const string sql = @"
            SELECT 
                creature_id, 
                trait_name,
                effect,
                condition_description
            FROM creature_traits ct
            WHERE ct.creature_id = ANY(@CreatureIds)";

        return QueryAsync<CreatureTraitDto>(sql, new
        {
            CreatureIds = creatureIds
        });
    }

    public Task<IEnumerable<CreatureStateDto>> GetCharacterStatesByExpeditionsAsync(List<Guid> expeditionIds)
    {
        const string sql = @"
            SELECT
                e.expedition_id,
                c.creature_id,
                cc.max_health
                    + COALESCE(SUM(csc.health_change), 0) AS current_health,
                last_status.status_id AS status
            FROM expeditions e
            INNER JOIN expedition_participants ep
                ON e.expedition_id = ep.expedition_id
            INNER JOIN characters ch
                ON ch.creature_id = ep.character_id
            INNER JOIN creatures c
                ON c.creature_id = ch.creature_id
            INNER JOIN combat_creatures cc
                ON cc.creature_id = c.creature_id
            LEFT JOIN creature_state_changes csc
                ON csc.creature_id = c.creature_id
            LEFT JOIN LATERAL (
                SELECT csc2.status_id
                FROM creature_state_changes csc2
                WHERE csc2.creature_id = c.creature_id
                ORDER BY csc2.changed_at DESC
                LIMIT 1
            ) last_status ON TRUE
            WHERE e.expedition_id = ANY(@ExpeditionIds)
            GROUP BY
                e.expedition_id,
                c.creature_id,
                cc.max_health,
                last_status.status_id";

        return QueryAsync<CreatureStateDto>(sql, new
        {
            ExpeditionIds = expeditionIds
        });
    }

    public Task<IEnumerable<CreatureDto>> GetCreaturesAsync(List<Guid> creatureIds)
    {
        const string sql = @"
            SELECT 
                c.creature_id,
                c.name,
                c.description,
                c.type_id AS creature_type,
                c.alignment_id AS alignment,
                c.gender_id AS gender,
                c.strength,
                c.dexterity,
                c.intelligence,
                c.wisdom,
                c.constitution,
                c.charisma
            FROM creatures c
            WHERE c.creature_id = ANY(@CreatureIds)";

        return QueryAsync<CreatureDto>(sql, new
        {
            CreatureIds = creatureIds
        });
    }

    public Task<CharacterDto> GetCharacterAsync(Guid characterId)
    {
        const string sql = @"
            SELECT 
                c.creature_id,
                c.name,
                c.description,
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
            WHERE c.creature_id = @CreatureId";

        return QuerySingleAsync<CharacterDto>(sql, new
        {
            CreatureId = characterId
        });
    }
}