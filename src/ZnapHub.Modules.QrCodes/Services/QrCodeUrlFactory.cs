using Microsoft.Extensions.Options;
using ZnapHub.Shared.Options;

namespace ZnapHub.Modules.QrCodes.Services;

internal sealed class QrCodeUrlFactory : IQrCodeUrlFactory
{
    private readonly StorageOptions _options;

    public QrCodeUrlFactory(IOptions<StorageOptions> options) => _options = options.Value;

    public Uri CreateUploadUrl(string shortId) => new(new Uri(_options.BaseUploadUrl), shortId);
}
