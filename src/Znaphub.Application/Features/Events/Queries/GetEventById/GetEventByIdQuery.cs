using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Application.Features.Events.Dtos;

namespace ZnapHub.Application.Features.Events.Queries.GetEventById;

public sealed record GetEventByIdQuery(Guid EventId) : IQuery<EventDto>;
