using Microsoft.AspNetCore.Mvc;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Application.Features.Photos.Commands.UploadPhoto;

namespace ZnapHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PhotoController : ControllerBase
{
    private readonly ICommandDispatcher _dispatcher;

    public PhotoController(ICommandDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> UploadAsync([FromForm] UploadPhotoCommand command)
    {
        await _dispatcher.DispatchAsync(command);
        return Ok();
    }
}
