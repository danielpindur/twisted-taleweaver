using TwistedTaleweaver.Api.Creatures.Entities;
using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.DataAccess.Creatures.Writers;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class UpdateCreaturesStateCommand
{
    public required List<CreatureStateUpdate> StateUpdates { get; set; }
}

internal interface IUpdateCreaturesStateCommandHandler : ICommandHandler<UpdateCreaturesStateCommand>
{
}

internal class UpdateCreaturesStateCommandHandler(
    ICreaturesWriter creaturesWriter) : IUpdateCreaturesStateCommandHandler
{
    public Task ExecuteAsync(UpdateCreaturesStateCommand command)
    {
        return creaturesWriter.SaveStateUpdatesAsync(command.StateUpdates);
    }
}