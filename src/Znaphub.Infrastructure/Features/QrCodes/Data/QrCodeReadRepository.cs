using ZnapHub.Domain.Features.QrCodes.Entities;
using ZnapHub.Domain.Features.QrCodes.Repositories;
using ZnapHub.Domain.Features.QrCodes.ValueObjects;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Features.QrCodes.Data;

internal sealed class QrCodeReadRepository : IQrCodeReadRepository
{
    private readonly ZnapHubReadContext _db;

    public QrCodeReadRepository(ZnapHubReadContext db) => _db = db;

    public async Task<QrCode?> GetAsync(QrCodeId id)
    {
        var qrCode = await _db.QrCodes.FindAsync(id);
        return qrCode.ToDomain();
    }
}
