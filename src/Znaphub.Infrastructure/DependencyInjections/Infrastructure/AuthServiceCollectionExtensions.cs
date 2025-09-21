using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Infrastructure.Data.Contexts;
using ZnapHub.Infrastructure.Identity;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

public static class AuthServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<AuthDbContext>();
        return services;
    }
}