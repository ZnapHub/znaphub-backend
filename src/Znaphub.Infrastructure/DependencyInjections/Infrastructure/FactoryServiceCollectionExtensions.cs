using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Domain.Features.QrCodes.Factories;
using ZnapHub.Infrastructure.Features.QrCodes.Factories;

namespace ZnapHub.Infrastructure.DependencyInjections.Infrastructure;

internal static class FactoryServiceCollectionExtensions
{
    internal static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddScoped<IQrCodeUrlFactory, QrCodeUrlFactory>();
        return services;
    }
}
