namespace EventFlow.Infrastructure.Storage;

public interface IStorageService
{
    Task UploadAsync(
        string objectName,
        Stream data,
        string contentType,
        CancellationToken ct = default
    );
}
