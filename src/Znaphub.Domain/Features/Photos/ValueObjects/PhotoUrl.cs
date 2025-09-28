namespace ZnapHub.Domain.Features.Photos.ValueObjects;

public sealed record PhotoUrl
{
    public string Value { get; }

    private PhotoUrl(string value) => Value = value;

    public static PhotoUrl FromString(string value) => new(value.Trim());

    public override string ToString() => Value;

    public static implicit operator string(PhotoUrl photoUrl) => photoUrl.Value;
}
