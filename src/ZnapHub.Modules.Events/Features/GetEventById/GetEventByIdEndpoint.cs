using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Modules.Events.Features.GetEventById;

internal static class GetEventByIdEndpoint
{
    extension(IEndpointRouteBuilder routes)
    {
        public IEndpointRouteBuilder MapGetEventById()
        {
            routes
                .MapGet(
                    "/events/{eventId:guid}",
                    async (Guid eventId, GetEventByIdHandler handler, CancellationToken ct) =>
                    {
                        var result = await handler.HandleAsync(new GetEventByIdQuery(eventId), ct);
                        return result.Match<IResult>(
                            Results.Ok,
                            e => e == Error.NotFound ? Results.NotFound(e) : Results.StatusCode(500)
                        );
                    }
                )
                .RequireAuthorization();

            return routes;
        }
    }
}
