using Znaphub.Sharedz.Messages;

namespace Znaphub.Sharedz.Abstractions;

public sealed record Error(string Description)
{
    public static readonly Error None = new(string.Empty);
    public static readonly Error NullValue = new(ErrorMessages.NullValue.Text);

    public static implicit operator Result(Error error) => Result.Failure(error);

    public Result ToResult() => Result.Failure(this);
}
