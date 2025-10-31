using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Abstractions.Messaging.Commands;

public interface ICommandDispatcher
{
    Task<Result> DispatchAsync<TCommand>(TCommand command, CancellationToken ct = default)
        where TCommand : class, ICommand;

    Task<Result<TResult>> DispatchAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken ct = default
    )
        where TCommand : class, ICommand;
}
