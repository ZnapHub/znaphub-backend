using EventFlow.Application.Abstractions.Storage;
using Minio;
using Minio.DataModel.Args;

namespace EventFlow.Infrastructure.Storage;

internal sealed class MinioStorageService : IStorageService
{
    private readonly IMinioClient _client;
    private readonly string _bucket;

    public MinioStorageService(IMinioClient client, string bucket)
    {
        _client = client;
        _bucket = bucket;
    }

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

    public async Task<string> GetUrlAsync(string objectName)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(_bucket)
            .WithObject(objectName)
            .WithExpiry((int)TimeSpan.FromDays(7).TotalSeconds);

        return await _client.PresignedGetObjectAsync(args);
    }
}
