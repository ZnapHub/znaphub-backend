using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Application.Features.Photos.Dtos;
using ZnapHub.Application.Features.Photos.Mappers;
using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.Photos.Interfaces;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Features.Photos.Queries.GetPhotosByEvent;

public sealed class GetPhotosByEventQueryHandler
    : IQueryHandler<GetPhotosByEventQuery, IReadOnlyList<PhotoDto>>
{
    private readonly IPhotoReadRepository _photoReadRepository;

    public GetPhotosByEventQueryHandler(IPhotoReadRepository photoReadRepository) =>
        _photoReadRepository = photoReadRepository;

    public async Task<Result<IReadOnlyList<PhotoDto>>> HandleAsync(GetPhotosByEventQuery query)
    {
        var eventId = EventId.FromGuid(query.EventId);
        var photos = await _photoReadRepository.GetByEventAsync(eventId);
        return photos.Select(p => p.ToDto()).ToList();
    }
}
