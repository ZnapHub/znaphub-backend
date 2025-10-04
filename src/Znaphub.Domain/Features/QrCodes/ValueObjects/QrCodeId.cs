namespace ZnapHub.Domain.Features.QrCodes.ValueObjects;

public sealed record QrCodeId
{
    private Guid Value { get; }

    private QrCodeId(Guid value) => Value = value;

    public static QrCodeId New() => new(Guid.NewGuid());

    public static QrCodeId FromGuid(Guid value) => new(value);

    public override string ToString() => Value.ToString();

    public static implicit operator Guid(QrCodeId qrCodeId) => qrCodeId.Value;
}
