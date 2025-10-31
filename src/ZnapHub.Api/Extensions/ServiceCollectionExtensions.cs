using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;

namespace ZnapHub.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Jwt:Authority"];
                options.Audience = configuration["Jwt:Audience"];
                options.RequireHttpsMetadata = configuration.GetValue<bool>("Jwt:HttpsMetadata");
            });

        return services;
    }

    internal static IServiceCollection AddFormLimit(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = configuration.GetValue<long>(
                "Storage:DefaultSizeLimit"
            );
        });

        return services;
    }
}
