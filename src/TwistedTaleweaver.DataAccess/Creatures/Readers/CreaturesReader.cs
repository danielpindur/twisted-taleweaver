using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Creatures.Dtos;

namespace TwistedTaleweaver.DataAccess.Creatures.Readers;

public interface ICreaturesReader
{
    Task<CharacterDto?> GetCharacterAsync(Guid characterId);
    
    Task<IEnumerable<CreatureActionDto>> GetActionsAsync(Guid creatureId);
    
    Task<IEnumerable<CreatureProficiencyDto>> GetProficienciesAsync(Guid creatureId);
    
    Task<IEnumerable<CreatureTraitDto>> GetTraitsAsync(Guid creatureId);
}

internal class CreaturesReader(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), ICreaturesReader
{
    public Task<CharacterDto?> GetCharacterAsync(Guid characterId)
    {
        const string sql = @"
            SELECT 
                c.creature_id,
                c.name,
                c.context_keywords,
                c.status_id AS status,
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
                cc.current_health,
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
            WHERE ch.creature_id = @CharacterId";

        return QuerySingleOrDefaultAsync<CharacterDto>(sql, new
        {
            CharacterId = characterId
        });
    }

    public Task<IEnumerable<CreatureActionDto>> GetActionsAsync(Guid creatureId)
    {
        const string sql = @"
            SELECT 
                creature_id,
                action_name,
                description,
                is_combat_action,
                is_legendary_action,
                cost,
                recharge,
                range
            FROM creature_actions ca
            WHERE ca.creature_id = @CreatureId";
        
        return QueryAsync<CreatureActionDto>(sql, new
        {
            CreatureId = creatureId
        });
    }

    public Task<IEnumerable<CreatureProficiencyDto>> GetProficienciesAsync(Guid creatureId)
    {
        const string sql = @"
            SELECT 
                creature_id,
                type_id AS type,
                target
            FROM creature_proficiencies cp
            WHERE cp.creature_id = @CreatureId";
        
        return QueryAsync<CreatureProficiencyDto>(sql, new
        {
            CreatureId = creatureId
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