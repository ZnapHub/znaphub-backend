namespace ZnapHub.Infrastructure.Storage.Interfaces;

internal interface IStorageProvider
{
    Task UploadAsync(
        string bucketName,
        string objectName,
        Stream data,
        string contentType,
        CancellationToken ct = default
    );
}
