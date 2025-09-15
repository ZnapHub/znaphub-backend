namespace EventFlow.Application.Abstractions.Messaging.Commands;

public interface ICommandDispatcher
{
    Task DispatchAsync<TCommand>(TCommand command)
        where TCommand : class, ICommand;
}
