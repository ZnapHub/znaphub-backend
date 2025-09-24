using ZnapHub.Application.Abstractions.Data;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Application.Abstractions.Storage;
using ZnapHub.Domain.Events.ValueObjects;
using ZnapHub.Domain.Photos.Entities;
using ZnapHub.Domain.Photos.Interfaces;
using ZnapHub.Domain.Photos.ValueObjects;
using ZnapHub.Domain.ValueObjects;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Photos.UploadPhoto;

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
