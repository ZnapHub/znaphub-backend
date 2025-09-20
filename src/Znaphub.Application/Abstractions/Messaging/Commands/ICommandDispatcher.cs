using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Applicationz.Abstractions.Messaging.Commands;

public interface ICommandDispatcher
{
    Task<Result> DispatchAsync<TCommand>(TCommand command)
        where TCommand : class, ICommand;
}
