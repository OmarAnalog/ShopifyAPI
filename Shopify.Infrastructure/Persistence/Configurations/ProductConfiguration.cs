using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Entities;
using System.Reflection.Emit;

namespace Shopify.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
            new Product { Id = 1, Name = "Sample Product A", Description = "Dev seed", Price = 9.99m, Stock = 10 },
            new Product { Id = 2, Name = "Sample Product B", Description = "Dev seed", Price = 19.99m, Stock = 5 }
            );

        }
    }
}
