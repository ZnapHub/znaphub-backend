using Microsoft.Extensions.Configuration;
using Minio;

namespace EventFlow.Infrastructure.Storage;

public sealed class MinioStorageService : IStorageService
{
    private readonly IMinioClient _client;
    private readonly string _bucket;

    public MinioStorageService(IMinioClient client, string bucket)
    {
        _client = client;
        _bucket = bucket;
    }

    public Task UploadAsync(
        string objectName,
        Stream data,
        string contentType,
        CancellationToken ct = default
    )
    {
        throw new NotImplementedException();
    }
}
