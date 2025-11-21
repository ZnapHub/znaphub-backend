using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Application.Abstractions.Messaging.Queries;
using ZnapHub.Application.Features.Photos.Commands.UploadPhoto;
using ZnapHub.Application.Features.Photos.Dtos;
using ZnapHub.Application.Features.Photos.Queries.GetPhotosByEvent;

namespace ZnapHub.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public PhotosController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{shortId}")]
    public async Task<IActionResult> UploadAsync(
        string shortId,
        IFormFile file,
        CancellationToken ct
    )
    {
        await using var stream = file.OpenReadStream();
        var result = await _commandDispatcher.DispatchAsync(
            new UploadPhotoCommand(
                shortId,
                new PhotoContentDto(file.FileName, stream, file.ContentType)
            ),
            ct
        );
        return result.Match<IActionResult>(
            Created,
            e =>
                e switch
                {
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, e),
                }
        );
    }
}
