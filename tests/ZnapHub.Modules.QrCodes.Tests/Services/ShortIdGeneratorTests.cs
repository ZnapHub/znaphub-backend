using Shouldly;
using ZnapHub.Modules.QrCodes.Domain.Entities;
using ZnapHub.Modules.QrCodes.Domain.Repositories;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;
using ZnapHub.Modules.QrCodes.Services;
using ZnapHub.Modules.QrCodes.Tests.Fakes;

namespace ZnapHub.Modules.QrCodes.Tests.Services;

public class ShortIdGeneratorTests
{
    private const string AllowedLowercase = "23456789abcdefghjklmnpqrstuvwxyz";

    [Fact]
    public async Task GenerateUniqueAsync_Produces8CharIdFromAllowedAlphabet()
    {
        var generator = new ShortIdGenerator(new FakeQrCodeRepository());

        var id = await generator.GenerateUniqueAsync();

        var value = id.ToString();
        value.Length.ShouldBe(8);
        value.ShouldAllBe(c => AllowedLowercase.Contains(c));
    }

    [Fact]
    public async Task GenerateUniqueAsync_ProducesUniqueValuesAcrossCalls()
    {
        var generator = new ShortIdGenerator(new FakeQrCodeRepository());

        var ids = new HashSet<string>();
        for (var i = 0; i < 50; i++)
            ids.Add((await generator.GenerateUniqueAsync()).ToString());

        ids.Count.ShouldBeGreaterThan(45);
    }

    [Fact]
    public async Task GenerateUniqueAsync_Throws_WhenAllAttemptsCollide()
    {
        var generator = new ShortIdGenerator(new AlwaysCollidingRepository());

        await Should.ThrowAsync<InvalidOperationException>(() => generator.GenerateUniqueAsync());
    }

    private sealed class AlwaysCollidingRepository : IQrCodeRepository
    {
        public Task AddAsync(QrCode qrCode, CancellationToken ct = default) =>
            throw new NotSupportedException();

        public Task<QrCode?> GetByIdAsync(QrCodeId id, CancellationToken ct = default) =>
            throw new NotSupportedException();

        public Task<QrCode?> GetByShortIdAsync(ShortId shortId, CancellationToken ct = default) =>
            Task.FromResult<QrCode?>(QrCode.Create(shortId, Guid.CreateVersion7(), maxUploads: 0));

        public Task UpdateAsync(QrCode qrCode, CancellationToken ct = default) =>
            throw new NotSupportedException();
    }
}
