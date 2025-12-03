namespace TwistedTaleweaver.Common.QueryHandlers;

public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> ExecuteAsync(TQuery query);
}