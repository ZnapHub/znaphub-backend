using Minio;
using Minio.DataModel.Args;
using ZnapHub.Infrastructure.Storage.Interfaces;

namespace ZnapHub.Infrastructure.Storage.Providers;

internal sealed class MinioStorageService : IStorageService
{
    private readonly IMinioClient _client;

    public MinioStorageService(IMinioClient client) => _client = client;

    public async Task UploadAsync(
        string bucketName,
        string objectName,
        Stream data,
        string contentType,
        CancellationToken ct = default
    )
    {
        var args = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithStreamData(data)
            .WithObjectSize(data.Length)
            .WithContentType(contentType);

        await _client.PutObjectAsync(args, ct);
    }
}
