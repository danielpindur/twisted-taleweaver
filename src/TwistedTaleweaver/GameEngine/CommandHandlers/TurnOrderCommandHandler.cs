using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Creatures.QueryHandlers;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;

namespace TwistedTaleweaver.GameEngine.CommandHandlers;

internal class ConfigureTurnOrderCommand
{
    public Guid ExpeditionId { get; set; }
}

internal class AdvanceTurnOrderCommand
{
    public Guid ExpeditionId { get; set; }
}

internal class ActionTakenCommand
{
    public Guid ExpeditionId { get; set; }
}

internal class RetakeActionCommand
{
    public Guid ExpeditionId { get; set; }
}

internal class AddMonsterCommand
{
    public Guid ExpeditionId { get; set; }
    
    public Guid MonsterId { get; set; }
}

internal class RemoveCreatureCommand
{
    public Guid ExpeditionId { get; set; }
    
    public Guid CreatureId { get; set; }
}

internal interface ITurnOrderCommandHandler :
    ICommandHandler<ConfigureTurnOrderCommand>,
    ICommandHandler<AdvanceTurnOrderCommand>,
    ICommandHandler<ActionTakenCommand>,
    ICommandHandler<RetakeActionCommand>,
    ICommandHandler<AddMonsterCommand>,
    ICommandHandler<RemoveCreatureCommand>
{
}

internal class TurnOrderCommandHandler(
    ICharactersQueryHandler charactersQueryHandler,
    IMonstersQueryHandler monstersQueryHandler,
    IExpeditionTurnOrdersReader expeditionTurnOrdersReader,
    IExpeditionTurnOrdersWriter expeditionTurnOrdersWriter) : ITurnOrderCommandHandler
{
    public async Task ExecuteAsync(ConfigureTurnOrderCommand command)
    {
        var characters = await charactersQueryHandler.ExecuteAsync(
            new CharactersByExpeditionIdQuery() { ExpeditionId = command.ExpeditionId });

        var turnOrder = new ExpeditionTurnOrder(command.ExpeditionId);
        foreach (var character in characters)
        {
            turnOrder.Add(character, true);
        }
        turnOrder.InitIndex();
        
        await expeditionTurnOrdersWriter.Save(turnOrder);
    }

    public async Task ExecuteAsync(AdvanceTurnOrderCommand command)
    {
        var turnOrder = await expeditionTurnOrdersReader.GetByExpeditionAsync(command.ExpeditionId);
        turnOrder.NextTurn();
        await expeditionTurnOrdersWriter.Save(turnOrder);
    }

    public async Task ExecuteAsync(ActionTakenCommand command)
    {
        var turnOrder = await expeditionTurnOrdersReader.GetByExpeditionAsync(command.ExpeditionId);
        turnOrder.ActionTaken();
        await expeditionTurnOrdersWriter.Save(turnOrder);
    }

    public async Task ExecuteAsync(RetakeActionCommand command)
    {
        var turnOrder = await expeditionTurnOrdersReader.GetByExpeditionAsync(command.ExpeditionId);
        turnOrder.RetakeAction();
        await expeditionTurnOrdersWriter.Save(turnOrder);
    }

    public async Task ExecuteAsync(AddMonsterCommand command)
    {
        var monster = await monstersQueryHandler.ExecuteAsync(new MonsterByIdQuery() { MonsterId = command.MonsterId });
        var turnOrder = await expeditionTurnOrdersReader.GetByExpeditionAsync(command.ExpeditionId);
        turnOrder.Add(monster, false);
        await expeditionTurnOrdersWriter.Save(turnOrder);
    }

    public async Task ExecuteAsync(RemoveCreatureCommand command)
    {
        var turnOrder = await expeditionTurnOrdersReader.GetByExpeditionAsync(command.ExpeditionId);
        turnOrder.RemoveCreature(command.CreatureId);
        await expeditionTurnOrdersWriter.Save(turnOrder);
    }
}