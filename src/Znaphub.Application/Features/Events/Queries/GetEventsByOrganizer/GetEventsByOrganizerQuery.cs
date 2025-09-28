using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Application.Features.Events.Dtos;

namespace ZnapHub.Application.Features.Events.Queries.GetEventsByOrganizer;

public sealed record GetEventsByOrganizerQuery : IQuery<IReadOnlyList<EventDto>>;
