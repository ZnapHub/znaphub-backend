namespace EventFlow.Domain.ValueObjects;

public sealed record PhotoUrl(string Value)
{
    public static PhotoUrl Empty() => new(string.Empty);

    public override string ToString() => Value;

    public static implicit operator string(PhotoUrl photoUrl) => photoUrl.Value;
}
