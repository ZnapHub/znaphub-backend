namespace ZnapHub.Domain.Events.ValueObjects;

public sealed record EventSlug
{
    public string Value { get; }

    private EventSlug(string value) => Value = value;

    public static EventSlug FromString(string value) => new(value.ToLowerInvariant().Trim());

    public override string ToString() => Value;

    public static implicit operator string(EventSlug eventSlug) => eventSlug.Value;
};
