using Microsoft.Extensions.DependencyInjection;

namespace Shopify.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Add application services here, e.g.:
            // services.AddTransient<IMyService, MyService>();

            return services;
        }
    }
}
