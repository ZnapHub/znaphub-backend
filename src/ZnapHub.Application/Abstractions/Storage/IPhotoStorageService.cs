namespace ZnapHub.Application.Abstractions.Storage;

public interface IPhotoStorageService
{
    Task UploadAsync(
        string objectName,
        Stream data,
        string contentType,
        CancellationToken ct = default
    );
}
