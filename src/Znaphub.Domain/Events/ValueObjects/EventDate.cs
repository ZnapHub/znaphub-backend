namespace ZnapHub.Domain.Events.ValueObjects;

public sealed record EventDate(DateTimeOffset Value)
{
    public static implicit operator DateTimeOffset(EventDate d) => d.Value;
}
