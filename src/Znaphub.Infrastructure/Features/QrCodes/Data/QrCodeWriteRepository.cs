using ZnapHub.Domain.Features.QrCodes.Entities;
using ZnapHub.Domain.Features.QrCodes.Repositories;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Infrastructure.Features.QrCodes.Data;

internal sealed class QrCodeWriteRepository : IQrCodeWriteRepository
{
    private readonly ZnapHubWriteContext _db;

    public QrCodeWriteRepository(ZnapHubWriteContext db) => _db = db;

    public async Task AddAsync(QrCode qrCode, CancellationToken ct = default) =>
        await _db.AddAsync(qrCode.ToEntity(), ct);
}
