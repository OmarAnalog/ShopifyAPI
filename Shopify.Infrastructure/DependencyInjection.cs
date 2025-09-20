using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopify.Application.Services;
using Shopify.Domain.Repositories;
using Shopify.Infrastructure.ExternalClients;
using Shopify.Infrastructure.Persistence;
using Shopify.Infrastructure.Persistence.Configurations;
using Shopify.Infrastructure.Persistence.Repositories;
using System;

namespace Shopify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            // Add infrastructure services here, e.g.:
            // services.AddTransient<IMyRepository, MyRepository>();
            var conn = configuration.GetConnectionString("Default");

            services.AddDbContext<ShopifyDbContext>(options =>
                options.UseNpgsql(conn, npgsql => npgsql.EnableRetryOnFailure()));
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IPaymentService, PaymentService>();
            return services;
        }
    }
}
