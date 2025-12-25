using FluentValidation;
using TwistedTaleweaver.Api.Expeditions.Commands;
using TwistedTaleweaver.DataAccess.Expeditions.Readers;

namespace TwistedTaleweaver.Expeditions.Validators;

internal class JoinExpeditionCommandValidator : AbstractValidator<JoinExpeditionCommand>
{
    private readonly IExpeditionsReader _expeditionsReader;
    private readonly IExpeditionParticipantsReader _expeditionParticipantsReader;

    private void InitialiseRules()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
                .WithMessage("UserId is a required field");

        RuleFor(x => x.ExpeditionId)
            .NotEmpty()
                .WithMessage("ExpeditionId is a required field"); ;

        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ExpeditionExistsAsync)
                .WithMessage("Expedition doesn't exist")
            .MustAsync(HasNotAlreadyJoinedAsync)
                .WithMessage("User has already joined this expedition");
    }

    public JoinExpeditionCommandValidator(
        IExpeditionsReader expeditionsReader,
        IExpeditionParticipantsReader expeditionParticipantsReader)
    {
        _expeditionsReader = expeditionsReader;
        _expeditionParticipantsReader = expeditionParticipantsReader;

        InitialiseRules();
    }

    private async Task<bool> HasNotAlreadyJoinedAsync(JoinExpeditionCommand command, CancellationToken cancellationToken)
    {
        var hasJoined = await _expeditionParticipantsReader.ExistsAsync(command.ExpeditionId, command.UserId);
        return !hasJoined;
    }

    private async Task<bool> ExpeditionExistsAsync(JoinExpeditionCommand command, CancellationToken cancellationToken)
    {
        return await _expeditionsReader.ExistsAsync(command.ExpeditionId);
    }
}