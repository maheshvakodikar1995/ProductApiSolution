using API.Extensions;
using API.Middleware;
using Application;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;

SerilogConfiguration.ConfigureBootstrapLogger();

try
{
    Log.Information("Starting Product API");

    var builder = WebApplication.CreateBuilder(args);

    SerilogConfiguration.ConfigureHost(builder.Host);

    builder.Services.AddOpenApi();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerDocumentation();

    builder.Services.AddApplication();

    builder.Services.AddInfrastructure(
        builder.Configuration);

    builder.Services.AddJwtAuthentication(
        builder.Configuration);

    builder.Services.AddApiVersioningConfiguration();

    builder.Services.AddHealthChecks();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseSerilogRequestLogging();

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseSwagger();

    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.MapHealthChecks("/health");

    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    app.MapGet("/weatherforecast", () =>
    {
        var forecast =  Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
