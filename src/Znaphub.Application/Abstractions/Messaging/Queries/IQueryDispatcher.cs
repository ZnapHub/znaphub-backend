using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Abstractions.Messaging.Queries;

public interface IQueryDispatcher
{
    Task<Result<TResult>> QueryAsync<TQuery, TResult>(TQuery query)
        where TQuery : class, IQuery<TResult>;
}
