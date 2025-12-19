using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.DataAccess.Expeditions.Writers;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class BeginExpeditionCommandHandler(IExpeditionsWriter expeditionsWriter) : IBeginExpeditionCommandHandler
{
    public async Task ExecuteAsync(BeginExpeditionCommand command)
    {
        // TODO: Validation
        
        await expeditionsWriter.StartAsync(command.ExpeditionId);
    }
}