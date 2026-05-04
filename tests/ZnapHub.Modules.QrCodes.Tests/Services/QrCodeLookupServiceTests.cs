using Shouldly;
using ZnapHub.Modules.QrCodes.Domain.Entities;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;
using ZnapHub.Modules.QrCodes.Services;
using ZnapHub.Modules.QrCodes.Tests.Fakes;

namespace ZnapHub.Modules.QrCodes.Tests.Services;

public class QrCodeLookupServiceTests
{
    [Fact]
    public async Task GetByShortIdAsync_ReturnsNull_WhenNotFound()
    {
        var service = new QrCodeLookupService(new FakeQrCodeRepository());

        var result = await service.GetByShortIdAsync("missing");

        result.ShouldBeNull();
    }

    [Fact]
    public async Task GetByShortIdAsync_ReturnsEventIdAndCanUpload_WhenFoundAndActive()
    {
        var repo = new FakeQrCodeRepository();
        var eventId = Guid.CreateVersion7();
        await repo.AddAsync(QrCode.Create(ShortId.FromString("abc123"), eventId, maxUploads: 5));
        var service = new QrCodeLookupService(repo);

        var result = await service.GetByShortIdAsync("abc123");

        result.ShouldNotBeNull();
        result.EventId.ShouldBe(eventId);
        result.CanUpload.ShouldBeTrue();
    }

    [Fact]
    public async Task GetByShortIdAsync_NormalizesCaseToMatchStoredShortId()
    {
        var repo = new FakeQrCodeRepository();
        await repo.AddAsync(
            QrCode.Create(ShortId.FromString("xyz789"), Guid.CreateVersion7(), maxUploads: 0)
        );
        var service = new QrCodeLookupService(repo);

        var result = await service.GetByShortIdAsync("  XYZ789  ");

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetByShortIdAsync_ReturnsCanUploadFalse_WhenDeactivated()
    {
        var repo = new FakeQrCodeRepository();
        var qr = QrCode.Create(ShortId.FromString("dead00"), Guid.CreateVersion7(), maxUploads: 0);
        qr.Deactivate();
        await repo.AddAsync(qr);
        var service = new QrCodeLookupService(repo);

        var result = await service.GetByShortIdAsync("dead00");

        result.ShouldNotBeNull();
        result.CanUpload.ShouldBeFalse();
    }
}
