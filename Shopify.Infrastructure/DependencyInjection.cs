using Microsoft.Extensions.DependencyInjection;

namespace Shopify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Add infrastructure services here, e.g.:
            // services.AddTransient<IMyRepository, MyRepository>();

            return services;
        }
    }
}
