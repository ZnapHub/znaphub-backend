using Microsoft.Extensions.Options;
using Shouldly;
using ZnapHub.Modules.QrCodes.Services;
using ZnapHub.Shared.Options;

namespace ZnapHub.Modules.QrCodes.Tests.Services;

public class QrCodeUrlFactoryTests
{
    [Fact]
    public void CreateUploadUrl_AppendsShortIdToBaseUploadUrl()
    {
        var factory = new QrCodeUrlFactory(
            Options.Create(
                new StorageOptions
                {
                    BaseUploadUrl = "https://upload.znap.example/u/",
                    DefaultMaxUploads = 0,
                    DefaultSizeLimit = 0,
                }
            )
        );

        var url = factory.CreateUploadUrl("abc123");

        url.AbsoluteUri.ShouldBe("https://upload.znap.example/u/abc123");
    }
}
