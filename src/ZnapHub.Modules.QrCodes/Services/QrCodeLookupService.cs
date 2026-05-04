using ZnapHub.Modules.QrCodes.Domain.Repositories;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;
using ZnapHub.Shared.Contracts.QrCodes;

namespace ZnapHub.Modules.QrCodes.Services;

internal sealed class QrCodeLookupService : IQrCodeLookupService
{
    private readonly IQrCodeRepository _qrCodes;

    public QrCodeLookupService(IQrCodeRepository qrCodes) => _qrCodes = qrCodes;

    public async Task<QrCodeLookupResult?> GetByShortIdAsync(
        string shortId,
        CancellationToken ct = default
    )
    {
        var qrCode = await _qrCodes.GetByShortIdAsync(ShortId.FromString(shortId), ct);
        return qrCode is null ? null : new QrCodeLookupResult(qrCode.EventId, qrCode.CanUpload());
    }
}
