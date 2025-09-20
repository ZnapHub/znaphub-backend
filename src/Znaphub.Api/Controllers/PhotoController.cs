using Microsoft.AspNetCore.Mvc;
using Znaphub.Application.Abstractions.Messaging.Commands;
using Znaphub.Application.Photos.UploadPhoto;

namespace ZnapHub.Apiz.Controllers;

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
