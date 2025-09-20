namespace Znaphub.Domainz.Photos;

public sealed record PhotoUrl(string Value)
{
    public static PhotoUrl FromString(string value) => new(value.Trim());

    public override string ToString() => Value;

    public static implicit operator string(PhotoUrl photoUrl) => photoUrl.Value;
}
