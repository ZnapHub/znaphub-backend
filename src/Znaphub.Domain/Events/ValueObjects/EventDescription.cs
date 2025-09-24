namespace ZnapHub.Domain.Events.ValueObjects;

public sealed record EventDescription(string Value)
{
    public static EventDescription Empty => new(string.Empty);

    public static EventDescription FromString(string value) => new(value.Trim());

    public override string ToString() => Value;

    public static implicit operator string(EventDescription eventDescription) =>
        eventDescription.Value;
}
