using EventFlow.Shared.Abstractions;

namespace EventFlow.Application.Abstractions.Messaging.Commands;

public interface ICommandHandler<in TCommand>
    where TCommand : class, ICommand
{
    Task<Result> HandleAsync(TCommand command);
}
