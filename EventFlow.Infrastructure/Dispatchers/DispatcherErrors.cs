using EventFlow.Shared.Abstractions;

namespace EventFlow.Infrastructure.Dispatchers;

internal static class DispatcherErrors
{
    internal static Error QueryExecutionError(Exception ex) =>
        new(DispatcherErrorCodes.QueryExecutionError, ex.Message);

    internal static Error CommandExecutionError(Exception ex) =>
        new(DispatcherErrorCodes.CommandExecutionError, ex.Message);
};
