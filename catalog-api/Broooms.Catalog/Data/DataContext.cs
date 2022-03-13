namespace Broooms.Catalog.Data;

using Broooms.Catalog.Categories;
using Broooms.Catalog.Products;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(
            table =>
            {
                table.ToTable("Products");

                table
                    .HasMany(x => x.Categories)
                    .WithMany(x => x.Products)
                    .UsingEntity("ProductCategories");
            }
        );

        modelBuilder.Entity<Category>(
            table =>
            {
                table.ToTable("Categories");

                table.Property(x => x.Id).UseSerialColumn();

                table
                    .HasMany(x => x.Products)
                    .WithMany(x => x.Categories)
                    .UsingEntity("ProductCategories");
            }
        );
    }
}
