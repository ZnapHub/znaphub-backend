using Amazon.S3;
using Amazon.S3.Model;
using ZnapHub.Infrastructure.Storage.Interfaces;

namespace ZnapHub.Infrastructure.Storage.Providers;

internal sealed class S3StorageProvider : IStorageProvider
{
    private readonly IAmazonS3 _s3Client;

    public S3StorageProvider(IAmazonS3 s3Client) => _s3Client = s3Client;

    public async Task UploadAsync(
        string bucketName,
        string objectName,
        Stream data,
        string contentType,
        CancellationToken ct = default
    )
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = objectName,
            InputStream = data,
            ContentType = contentType,
        };

        await _s3Client.PutObjectAsync(putRequest, ct);
    }
}
