using EventFlow.Application.Abstractions.Messaging.Commands;
using EventFlow.Application.Photos.UploadPhoto;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Api.Controllers;

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
    public async Task<IActionResult> Upload([FromForm] UploadPhotoCommand command)
    {
        await _dispatcher.DispatchAsync(command);
        return Ok();
    }
}
