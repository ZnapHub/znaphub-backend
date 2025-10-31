using ZnapHub.Application.Abstractions.Storage;
using ZnapHub.Infrastructure.Storage.Interfaces;

namespace ZnapHub.Infrastructure.Features.Photos.Services;

internal sealed class PhotoStorageService : IPhotoStorageService
{
    private readonly IStorageProvider _storageProvider;
    private readonly string _bucket;

    public PhotoStorageService(IStorageProvider storageProvider, string bucket) =>
        (_storageProvider, _bucket) = (storageProvider, bucket);

    public async Task UploadAsync(
        string objectName,
        Stream data,
        string contentType,
        CancellationToken ct = default
    ) => await _storageProvider.UploadAsync(_bucket, objectName, data, contentType, ct);
}
