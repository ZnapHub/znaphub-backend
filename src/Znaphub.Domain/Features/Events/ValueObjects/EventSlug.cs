namespace ZnapHub.Domain.Features.Events.ValueObjects;

public sealed record EventSlug
{
    private string Value { get; }

    private EventSlug(string value) => Value = value;

    public static EventSlug FromString(string value) => new(value.ToLowerInvariant().Trim());

    public override string ToString() => Value;

    public static implicit operator string(EventSlug eventSlug) => eventSlug.Value;

    public static implicit operator EventSlug(string value) => FromString(value);
};
