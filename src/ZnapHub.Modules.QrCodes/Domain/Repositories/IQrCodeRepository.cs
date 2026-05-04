using ZnapHub.Modules.QrCodes.Domain.Entities;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;

namespace ZnapHub.Modules.QrCodes.Domain.Repositories;

public interface IQrCodeRepository
{
    Task AddAsync(QrCode qrCode, CancellationToken ct = default);

    Task<QrCode?> GetByIdAsync(QrCodeId id, CancellationToken ct = default);

    Task<QrCode?> GetByShortIdAsync(ShortId shortId, CancellationToken ct = default);

    Task UpdateAsync(QrCode qrCode, CancellationToken ct = default);
}
