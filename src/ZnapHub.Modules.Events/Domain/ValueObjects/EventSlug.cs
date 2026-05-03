namespace ZnapHub.Modules.Events.Domain.ValueObjects;

public sealed record EventSlug
{
    private string Value { get; }

    private EventSlug(string value) => Value = value;

    public static EventSlug FromString(string value) => new(value.Trim().ToLowerInvariant());

    public override string ToString() => Value;
}
