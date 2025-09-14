namespace EventFlow.Domain.ValueObjects;

public sealed record EventId(string Value)
{
    public static EventId New() => new(Guid.NewGuid().ToString());
    public override string ToString() => Value;
}