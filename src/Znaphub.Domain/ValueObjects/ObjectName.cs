using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.Photos.ValueObjects;

namespace ZnapHub.Domain.ValueObjects;

public sealed record ObjectName
{
    private string Value { get; }

    private ObjectName(string value) => Value = value;

    public static ObjectName FromString(string value) => new(value.Trim());

    public override string ToString() => Value;

    public static implicit operator string(ObjectName objectName) => objectName.ToString();
}
