using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Application.Abstractions.Identity;
using ZnapHub.Infrastructure.Identity;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

public static class AuthServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}
