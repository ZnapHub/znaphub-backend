namespace ZnapHub.Domain.ValueObjects;

public sealed record FileName
{
    public string Value { get; }

    private FileName(string value) => Value = value;

    public static FileName FromString(string value) => new(value.Trim());

    public override string ToString() => Value;

    public static implicit operator string(FileName fileName) => fileName.Value.Trim();
};
