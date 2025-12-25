using FluentValidation;
using TwistedTaleweaver.Api.Expeditions.Commands;

namespace TwistedTaleweaver.Expeditions.Validators;

internal class CreateExpeditionCommandValidator : AbstractValidator<CreateExpeditionCommand>
{
    private void InitialiseRules()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
                .WithMessage("UserId is a required field");

        RuleFor(x => x.MinParticipants)
            .GreaterThanOrEqualTo(1)
                .WithMessage("MinParticipants must be greater than or equal to 1")
            .LessThanOrEqualTo(10)
                .WithMessage("MinParticipants must be less than or equal to 10");

        RuleFor(x => x.MaxParticipants)
            .GreaterThanOrEqualTo(1)
                .WithMessage("MaxParticipants must be greater than or equal to 1")
            .LessThanOrEqualTo(10)
                .WithMessage("MaxParticipants must be less than or equal to 10");

        RuleFor(x => x)
            .Must(x => x.MinParticipants <= x.MaxParticipants)
                .WithMessage("MinParticipants must be less than or equal to MaxParticipants");
    }

    public CreateExpeditionCommandValidator()
    {
        InitialiseRules();
    }
}