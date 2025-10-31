using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Domain.Features.QrCodes.Services;
using ZnapHub.Infrastructure.Features.QrCodes.Services;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class ServicesServiceCollectionExtensions
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IShortIdGenerator, ShortIdGenerator>();
        return services;
    }
}
