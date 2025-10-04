namespace ZnapHub.Domain.Features.Events.ValueObjects;

public sealed record EventId
{
    public Guid Value { get; }

    private EventId(Guid value) => Value = value;

    public static EventId New() => new(Guid.NewGuid());

    public static EventId FromString(string value) => new(Guid.Parse(value.Trim()));

    public static EventId FromGuid(Guid value) => new(value);

    public static implicit operator Guid(EventId eventId) => eventId.Value;

    public static implicit operator EventId(Guid value) => FromGuid(value);
}
