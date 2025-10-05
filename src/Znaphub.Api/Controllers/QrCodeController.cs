using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Application.Features.QrCodes.Commands;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class QrCodeController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public QrCodeController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] GenerateQrCodeCommand command,
        CancellationToken ct
    )
    {
        var result = await _commandDispatcher.DispatchAsync<
            GenerateQrCodeCommand,
            GenerateQrCodeResponse
        >(command, ct);
        return result.Match<IActionResult>(
            Ok,
            e =>
                e switch
                {
                    not null when e == Error.NotFound => BadRequest(e),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, e),
                }
        );
    }
}
