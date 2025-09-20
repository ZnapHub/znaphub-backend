using ZnapHub.Applicationz.Abstractions.Data;
using ZnapHub.Applicationz.Abstractions.Messaging.Commands;
using ZnapHub.Applicationz.Abstractions.Storage;
using ZnapHub.Domain.Photos;
using ZnapHub.Domain.ValueObjects;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Applicationz.Photos.UploadPhoto;

internal sealed class UploadPhotoCommandHandler : ICommandHandler<UploadPhotoCommand>
{
    private readonly IStorageService _storageService;
    private readonly IPhotoWriteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadPhotoCommandHandler(
        IStorageService storageService,
        IPhotoWriteRepository repository,
        IUnitOfWork unitOfWork
    )
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
    }

    public async Task<Result> HandleAsync(UploadPhotoCommand command)
    {
        var eventId = EventId.FromGuid(command.EventId);
        var photoId = PhotoId.New();
        var fileName = command.File.FileName;
        var objectName = ObjectName.ForEvent(eventId, photoId, fileName);

        await using var stream = command.File.OpenReadStream();
        await _storageService.UploadAsync(objectName, stream, command.File.ContentType);

        var url = await _storageService.GetUrlAsync(objectName);

        var photo = Photo.Create(
            eventId,
            FileName.FromString(fileName),
            objectName,
            PhotoUrl.FromString(url)
        );

        await _repository.AddAsync(photo);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
