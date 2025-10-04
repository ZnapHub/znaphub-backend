using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Abstractions.Messaging.Commands;

public interface ICommandDispatcher
{
    Task<Result> DispatchAsync<TCommand>(TCommand command, CancellationToken ct = default)
        where TCommand : class, ICommand;
}
