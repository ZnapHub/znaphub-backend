using ZnapHub.Shared.Messages;

namespace ZnapHub.Shared.Abstractions;

public sealed record Error(string Description)
{
    public static readonly Error None = new(string.Empty);
    public static readonly Error NullValue = new(ErrorMessages.NullValue);
    public static readonly Error NotFound = new(ErrorMessages.NotFound);

    public static implicit operator Result(Error error) => Result.Failure(error);

    public Result ToResult() => Result.Failure(this);
}
