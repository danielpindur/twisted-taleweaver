namespace TwistedTaleweaver.Core.Queries;

public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> ExecuteAsync(TQuery query);
}