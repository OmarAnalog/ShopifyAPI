using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Entities;

namespace Shopify.Infrastructure.Persistence
{
    public class ShopifyDbContext : DbContext
    {
        public ShopifyDbContext(DbContextOptions<ShopifyDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // add configuration for product from class product configuration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopifyDbContext).Assembly);

        }
    }
}
