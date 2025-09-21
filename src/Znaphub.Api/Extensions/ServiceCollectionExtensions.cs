using Microsoft.AspNetCore.Authentication.JwtBearer;

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
}
