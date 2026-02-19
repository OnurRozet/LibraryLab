using LibraryLab.Databases;
using LibraryLab.Extensions;
using LibraryLab.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddLibraryLabServices(builder.Configuration);
builder.Services.RegisterEndpointsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapEndpoints();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContenxt>();
    await context.Database.MigrateAsync();
    await DatabaseSeedService.SeedDatabaseAsync(context, 100);
}

await app.RunAsync();
