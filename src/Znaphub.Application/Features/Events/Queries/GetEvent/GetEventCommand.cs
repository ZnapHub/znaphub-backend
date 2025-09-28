using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Application.Features.Events.Dtos;

namespace ZnapHub.Application.Features.Events.Queries.GetEvent;

public sealed record GetEventCommand(Guid OrganizerId) : IQuery<EventDto>;
