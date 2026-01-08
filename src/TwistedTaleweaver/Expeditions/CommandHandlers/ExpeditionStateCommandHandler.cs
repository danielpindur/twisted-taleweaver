using TwistedTaleweaver.Api.Expeditions.Queries;
using TwistedTaleweaver.Core.Commands;
using TwistedTaleweaver.Core.Kafka;
using TwistedTaleweaver.Expeditions.Mappers;

namespace TwistedTaleweaver.Expeditions.CommandHandlers;

internal class PublishUpdatedExpeditionStateCommand
{
    public Guid ExpeditionId { get; set; }
}

internal interface IExpeditionStateCommandHandler : ICommandHandler<PublishUpdatedExpeditionStateCommand>
{
}

internal class ExpeditionStateCommandHandler(
    IExpeditionStatesQueryHandler expeditionStatesQueryHandler,
    IKafkaEventPublisher kafkaEventPublisher) : IExpeditionStateCommandHandler
{
    public async Task ExecuteAsync(PublishUpdatedExpeditionStateCommand command)
    {
        var expeditionState = await expeditionStatesQueryHandler.ExecuteAsync(
            new ExpeditionStateQuery() { ExpeditionId = command.ExpeditionId });
        
        await kafkaEventPublisher.PublishAsync(expeditionState.ToExpeditionStateUpdatedEvent());
    }
}