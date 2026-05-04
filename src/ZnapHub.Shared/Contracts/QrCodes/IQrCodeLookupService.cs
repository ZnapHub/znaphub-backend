namespace ZnapHub.Shared.Contracts.QrCodes;

public interface IQrCodeLookupService
{
    Task<QrCodeLookupResult?> GetByShortIdAsync(string shortId, CancellationToken ct = default);
}
