using EventFlow.Application.Abstractions.Data;
using EventFlow.Application.Abstractions.Messaging.Commands;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Repositories;
using EventFlow.Domain.ValueObjects;

namespace EventFlow.Application.Photos.UploadPhoto;

internal sealed class UploadPhotoCommandHandler : ICommandHandler<UploadPhotoCommand>
{
    private readonly IPhotoWriteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadPhotoCommandHandler(IPhotoWriteRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task HandleAsync(UploadPhotoCommand command)
    {
        var photo = Photo.Create(new EventId(""), "", "", "");

        await _repository.AddAsync(photo);
        await _unitOfWork.SaveChangesAsync();
    }
}
