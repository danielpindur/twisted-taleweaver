using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Api.Creatures.Entities.Enums;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Creatures.Dtos;

namespace TwistedTaleweaver.DataAccess.Creatures.Writers;

public interface ICreaturesWriter
{
    Task<Guid> CreateAsync(
        string name,
        string description,
        CreatureStatus status,
        CreatureType type,
        NarrativeAlignment narrativeAlignment,
        CreatureGender gender,
        AttributeScores attributeScores);

    Task SaveCombatDetailsAsync(
        Guid creatureId,
        int maxHealth,
        int armorClass,
        SpeedScores speedScores);

    Task SaveTraitsAsync(List<CreatureTraitDto> traits);

    Task SaveCharacterDetailsAsync(Guid creatureId, CharacterClass @class);
    
    Task SaveMonsterDetailsAsync(Guid creatureId, Guid expeditionId, int challengeRating);
    
    Task SaveStateUpdatesAsync(List<CreatureStateUpdate> creatureStateUpdates);
}

internal class CreaturesWriter(IUnitOfWorkProvider provider) : DatabaseAccessor(provider), ICreaturesWriter
{
    public Task<Guid> CreateAsync(string name, string description, CreatureStatus status, CreatureType type,
        NarrativeAlignment narrativeAlignment, CreatureGender gender, AttributeScores attributeScores)
    {
        const string sql = @"
            INSERT INTO creatures 
            (name, description, type_id, alignment_id, strength, dexterity, constitution, intelligence, wisdom, charisma, gender_id)
            VALUES 
            (@Name, @Description, @Type, @NarrativeAlignment, @Strength, @Dexterity, @Constitution, @Intelligence, @Wisdom, @Charisma, @Gender)
            RETURNING creature_id";

        return ExecuteWithOutputAsync<Guid>(sql, new
        {
            Name = name,
            Description = description,
            Status = status,
            Type = type,
            NarrativeAlignment = narrativeAlignment,
            Gender = gender,
            attributeScores.Strength,
            attributeScores.Charisma,
            attributeScores.Intelligence,
            attributeScores.Wisdom,
            attributeScores.Constitution,
            attributeScores.Dexterity
        });
    }

    public Task SaveCombatDetailsAsync(Guid creatureId, int maxHealth, int armorClass,
        SpeedScores speedScores)
    {
        const string sql = @"
            INSERT INTO combat_creatures 
            (creature_id, max_health, armor_class, walking_speed, flying_speed, swimming_speed, climbing_speed, burrowing_speed, teleportation_speed)
            VALUES 
            (@CreatureId, @MaxHealth, @ArmorClass, @WalkingSpeed, @FlyingSpeed, @SwimmingSpeed, @ClimbingSpeed, @BurrowingSpeed, @TeleportationSpeed)";

        return ExecuteAsync(sql, new
        {
            CreatureId = creatureId,
            MaxHealth = maxHealth,
            ArmorClass = armorClass,
            WalkingSpeed = speedScores.Walking,
            FlyingSpeed = speedScores.Flying,
            SwimmingSpeed = speedScores.Swimming,
            ClimbingSpeed = speedScores.Climbing,
            BurrowingSpeed = speedScores.Burrowing,
            TeleportationSpeed = speedScores.Teleportation
        });
    }

    public Task SaveTraitsAsync(List<CreatureTraitDto> traits)
    {
        const string sql = @"
            INSERT INTO creature_traits 
            (creature_id, trait_name, effect, condition_description)
            VALUES 
            (@CreatureId, @TraitName, @Effect, @ConditionDescription)";

        return ExecuteAsync(sql, traits);
    }

    public Task SaveCharacterDetailsAsync(Guid creatureId, CharacterClass @class)
    {
        const string sql = @"
            INSERT INTO characters 
            (creature_id, character_class_type_id)
            VALUES 
            (@CreatureId, @ClassType)";

        return ExecuteAsync(sql, new
        {
            CreatureId = creatureId,
            ClassType = @class
        });
    }

    public Task SaveMonsterDetailsAsync(Guid creatureId, Guid expeditionId, int challengeRating)
    {
        const string sql = @"
            INSERT INTO monsters 
            (creature_id, expedition_id, challenge_rating)
            VALUES 
            (@CreatureId, @ExpeditionId, @ChallengeRating)";

        return ExecuteAsync(sql, new
        {
            CreatureId = creatureId,
            ExpeditionId = @expeditionId,
            ChallengeRating = challengeRating
        });
    }

    public Task SaveStateUpdatesAsync(List<CreatureStateUpdate> creatureStateUpdates)
    {
        const string sql = @"
            INSERT INTO creature_state_changes 
            (creature_id, status_id, health_change)
            VALUES 
            (@CreatureId, @Status, @HealthUpdate)";

        return ExecuteAsync(sql, creatureStateUpdates);
    }
}