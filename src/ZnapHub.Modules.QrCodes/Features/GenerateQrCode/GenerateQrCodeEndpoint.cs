using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ZnapHub.Modules.QrCodes.Features.GenerateQrCode;

internal static class GenerateQrCodeEndpoint
{
    extension(IEndpointRouteBuilder routes)
    {
        public IEndpointRouteBuilder MapGenerateQrCode()
        {
            routes
                .MapPost(
                    "/qrcodes",
                    async (
                        GenerateQrCodeCommand command,
                        GenerateQrCodeHandler handler,
                        CancellationToken ct
                    ) =>
                    {
                        var result = await handler.HandleAsync(command, ct);
                        return result.Match<IResult>(
                            Results.Ok,
                            _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
                        );
                    }
                )
                .RequireAuthorization();

            return routes;
        }
    }
}
