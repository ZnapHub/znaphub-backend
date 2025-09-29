using Minio;
using Minio.DataModel.Args;
using ZnapHub.Application.Abstractions.Storage;

namespace ZnapHub.Infrastructure.Storage.Providers;

public sealed class MinioUrlService : IUrlService
{
    private readonly IMinioClient _client;
    private readonly string _bucket;

    public MinioUrlService(IMinioClient client, string bucket) =>
        (_client, _bucket) = (client, bucket);

    public async Task<string> GetUrlAsync(string objectName, TimeSpan? expiry = null)
    {
        var expiryTimespan = expiry ?? TimeSpan.FromMinutes(7);

        var args = new PresignedGetObjectArgs()
            .WithBucket(_bucket)
            .WithObject(objectName)
            .WithExpiry((int)expiryTimespan.TotalSeconds);

        return await _client.PresignedGetObjectAsync(args);
    }
}
