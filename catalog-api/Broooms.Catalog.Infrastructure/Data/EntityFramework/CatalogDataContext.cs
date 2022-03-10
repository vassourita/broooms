namespace Broooms.Catalog.Infrastructure.Data.EntityFramework;

using Broooms.Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;

public class CatalogDataContext : DbContext
{
    public CatalogDataContext(DbContextOptions<CatalogDataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(
            b =>
            {
                b.HasKey(p => p.Id);

                b.HasIndex(c => c.Name).IsUnique();

                b.Property(p => p.Name).IsRequired().HasMaxLength(255);

                b.Property(p => p.Description).IsRequired().HasMaxLength(1024);

                b.Property(p => p.PriceInCents).IsRequired();

                b.Property(p => p.Quantity).IsRequired();

                b.Property(p => p.ImageUrl).IsRequired().HasMaxLength(512);

                b.Property(p => p.CreatedAt).IsRequired();

                b.Property(p => p.UpdatedAt).IsRequired();

                b.HasMany(p => p.Categories)
                    .WithMany(c => c.Products)
                    .UsingEntity("ProductCategory");
            }
        );

        modelBuilder.Entity<Category>(
            b =>
            {
                b.HasKey(c => c.Id);

                b.HasIndex(c => c.Name).IsUnique();

                b.Property(c => c.Name).IsRequired().HasMaxLength(255);

                b.Property(c => c.Description).IsRequired().HasMaxLength(1024);

                b.Property(c => c.CreatedAt).IsRequired();

                b.Property(c => c.UpdatedAt).IsRequired();

                b.HasMany(p => p.Products)
                    .WithMany(c => c.Categories)
                    .UsingEntity("ProductCategory");
            }
        );
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
