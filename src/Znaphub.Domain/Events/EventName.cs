namespace ZnapHub.Domain.Events;

public sealed record EventName(string Value)
{
    public static EventName FromString(string value) => new(value.Trim());

    public override string ToString() => Value;

    public static implicit operator string(EventName eventName) => eventName.Value;
};
