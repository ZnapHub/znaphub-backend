namespace EventFlow.Domain.ValueObjects.Photos;

public sealed record PhotoId(Guid Value)
{
    public static PhotoId New() => new(Guid.NewGuid());

    public static PhotoId FromString(string value) => new(Guid.Parse(value));

    public static PhotoId FromGuid(Guid value) => new(value);

    public override string ToString() => Value.ToString();

    public static implicit operator string(PhotoId photoId) => photoId.ToString();
}
