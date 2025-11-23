using Microsoft.Extensions.Options;
using ZnapHub.Domain.Features.QrCodes.Factories;
using ZnapHub.Shared.Options;

namespace ZnapHub.Infrastructure.Features.QrCodes.Factories;

internal sealed class QrCodeUrlFactory : IQrCodeUrlFactory
{
    private readonly StorageOptions _options;

    public QrCodeUrlFactory(IOptions<StorageOptions> options)
    {
        _options = options.Value;
    }

    public Uri CreateUploadUrl(string shortId) => new(new Uri(_options.BaseUploadUrl), shortId);
}
