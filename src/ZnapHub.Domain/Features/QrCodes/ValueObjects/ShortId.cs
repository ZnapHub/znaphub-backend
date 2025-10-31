namespace ZnapHub.Domain.Features.QrCodes.ValueObjects;

public sealed record ShortId
{
    private string Value { get; }

    private ShortId(string value) => Value = value;

    public static ShortId FromString(string value) => new(value.ToLowerInvariant().Trim());

    public override string ToString() => Value;

    public static implicit operator string(ShortId value) => value.Value;

    public static implicit operator ShortId(string value) => FromString(value);
}
