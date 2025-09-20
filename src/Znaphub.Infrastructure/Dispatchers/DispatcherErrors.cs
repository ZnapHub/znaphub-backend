using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Infrastructure.Dispatchers;

internal static class DispatcherErrors
{
    internal static Error QueryExecutionError(Exception ex) =>
        new($"QueryExecutionError: {ex.Message}");

    internal static Error CommandExecutionError(Exception ex) =>
        new($"CommandExecutionError: {ex.Message}");
};
