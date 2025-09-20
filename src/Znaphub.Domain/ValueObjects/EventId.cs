namespace ZnapHub.Domain.ValueObjects;

public sealed record EventId(Guid Value)
{
    public static EventId New() => new(Guid.NewGuid());

    public static EventId FromString(string value) => new(Guid.Parse(value.Trim()));

    public static EventId FromGuid(Guid value) => new(value);

    public static implicit operator string(EventId eventId) => eventId.ToString();
}
