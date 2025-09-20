using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Applicationz.Abstractions.Messaging.Queries;

public interface IQueryDispatcher
{
    Task<Result<TResult>> QueryAsync<TQuery, TResult>(TQuery query)
        where TQuery : class, IQuery<TResult>;
}
