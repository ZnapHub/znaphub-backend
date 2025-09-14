namespace EventFlow.Domain.ValueObjects;

public sealed record PhotoId(Guid Value)
{
    public static PhotoId New() => new(Guid.NewGuid());
}