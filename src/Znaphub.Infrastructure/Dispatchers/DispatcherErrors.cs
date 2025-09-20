using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Infrastructurez.Dispatchers;

internal static class DispatcherErrors
{
    internal static Error QueryExecutionError(Exception ex) =>
        new($"QueryExecutionError: {ex.Message}");

    internal static Error CommandExecutionError(Exception ex) =>
        new($"CommandExecutionError: {ex.Message}");
};
