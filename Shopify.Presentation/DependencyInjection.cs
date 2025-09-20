using Serilog;
using OpenTelemetry.Extensions.Hosting;
using OpenTelemetry.Trace;
using Microsoft.Extensions.Options;
using Serilog.Enrichers.OpenTelemetry;
namespace Shopify.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            Log.Logger= new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithOpenTelemetryTraceId()
                .Enrich.WithOpenTelemetrySpanId()
                .WriteTo.File("logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            services.AddOpenTelemetry().WithTracing(b => 
                {
                    b.AddAspNetCoreInstrumentation();
                    b.AddHttpClientInstrumentation();
                    b.AddConsoleExporter();
                    b.AddEntityFrameworkCoreInstrumentation(opt =>
                    {
                        opt.SetDbStatementForText = true;
                    });
                }
            );
            return services;
        }
    }
}
