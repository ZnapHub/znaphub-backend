namespace ZnapHub.Domain.Events.ValueObjects;

public sealed record EventDate
{
    public DateTimeOffset Value { get; }

    private EventDate(DateTimeOffset value) => Value = value;

    public static EventDate FromDateTimeOffset(DateTimeOffset dateTimeOffset) =>
        new(dateTimeOffset);

    public static implicit operator DateTimeOffset(EventDate d) => d.Value;
}
