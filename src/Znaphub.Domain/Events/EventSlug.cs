namespace ZnapHub.Domain.Events;

public sealed record EventSlug(string Value)
{
    public static EventSlug FromString(string value) => new(value.ToLowerInvariant().Trim());

    public override string ToString() => Value;

    public static implicit operator string(EventSlug eventSlug) => eventSlug.Value;
};
