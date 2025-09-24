namespace ZnapHub.Domain.Photos.ValueObjects;

public sealed record PhotoId
{
    public Guid Value { get; }

    private PhotoId(Guid value) => Value = value;

    public static PhotoId New() => new(Guid.NewGuid());

    public static PhotoId FromGuid(Guid value) => new(value);

    public override string ToString() => Value.ToString();

    public static implicit operator string(PhotoId photoId) => photoId.ToString();
}
