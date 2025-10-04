using ZnapHub.Application.Abstractions.Storage;
using ZnapHub.Infrastructure.Storage.Interfaces;

namespace ZnapHub.Infrastructure.Features.Photos;

internal sealed class PhotoUrlService : IPhotoUrlService
{
    private readonly IUrlProvider _urlProvider;
    private readonly string _bucket;

    public PhotoUrlService(IUrlProvider urlProvider, string bucket) =>
        (_urlProvider, _bucket) = (urlProvider, bucket);

    public async Task<string> GetUrlAsync(string objectName, TimeSpan? expiry = null) =>
        await _urlProvider.GetUrlAsync(_bucket, objectName, expiry);
}
