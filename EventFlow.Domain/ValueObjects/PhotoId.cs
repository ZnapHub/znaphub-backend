namespace EventFlow.Domain.ValueObjects;

public sealed record PhotoId(Guid Value)
{
    public static PhotoId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();

    public static implicit operator string(PhotoId photoId) => photoId.ToString();
}
