using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Abstractions.Messaging.Commands;

public interface ICommandHandler<in TCommand>
    where TCommand : class, ICommand
{
    Task<Result> HandleAsync(TCommand command);
}

public interface ICommandHandler<in TCommand, TResult>
    where TCommand : class, ICommand
{
    Task<Result<TResult>> HandleAsync(TCommand command);
}
