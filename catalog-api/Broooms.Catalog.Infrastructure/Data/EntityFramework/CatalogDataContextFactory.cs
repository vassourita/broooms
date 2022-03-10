namespace Broooms.Catalog.Infrastructure.Data.EntityFramework;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class CatalogDataContextFactory : IDesignTimeDbContextFactory<CatalogDataContext>
{
    public CatalogDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CatalogDataContext>();
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5101;Database=catalog_db;Username=docker;Password=docker"
        );

        return new CatalogDataContext(optionsBuilder.Options);
    }
}
