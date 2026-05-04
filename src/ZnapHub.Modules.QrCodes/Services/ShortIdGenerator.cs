using ZnapHub.Modules.QrCodes.Domain.Repositories;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;

namespace ZnapHub.Modules.QrCodes.Services;

internal sealed class ShortIdGenerator : IShortIdGenerator
{
    private const string Alphabet = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";
    private const int Length = 8;
    private const int MaxAttempts = 10;

    private readonly IQrCodeRepository _repository;

    public ShortIdGenerator(IQrCodeRepository repository) => _repository = repository;

    public async Task<ShortId> GenerateUniqueAsync(CancellationToken ct = default)
    {
        for (var i = 0; i < MaxAttempts; i++)
        {
            var candidate = ShortId.FromString(
                string.Create(
                    Length,
                    Alphabet,
                    (span, chars) =>
                    {
                        for (var j = 0; j < span.Length; j++)
                            span[j] = chars[Random.Shared.Next(chars.Length)];
                    }
                )
            );

            if (await _repository.GetByShortIdAsync(candidate, ct) is null)
                return candidate;
        }

        throw new InvalidOperationException(
            "Failed to generate unique short ID after 10 attempts."
        );
    }
}
