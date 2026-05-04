using ZnapHub.Modules.QrCodes.Domain.Entities;
using ZnapHub.Modules.QrCodes.Domain.Repositories;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;

namespace ZnapHub.Modules.QrCodes.Tests.Fakes;

internal sealed class FakeQrCodeRepository : IQrCodeRepository
{
    private readonly List<QrCode> _store = [];

    public IReadOnlyList<QrCode> Stored => _store;

    public Task AddAsync(QrCode qrCode, CancellationToken ct = default)
    {
        _store.Add(qrCode);
        return Task.CompletedTask;
    }

    public Task<QrCode?> GetByIdAsync(QrCodeId id, CancellationToken ct = default) =>
        Task.FromResult<QrCode?>(_store.FirstOrDefault(q => q.Id == id));

    public Task<QrCode?> GetByShortIdAsync(ShortId shortId, CancellationToken ct = default) =>
        Task.FromResult<QrCode?>(
            _store.FirstOrDefault(q => string.Equals(q.ShortId, shortId, StringComparison.Ordinal))
        );

    public Task UpdateAsync(QrCode qrCode, CancellationToken ct = default)
    {
        var index = _store.FindIndex(q => q.Id == qrCode.Id);
        if (index >= 0)
            _store[index] = qrCode;
        return Task.CompletedTask;
    }
}
