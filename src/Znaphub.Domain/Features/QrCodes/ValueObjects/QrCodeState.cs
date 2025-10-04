namespace ZnapHub.Domain.Features.QrCodes.ValueObjects;

public abstract record QrCodeState
{
    private QrCodeState() { }

    public sealed record Active(DateTimeOffset? ExpiresAt, int MaxUploads, int UploadCount)
        : QrCodeState;

    public sealed record Expired(DateTimeOffset ExpiredAt) : QrCodeState;

    public sealed record Deactivated : QrCodeState;
}
