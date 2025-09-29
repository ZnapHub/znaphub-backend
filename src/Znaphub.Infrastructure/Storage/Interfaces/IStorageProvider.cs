namespace ZnapHub.Infrastructure.Storage.Interfaces;

public interface IStorageService
{
    Task UploadAsync(
        string bucketName,
        string objectName,
        Stream data,
        string contentType,
        CancellationToken ct = default
    );
}
