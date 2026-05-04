using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZnapHub.Modules.QrCodes.Data;
using ZnapHub.Modules.QrCodes.Domain.Repositories;
using ZnapHub.Modules.QrCodes.Features.GenerateQrCode;
using ZnapHub.Modules.QrCodes.Services;
using ZnapHub.Shared.Contracts.QrCodes;

namespace ZnapHub.Modules.QrCodes;

public static class QrCodesModule
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddQrCodesModule(IConfiguration configuration)
        {
            services.AddDbContext<QrCodesDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ZnapHub"))
            );

            services.AddScoped<IQrCodeRepository, QrCodeRepository>();
            services.AddScoped<IShortIdGenerator, ShortIdGenerator>();
            services.AddScoped<IQrCodeUrlFactory, QrCodeUrlFactory>();
            services.AddScoped<IQrCodeLookupService, QrCodeLookupService>();
            services.AddScoped<GenerateQrCodeHandler>();

            return services;
        }
    }

    extension(IEndpointRouteBuilder routes)
    {
        public IEndpointRouteBuilder MapQrCodesEndpoints()
        {
            routes.MapGenerateQrCode();
            return routes;
        }
    }
}
