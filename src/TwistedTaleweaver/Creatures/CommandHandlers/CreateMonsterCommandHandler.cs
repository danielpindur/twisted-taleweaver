using TwistedTaleweaver.Api.Creatures.Entities.Enums;
using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Creatures.Mappers;
using TwistedTaleweaver.DataAccess.Creatures.Dtos;
using TwistedTaleweaver.DataAccess.Creatures.Writers;
using TwistedTaleweaver.GameEngine.CommandHandlers;
using TwistedTaleweaver.Integration.OpenAi.Creatures.Entities;

namespace TwistedTaleweaver.Creatures.CommandHandlers;

internal class CreateMonsterCommand
{
    public Guid ExpeditionId { get; set; }
    
    public required GeneratedMonster Monster { get; set; }
}

internal interface ICreateMonsterCommandHandler : ICommandHandler<CreateMonsterCommand>
{
}

internal class CreateMonsterCommandHandler(
    ICreaturesWriter creaturesWriter,
    ITurnOrderCommandHandler turnOrderCommandHandler) : ICreateMonsterCommandHandler
{
    public async Task ExecuteAsync(CreateMonsterCommand command)
    {
        await using var scope = new UnitOfWorkScope();
       
        var creatureId = await creaturesWriter.CreateAsync(
            command.Monster.Name,
            command.Monster.Description,
            CreatureStatus.Alive,
            command.Monster.CreatureType,
            command.Monster.NarrativeAlignment,
            command.Monster.CreatureGender,
            command.Monster.AttributeScores);

        await creaturesWriter.SaveCombatDetailsAsync(
            creatureId,
            command.Monster.MaxHealth,
            command.Monster.ArmorClass,
            command.Monster.SpeedScores.ToEntity());

        await creaturesWriter.SaveTraitsAsync(command.Monster.Traits
            .Select(x => new CreatureTraitDto()
            {
                CreatureId = creatureId,
                TraitName = x.Name,
                Effect = x.Effect,
                ConditionDescription = x.ConditionForPerform.HasValue ? x.ConditionForPerform.ActualValue : null,
            }).ToList());

        await creaturesWriter.SaveMonsterDetailsAsync(creatureId, command.ExpeditionId, command.Monster.ChallengeRating);

        await turnOrderCommandHandler.ExecuteAsync(new AddMonsterCommand() { ExpeditionId = command.ExpeditionId, MonsterId = creatureId });
        
        await scope.CompleteAsync();
    }
}