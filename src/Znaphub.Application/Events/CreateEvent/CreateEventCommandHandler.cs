using ZnapHub.Application.Abstractions.Data;
using ZnapHub.Application.Abstractions.Identity;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Domain.Events.Entities;
using ZnapHub.Domain.Events.Interfaces;
using ZnapHub.Domain.Events.ValueObjects;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler : ICommandHandler<CreateEventCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IEventWriteRepository _eventWriteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEventCommandHandler(
        ICurrentUserService currentUserService,
        IEventWriteRepository eventWriteRepository,
        IUnitOfWork unitOfWork
    )
    {
        _currentUserService = currentUserService;
        _eventWriteRepository = eventWriteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(CreateEventCommand command)
    {
        var userId = _currentUserService.UserId;
        if (userId is null)
            return Result.Failure(Error.NullValue);

        var @event = Event.Create(
            OrganizerId.FromGuid(userId.Value),
            EventName.FromString(command.EventName),
            EventSlug.FromString(command.EventSlug),
            EventVisibility.FromBool(command.IsPublic),
            EventDescription.FromString(command.Description)
        );

        await _eventWriteRepository.AddAsync(@event);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
