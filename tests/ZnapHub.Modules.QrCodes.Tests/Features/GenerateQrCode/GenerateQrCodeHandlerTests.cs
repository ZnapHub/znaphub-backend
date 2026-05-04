using Microsoft.Extensions.Options;
using Shouldly;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;
using ZnapHub.Modules.QrCodes.Features.GenerateQrCode;
using ZnapHub.Modules.QrCodes.Tests.Fakes;
using ZnapHub.Shared.Options;

namespace ZnapHub.Modules.QrCodes.Tests.Features.GenerateQrCode;

public class GenerateQrCodeHandlerTests
{
    private static IOptions<StorageOptions> Options(int defaultMaxUploads = 100) =>
        Microsoft.Extensions.Options.Options.Create(
            new StorageOptions
            {
                BaseUploadUrl = "https://test.local/u/",
                DefaultMaxUploads = defaultMaxUploads,
                DefaultSizeLimit = 0,
            }
        );

    [Fact]
    public async Task Handle_PersistsQrCodeAndReturnsUploadUrl()
    {
        var repo = new FakeQrCodeRepository();
        var generator = new FakeShortIdGenerator(ShortId.FromString("abc12345"));
        var handler = new GenerateQrCodeHandler(
            generator,
            new FakeQrCodeUrlFactory(),
            repo,
            Options()
        );

        var eventId = Guid.CreateVersion7();
        var result = await handler.HandleAsync(new GenerateQrCodeCommand(eventId));

        result.IsSuccess.ShouldBeTrue();
        result.Value.UploadUrl.AbsoluteUri.ShouldBe("https://test.local/u/abc12345");

        var stored = repo.Stored.ShouldHaveSingleItem();
        stored.EventId.ShouldBe(eventId);
        stored.ShortId.ToString().ShouldBe("abc12345");
    }

    [Fact]
    public async Task Handle_UsesDefaultMaxUploadsFromStorageOptions()
    {
        var repo = new FakeQrCodeRepository();
        var handler = new GenerateQrCodeHandler(
            new FakeShortIdGenerator(ShortId.FromString("xyz98765")),
            new FakeQrCodeUrlFactory(),
            repo,
            Options(defaultMaxUploads: 42)
        );

        await handler.HandleAsync(new GenerateQrCodeCommand(Guid.CreateVersion7()));

        var stored = repo.Stored.ShouldHaveSingleItem();
        var active = stored.State.ShouldBeOfType<QrCodeState.Active>();
        active.MaxUploads.ShouldBe(42);
    }

    [Fact]
    public async Task Handle_PassesExpiresAtFromCommandToAggregate()
    {
        var repo = new FakeQrCodeRepository();
        var handler = new GenerateQrCodeHandler(
            new FakeShortIdGenerator(ShortId.FromString("expdate1")),
            new FakeQrCodeUrlFactory(),
            repo,
            Options()
        );
        var expiresAt = DateTimeOffset.UtcNow.AddDays(3);

        await handler.HandleAsync(new GenerateQrCodeCommand(Guid.CreateVersion7(), expiresAt));

        var stored = repo.Stored.ShouldHaveSingleItem();
        var active = stored.State.ShouldBeOfType<QrCodeState.Active>();
        active.ExpiresAt.ShouldBe(expiresAt);
    }
}
