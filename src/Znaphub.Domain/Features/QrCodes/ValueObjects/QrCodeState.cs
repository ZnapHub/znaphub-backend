namespace ZnapHub.Domain.Features.QrCodes.ValueObjects;

public abstract record QrCodeState
{
    private QrCodeState() { }

    public sealed record Active(DateTimeOffset? ExpiresAt, int MaxUploads, int UploadCount)
        : QrCodeState;

    public sealed record Expired(DateTimeOffset ExpiredAt, int MaxUploads, int UploadCount)
        : QrCodeState;

    public sealed record Deactivated(int MaxUploads, int UploadCount) : QrCodeState;
}
