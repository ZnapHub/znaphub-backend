using EventFlow.Shared.Messages;

namespace EventFlow.Shared.Abstractions;

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new(
        ErrorMessages.NullValue.Code,
        ErrorMessages.NullValue.Text
    );

    public static implicit operator Result(Error error) => Result.Failure(error);

    public Result ToResult() => Result.Failure(this);
}
