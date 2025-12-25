namespace TwistedTaleweaver.Core.Commands;

public interface ICommandHandler<TCommand, TResult>
{
    Task<TResult> ExecuteAsync(TCommand command);
}

public interface ICommandHandler<TCommand>
{
    Task ExecuteAsync(TCommand command);
}