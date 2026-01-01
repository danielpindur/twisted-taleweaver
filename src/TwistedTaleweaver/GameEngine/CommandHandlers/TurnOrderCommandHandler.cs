using TwistedTaleweaver.Api.Creatures.Queries;
using TwistedTaleweaver.Api.Expeditions.Entities;
using TwistedTaleweaver.Core.Commands;
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

internal interface ITurnOrderCommandHandler :
    ICommandHandler<ConfigureTurnOrderCommand>,
    ICommandHandler<AdvanceTurnOrderCommand>,
    ICommandHandler<ActionTakenCommand>
{
}

internal class TurnOrderCommandHandler(
    ICharactersQueryHandler charactersQueryHandler,
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
            turnOrder.Add(character);
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
}