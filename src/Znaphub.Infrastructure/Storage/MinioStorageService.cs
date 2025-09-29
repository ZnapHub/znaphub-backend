using Minio;
using Minio.DataModel.Args;
using ZnapHub.Application.Abstractions.Storage;

namespace ZnapHub.Infrastructure.Storage;

internal sealed class MinioStorageService : IStorageService
{
    private readonly IMinioClient _client;
    private readonly string _bucket;

    public MinioStorageService(IMinioClient client, string bucket) =>
        (_client, _bucket) = (client, bucket);

    public async Task UploadAsync(
        string objectName,
        Stream data,
        string contentType,
        CancellationToken ct = default
    )
    {
        var args = new PutObjectArgs()
            .WithBucket(_bucket)
            .WithObject(objectName)
            .WithStreamData(data)
            .WithObjectSize(data.Length)
            .WithContentType(contentType);

        await _client.PutObjectAsync(args, ct);
    }
}
