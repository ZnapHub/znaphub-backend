using Microsoft.EntityFrameworkCore;
using ZnapHub.Modules.QrCodes.Domain.Entities;
using ZnapHub.Modules.QrCodes.Domain.Repositories;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;

namespace ZnapHub.Modules.QrCodes.Data;

internal sealed class QrCodeRepository : IQrCodeRepository
{
    private readonly QrCodesDbContext _db;

    public QrCodeRepository(QrCodesDbContext db) => _db = db;

    public async Task AddAsync(QrCode qrCode, CancellationToken ct = default)
    {
        await _db.QrCodes.AddAsync(qrCode.ToEntity(), ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<QrCode?> GetByIdAsync(QrCodeId id, CancellationToken ct = default)
    {
        var entity = await _db
            .QrCodes.AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == (Guid)id, ct);
        return entity?.ToDomain();
    }

    public async Task<QrCode?> GetByShortIdAsync(ShortId shortId, CancellationToken ct = default)
    {
        string normalized = shortId;
        var entity = await _db
            .QrCodes.AsNoTracking()
            .FirstOrDefaultAsync(q => q.ShortId == normalized, ct);
        return entity?.ToDomain();
    }

    public async Task UpdateAsync(QrCode qrCode, CancellationToken ct = default)
    {
        _db.QrCodes.Update(qrCode.ToEntity());
        await _db.SaveChangesAsync(ct);
    }
}
