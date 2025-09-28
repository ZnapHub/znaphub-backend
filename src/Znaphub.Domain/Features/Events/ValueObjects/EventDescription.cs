namespace ZnapHub.Domain.Features.Events.ValueObjects;

public sealed record EventDescription
{
    public string Value { get; }

    private EventDescription(string value) => Value = value;

    public static EventDescription Empty => new(string.Empty);

    public static EventDescription? FromString(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : new(value.Trim());

    public override string ToString() => Value;

    public static implicit operator string(EventDescription eventDescription) =>
        eventDescription.Value;
}
