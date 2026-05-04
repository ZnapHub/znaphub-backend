using ZnapHub.Modules.QrCodes.Services;

namespace ZnapHub.Modules.QrCodes.Tests.Fakes;

internal sealed class FakeQrCodeUrlFactory : IQrCodeUrlFactory
{
    public Uri CreateUploadUrl(string shortId) => new($"https://test.local/u/{shortId}");
}
