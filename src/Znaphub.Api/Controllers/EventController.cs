using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Application.Features.Events.Commands.CreateEvent;
using ZnapHub.Application.Features.Events.Dtos;
using ZnapHub.Application.Features.Events.Queries.GetEventsByOrganizer;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public EventController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        var result = await _queryDispatcher.QueryAsync<
            GetEventsByOrganizerQuery,
            IReadOnlyList<EventDto>
        >(new GetEventsByOrganizerQuery());

        return !result.IsSuccess
            ? StatusCode((int)HttpStatusCode.InternalServerError, result.Error)
            : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateEventCommand command)
    {
        var result = await _commandDispatcher.DispatchAsync(command);
        return !result.IsSuccess
            ? StatusCode((int)HttpStatusCode.InternalServerError, result.Error)
            : Ok();
    }
}
