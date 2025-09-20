using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Abstractions.Messaging.Commands;

public interface ICommandHandler<in TCommand>
    where TCommand : class, ICommand
{
    Task<Result> HandleAsync(TCommand command);
}
