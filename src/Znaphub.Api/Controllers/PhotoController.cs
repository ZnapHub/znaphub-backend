using System.Net;
using Microsoft.AspNetCore.Mvc;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Application.Features.Photos.Commands.UploadPhoto;
using ZnapHub.Application.Features.Photos.Dtos;
using ZnapHub.Application.Features.Photos.Queries.GetPhotosByEvent;

namespace ZnapHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PhotoController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public PhotoController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> UploadAsync([FromForm] UploadPhotoCommand command)
    {
        var result = await _commandDispatcher.DispatchAsync(command);
        return result.Match<IActionResult>(
            Ok,
            e =>
                e switch
                {
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, e),
                }
        );
    }

    [HttpGet("{eventId:guid}")]
    public async Task<IActionResult> GetListAsync(Guid eventId)
    {
        var result = await _queryDispatcher.QueryAsync<
            GetPhotosByEventQuery,
            IReadOnlyList<PhotoDto>
        >(new GetPhotosByEventQuery(eventId));

        return result.Match(
            Ok,
            e =>
                e switch
                {
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, e),
                }
        );
    }
}
