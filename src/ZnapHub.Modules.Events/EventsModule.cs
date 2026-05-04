using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Modules.Events.Data;
using ZnapHub.Modules.Events.Domain.Repositories;
using ZnapHub.Modules.Events.Features.CreateEvent;
using ZnapHub.Modules.Events.Features.GetEventById;
using ZnapHub.Modules.Events.Features.GetEventsByOrganizer;

namespace ZnapHub.Modules.Events;

public static class EventsModule
{
    public static IServiceCollection AddEventsModule(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<EventsDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("ZnapHub"))
        );

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<CreateEventHandler>();
        services.AddScoped<GetEventByIdHandler>();
        services.AddScoped<GetEventsByOrganizerHandler>();

        return services;
    }

    public static IEndpointRouteBuilder MapEventsEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapCreateEvent();
        routes.MapGetEventById();
        routes.MapGetEventsByOrganizer();
        return routes;
    }
}
