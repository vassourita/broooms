namespace Broooms.Catalog.Core.Data;

using Broooms.Catalog.Core.Dtos;
using Broooms.Catalog.Core.Entities;

public interface IProductRepository : IRepository<Product, Guid>
{
    public Task<IEnumerable<Product>> FindByFiltersAsync(ProductSearchDto filters);
}
