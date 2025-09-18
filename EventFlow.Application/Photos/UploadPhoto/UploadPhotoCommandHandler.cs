using EventFlow.Application.Abstractions.Data;
using EventFlow.Application.Abstractions.Messaging.Commands;
using EventFlow.Application.Abstractions.Storage;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Repositories;
using EventFlow.Domain.ValueObjects;

namespace EventFlow.Application.Photos.UploadPhoto;

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

    public async Task HandleAsync(UploadPhotoCommand command)
    {
        var eventId = EventId.FromString(command.EventId);
        var photoId = PhotoId.New();
        var fileName = command.File.FileName;
        var objectName = ObjectName.ForEvent(eventId, photoId, fileName);

        await using var stream = command.File.OpenReadStream();
        await _storageService.UploadAsync(objectName, stream, command.File.ContentType);

        var url = await _storageService.GetUrlAsync(objectName);

        var photo = Photo.Create(eventId, fileName, command.File.ContentType, url);

        await _repository.AddAsync(photo);
        await _unitOfWork.SaveChangesAsync();
    }
}
