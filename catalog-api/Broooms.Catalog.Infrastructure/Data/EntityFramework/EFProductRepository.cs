namespace Broooms.Catalog.Infrastructure.Data.EntityFramework;

using System.Collections.Generic;
using System.Threading.Tasks;
using Broooms.Catalog.Core.Data;
using Broooms.Catalog.Core.Dtos;
using Broooms.Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;

public class EFProductRepository : EFGenericRepository<Product, Guid>, IProductRepository
{
    public EFProductRepository(CatalogDataContext context) : base(context) { }

    public async Task<IEnumerable<Product>> FindByFiltersAsync(ProductSearchDto filters)
    {
        var queryable = this.DbSet.AsQueryable();

        if (!string.IsNullOrEmpty(filters.Query))
        {
            queryable = queryable.Where(
                x => x.Name.Contains(filters.Query) || x.Description.Contains(filters.Query)
            );
        }

        return await queryable
            .Take(filters.PageSize)
            .Skip((filters.Page - 1) * filters.PageSize)
            .ToListAsync();
    }
}
