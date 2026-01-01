using TwistedTaleweaver.Api.Creatures.Commands;
using TwistedTaleweaver.Api.Creatures.Entities.Enums;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Creatures.Mappers;
using TwistedTaleweaver.DataAccess.Creatures.Dtos;
using TwistedTaleweaver.DataAccess.Creatures.Writers;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;
using TwistedTaleweaver.Integration.OpenAi.Creatures;

namespace TwistedTaleweaver.Creatures.CommandHandlers;

internal class CreateCharacterCommandHandler(
    ICharacterGenerationAdapter characterGenerationAdapter,
    IExpeditionsReader expeditionsReader,
    ICreaturesWriter creaturesWriter,
    IExpeditionParticipantsWriter expeditionParticipantsWriter) : ICreateCharacterCommandHandler
{
    public async Task<CreateCharacterResult> ExecuteAsync(CreateCharacterCommand command)
    {
        // TODO: Validation

        var expedition = await expeditionsReader.GetByIdAsync(command.ExpeditionId);

        var generatedCharacter = await characterGenerationAdapter.GenerateAsync(
            new CharacterGenerationCommand()
            {
                CharacterName = command.Name,
                CharacterDescription = command.Description,
                ExpeditionDescription = expedition!.Description,
                CharacterClass = command.CharacterClass,
                Gender = command.CreatureGender,
                CreatureType = command.CreatureType,
                NarrativeAlignment = command.Alignment,
                AttributeScores = command.AttributeScores
            });

        await using var scope = new UnitOfWorkScope();

        var creatureId = await creaturesWriter.CreateAsync(
            generatedCharacter.Name,
            generatedCharacter.Description,
            CreatureStatus.Alive,
            command.CreatureType,
            command.Alignment,
            command.CreatureGender,
            command.AttributeScores);

        await creaturesWriter.SaveCombatDetailsAsync(
            creatureId,
            generatedCharacter.MaxHealth,
            generatedCharacter.ArmorClass,
            generatedCharacter.SpeedScores.ToEntity());

        await creaturesWriter.SaveTraitsAsync(generatedCharacter.Traits
            .Select(x => new CreatureTraitDto()
            {
                CreatureId = creatureId,
                TraitName = x.Name,
                Effect = x.Effect,
                ConditionDescription = x.ConditionForPerform.HasValue ? x.ConditionForPerform.ActualValue : null,
            }).ToList());

        await creaturesWriter.SaveCharacterDetailsAsync(creatureId, command.CharacterClass);

        await expeditionParticipantsWriter.SetCharacterIdAsync(command.ExpeditionId, command.UserId, creatureId);

        await scope.CompleteAsync();

        return new CreateCharacterResult
        {
            CharacterId = creatureId
        };
    }
}