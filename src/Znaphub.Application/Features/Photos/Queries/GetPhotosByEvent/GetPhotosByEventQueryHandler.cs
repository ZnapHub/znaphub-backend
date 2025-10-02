using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Application.Abstractions.Storage;
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
    private readonly IPhotoUrlService _urlService;

    public GetPhotosByEventQueryHandler(
        IPhotoReadRepository photoReadRepository,
        IPhotoUrlService urlService
    ) => (_photoReadRepository, _urlService) = (photoReadRepository, urlService);

    public async Task<Result<IReadOnlyList<PhotoDto>>> HandleAsync(GetPhotosByEventQuery query)
    {
        var eventId = EventId.FromGuid(query.EventId);
        var photos = await _photoReadRepository.GetByEventAsync(eventId);
        var photoTasks = photos
            .Select(async p =>
            {
                var url = await _urlService.GetUrlAsync(p.ObjectName);
                return p.ToDto(url);
            })
            .ToList();

        return await Task.WhenAll(photoTasks);
    }
}
