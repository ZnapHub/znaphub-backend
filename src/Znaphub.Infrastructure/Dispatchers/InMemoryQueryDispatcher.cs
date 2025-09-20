using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Znaphub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Infrastructurez.Dispatchers;

internal sealed class InMemoryQueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<InMemoryQueryDispatcher> _logger;

    public InMemoryQueryDispatcher(
        IServiceProvider serviceProvider,
        ILogger<InMemoryQueryDispatcher> logger
    ) => (_serviceProvider, _logger) = (serviceProvider, logger);

    public async Task<Result<TResult>> QueryAsync<TQuery, TResult>(TQuery query)
        where TQuery : class, IQuery<TResult>
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<
                IQueryHandler<TQuery, TResult>
            >();

            return await handler.HandleAsync(query);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing query {QueryType}", typeof(TQuery).Name);
            return (Result<TResult>)Result.Failure(DispatcherErrors.QueryExecutionError(ex));
        }
    }
}
