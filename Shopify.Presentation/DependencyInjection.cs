using Serilog;
using OpenTelemetry.Extensions.Hosting;
using OpenTelemetry.Trace;
using Microsoft.Extensions.Options;
using Serilog.Enrichers.OpenTelemetry;
using Shopify.Application.Services;
using Shopify.Presentation.Services.JwtService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shopify.Presentation.Services.JwtService.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace Shopify.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination"));
            });
            Log.Logger= new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
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
            services.AddScoped<ITokenService, JwtTokenService>();
            return services;
        }
        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = configuration.GetSection("Jwt").Get<JwtConfiguration>();
            services.Configure<JwtConfiguration>(configuration.GetSection("Jwt"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtConfiguration.Audiance,
                    ValidateLifetime = true,
                };
            });
            return services;
        }
    }
}
