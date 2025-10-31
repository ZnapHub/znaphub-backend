using ZnapHub.Application.Abstractions.Data;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Application.Abstractions.Storage;
using ZnapHub.Domain.Features.Photos.Entities;
using ZnapHub.Domain.Features.Photos.Factories;
using ZnapHub.Domain.Features.Photos.Repositories;
using ZnapHub.Domain.Features.Photos.ValueObjects;
using ZnapHub.Domain.Features.QrCodes.Repositories;
using ZnapHub.Domain.ValueObjects;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Features.Photos.Commands.UploadPhoto;

public sealed class UploadPhotoCommandHandler : ICommandHandler<UploadPhotoCommand>
{
    private readonly IPhotoStorageService _storageService;
    private readonly IQrCodeReadRepository _qrCodeReadRepository;
    private readonly IPhotoWriteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadPhotoCommandHandler(
        IPhotoStorageService storageService,
        IQrCodeReadRepository qrCodeReadRepository,
        IPhotoWriteRepository repository,
        IUnitOfWork unitOfWork
    )
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _qrCodeReadRepository =
            qrCodeReadRepository ?? throw new ArgumentNullException(nameof(qrCodeReadRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
    }

    public async Task<Result> HandleAsync(
        UploadPhotoCommand command,
        CancellationToken ct = default
    )
    {
        var qrCode = await _qrCodeReadRepository.GetByShortIdAsync(command.ShortId, ct);
        if (qrCode is null)
            return Error.NotFound;

        var eventId = qrCode.EventId;
        var photoId = PhotoId.New();
        var fileName = command.File.FileName;
        var objectName = ObjectNameFactory.ForEvent(eventId, photoId, fileName);

        await _storageService.UploadAsync(
            objectName,
            command.File.Stream,
            command.File.ContentType,
            ct
        );

        var photo = Photo.Create(eventId, FileName.FromString(fileName), objectName);

        await _repository.AddAsync(photo, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return Result.Success();
    }
}
