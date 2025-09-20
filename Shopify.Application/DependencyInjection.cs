using Microsoft.Extensions.DependencyInjection;
using Shopify.Application.Services;
using System.Reflection;

namespace Shopify.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Add application services here, e.g.:
            // services.AddTransient<IMyService, MyService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
