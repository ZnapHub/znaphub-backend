namespace ZnapHub.Modules.Events.Domain.ValueObjects;

public sealed record EventName
{
    public string Value { get; }

    private EventName(string value) => Value = value;

    public static EventName FromString(string value) => new(value.Trim());

    public override string ToString() => Value;

    public static implicit operator string(EventName eventName) => eventName.Value;
}
