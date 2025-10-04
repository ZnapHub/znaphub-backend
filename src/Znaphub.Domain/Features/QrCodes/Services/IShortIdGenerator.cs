using ZnapHub.Domain.Features.QrCodes.ValueObjects;

namespace ZnapHub.Domain.Features.QrCodes.Services;

public interface IShortIdGenerator
{
    Task<ShortId> GenerateUniqueAsync(CancellationToken ct = default);
}
