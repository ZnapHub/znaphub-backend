using Microsoft.EntityFrameworkCore;
using ZnapHub.Infrastructure.Data.Contexts;

namespace ZnapHub.Api.Extensions;

internal static class ApplicationBuilderExtensions
{
    internal static async Task ApplyMigrationAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var appDb = scope.ServiceProvider.GetRequiredService<ZnapHubWriteContext>();
        await appDb.Database.MigrateAsync();
    }
}
