using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Abstractions.Messaging.Queries;

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : class, IQuery<TResult>
{
    Task<Result<TResult>> HandleAsync(TQuery query);
}
