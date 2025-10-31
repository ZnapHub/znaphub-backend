using ZnapHub.Domain.Features.QrCodes.Repositories;
using ZnapHub.Domain.Features.QrCodes.Services;
using ZnapHub.Domain.Features.QrCodes.ValueObjects;
using ZnapHub.Infrastructure.Features.QrCodes.Messages;

namespace ZnapHub.Infrastructure.Features.QrCodes.Services;

internal sealed class ShortIdGenerator : IShortIdGenerator
{
    private readonly IQrCodeReadRepository _repository;

    public ShortIdGenerator(IQrCodeReadRepository repository) => _repository = repository;

    public async Task<ShortId> GenerateUniqueAsync(CancellationToken ct = default)
    {
        const string alphabet = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";

        for (var i = 0; i < 10; i++)
        {
            var id = ShortId.FromString(
                string.Create(
                    8,
                    alphabet,
                    (span, chars) =>
                    {
                        for (var j = 0; j < span.Length; j++)
                            span[j] = chars[Random.Shared.Next(chars.Length)];
                    }
                )
            );

            if (await _repository.GetByShortIdAsync(id, ct) is null)
                return id;
        }

        throw new InvalidOperationException(QrCodesErrorMessages.GenerateShortIdFailed);
    }
}
