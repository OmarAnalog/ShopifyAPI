using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Entities;
using Shopify.Domain.Entities.Identity;

namespace Shopify.Infrastructure.Persistence
{
    public class ShopifyDbContext : IdentityDbContext<User>
    {
        public ShopifyDbContext(DbContextOptions<ShopifyDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopifyDbContext).Assembly);

        }
    }
}
