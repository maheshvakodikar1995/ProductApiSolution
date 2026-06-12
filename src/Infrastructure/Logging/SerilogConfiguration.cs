using Microsoft.Extensions.Hosting;
using Serilog;

namespace Infrastructure.Logging;

public static class SerilogConfiguration
{
    public static void ConfigureBootstrapLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateBootstrapLogger();
    }

    public static void ConfigureHost(IHostBuilder host)
    {
        host.UseSerilog((context, services, loggerConfiguration) =>
            loggerConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "ProductApi"));
    }
}
