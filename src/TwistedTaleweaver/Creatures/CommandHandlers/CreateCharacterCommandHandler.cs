using TwistedTaleweaver.Api.Creatures.Commands;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.DataAccess.Creatures.Dtos;
using TwistedTaleweaver.DataAccess.Creatures.Writers;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;
using TwistedTaleweaver.Domain.Creatures;
using TwistedTaleweaver.Domain.Creatures.Enums;
using TwistedTaleweaver.Integration.OpenAi.Adapters;
using TwistedTaleweaver.Integration.OpenAi.Creatures;

namespace TwistedTaleweaver.Creatures.CommandHandlers;

internal class CreateCharacterCommandHandler(
    IGenerationAdapter generationAdapter,
    IExpeditionsReader expeditionsReader,
    ICreaturesWriter creaturesWriter,
    IExpeditionParticipantsWriter expeditionParticipantsWriter): ICreateCharacterCommandHandler
{
    public async Task<CreateCharacterResult> ExecuteAsync(CreateCharacterCommand command)
    {
        var expedition = await expeditionsReader.GetByIdAsync(command.ExpeditionId);

        var generatedCharacter = await generationAdapter.GenerateCharacterAsync(
            command.Name, command.Name,
            expedition!.Description,
            command.Class, command.Gender, command.CreatureType, command.Alignment,
            command.AttributeScores);

        await using var scope = new UnitOfWorkScope();
        
        var creatureId = await creaturesWriter.CreateAsync(
            generatedCharacter.Name,
            generatedCharacter.CharacterContextKeywords,
            CreatureStatus.Alive,
            command.CreatureType,
            command.Alignment,
            command.Gender,
            command.AttributeScores);
        
        const int tempHeath = 100; // TODO: Figure out how to do health
        
        await creaturesWriter.SaveCombatDetailsAsync(
            creatureId,
            tempHeath, tempHeath,
            generatedCharacter.ArmorClass,
            ToEntity(generatedCharacter.SpeedScores));

        await creaturesWriter.SaveTraitsAsync(generatedCharacter.Traits
            .Select(x => new CreatureTraitDto()
            {
                CreatureId = creatureId,
                TraitName = x.Name,
                Effect = x.Effect,
                ConditionDescription = x.ConditionForPerform.HasValue ? x.ConditionForPerform.ActualValue : null,
            }).ToList());

        await creaturesWriter.SaveCharacterDetailsAsync(creatureId, command.Class);

        await expeditionParticipantsWriter.SetCharacterIdAsync(command.ExpeditionId, command.UserId, creatureId);
        
        await scope.CompleteAsync();
        
        return new CreateCharacterResult
        {
            CharacterId = creatureId
        };
    }

    // extract to static class
    private static SpeedScores ToEntity(GeneratedSpeedScores source)
    {
        return new SpeedScores()
        {
            Walking = source.Walking,
            Climbing = source.Climbing.HasValue ? source.Climbing.ActualValue : null,
            Swimming = source.Swimming.HasValue ? source.Swimming.ActualValue : null,
            Burrowing = source.Burrowing.HasValue ? source.Burrowing.ActualValue : null,
            Flying = source.Flying.HasValue ? source.Flying.ActualValue : null,
            Teleportation = source.Teleportation.HasValue ? source.Teleportation.ActualValue : null
        };
    }
}