using EventFlow.Shared.Abstractions;

namespace EventFlow.Application.Abstractions.Messaging.Commands;

public interface ICommandDispatcher
{
    Task<Result> DispatchAsync<TCommand>(TCommand command)
        where TCommand : class, ICommand;
}
