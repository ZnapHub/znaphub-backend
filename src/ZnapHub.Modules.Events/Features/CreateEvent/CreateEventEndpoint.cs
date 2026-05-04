using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Modules.Events.Features.CreateEvent;

internal static class CreateEventEndpoint
{
    extension(IEndpointRouteBuilder routes)
    {
        public IEndpointRouteBuilder MapCreateEvent()
        {
            routes
                .MapPost(
                    "/events",
                    async (
                        CreateEventCommand command,
                        CreateEventHandler handler,
                        CancellationToken ct
                    ) =>
                    {
                        var result = await handler.HandleAsync(command, ct);
                        return result.Match<IResult>(
                            () => Results.Created("/events", null),
                            e =>
                                e == Error.NullValue
                                    ? Results.BadRequest(e)
                                    : Results.StatusCode(StatusCodes.Status500InternalServerError)
                        );
                    }
                )
                .RequireAuthorization();

            return routes;
        }
    }
}
