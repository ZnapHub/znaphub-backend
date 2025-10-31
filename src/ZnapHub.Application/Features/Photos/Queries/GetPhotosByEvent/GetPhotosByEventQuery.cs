using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Application.Features.Photos.Dtos;

namespace ZnapHub.Application.Features.Photos.Queries.GetPhotosByEvent;

public sealed record GetPhotosByEventQuery(Guid EventId) : IQuery<IReadOnlyList<PhotoDto>>;
