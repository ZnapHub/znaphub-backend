using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Modules.Events.Features.GetEventsByOrganizer;

internal static class GetEventsByOrganizerEndpoint
{
    extension(IEndpointRouteBuilder routes)
    {
        public IEndpointRouteBuilder MapGetEventsByOrganizer()
        {
            routes
                .MapGet(
                    "/events",
                    async (GetEventsByOrganizerHandler handler, CancellationToken ct) =>
                    {
                        var result = await handler.HandleAsync(new GetEventsByOrganizerQuery(), ct);
                        return result.Match(
                            Results.Ok,
                            e =>
                                e == Error.NullValue
                                    ? Results.BadRequest(e)
                                    : Results.StatusCode(500)
                        );
                    }
                )
                .RequireAuthorization();

            return routes;
        }
    }
}
