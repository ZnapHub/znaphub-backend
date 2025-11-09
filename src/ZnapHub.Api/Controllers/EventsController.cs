using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Application.Features.Events.Commands.CreateEvent;
using ZnapHub.Application.Features.Events.Dtos;
using ZnapHub.Application.Features.Events.Queries.GetEventById;
using ZnapHub.Application.Features.Events.Queries.GetEventsByOrganizer;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public EventsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync(CancellationToken ct)
    {
        var result = await _queryDispatcher.QueryAsync<
            GetEventsByOrganizerQuery,
            IReadOnlyList<EventDto>
        >(new GetEventsByOrganizerQuery(), ct);

        return result.Match<IActionResult>(
            Ok,
            e =>
                e switch
                {
                    not null when e == Error.NullValue => BadRequest(e),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, e),
                }
        );
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] CreateEventCommand command,
        CancellationToken ct
    )
    {
        var result = await _commandDispatcher.DispatchAsync(command, ct);
        return result.Match<IActionResult>(
            Created,
            e =>
                e switch
                {
                    not null when e == Error.NullValue => BadRequest(e),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, e),
                }
        );
    }

    [HttpGet("{eventId:guid}")]
    public async Task<IActionResult> GetAsync(Guid eventId, CancellationToken ct)
    {
        var result = await _queryDispatcher.QueryAsync<GetEventByIdQuery, EventDto>(
            new GetEventByIdQuery(eventId),
            ct
        );

        return result.Match<IActionResult>(
            Ok,
            e =>
                e switch
                {
                    not null when e == Error.NotFound => NotFound(e),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, e),
                }
        );
    }
}
