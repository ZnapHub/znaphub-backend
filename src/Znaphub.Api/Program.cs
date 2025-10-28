using ZnapHub.Api.Extensions;
using ZnapHub.Infrastructure.Data.Migrations;
using ZnapHub.Infrastructure.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddZnapHub(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddFormLimit(builder.Configuration);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1.json");
    app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
}

await app.ApplyMigrationAsync();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");
await app.RunAsync();
