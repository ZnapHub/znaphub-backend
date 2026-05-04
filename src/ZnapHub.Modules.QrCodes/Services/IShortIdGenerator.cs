using ZnapHub.Modules.QrCodes.Domain.ValueObjects;

namespace ZnapHub.Modules.QrCodes.Services;

internal interface IShortIdGenerator
{
    Task<ShortId> GenerateUniqueAsync(CancellationToken ct = default);
}
