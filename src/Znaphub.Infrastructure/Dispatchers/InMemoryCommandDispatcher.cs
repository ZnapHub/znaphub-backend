using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Znaphub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Infrastructurez.Dispatchers;

internal sealed class InMemoryCommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<InMemoryCommandDispatcher> _logger;

    public InMemoryCommandDispatcher(
        IServiceProvider serviceProvider,
        ILogger<InMemoryCommandDispatcher> logger
    ) => (_serviceProvider, _logger) = (serviceProvider, logger);

    public async Task<Result> DispatchAsync<TCommand>(TCommand command)
        where TCommand : class, ICommand
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            return await handler.HandleAsync(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error dispatching command {CommandType}", typeof(TCommand).Name);
            return Result.Failure(DispatcherErrors.CommandExecutionError(ex));
        }
    }
}
