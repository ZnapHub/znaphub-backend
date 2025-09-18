namespace EventFlow.Domain.ValueObjects;

public sealed record EventId(string Value)
{
    public static EventId New() => new(Guid.NewGuid().ToString());

    public static EventId FromString(string value) => new(value.Trim());

    public override string ToString() => Value;

    public static implicit operator string(EventId eventId) => eventId.ToString();
}
