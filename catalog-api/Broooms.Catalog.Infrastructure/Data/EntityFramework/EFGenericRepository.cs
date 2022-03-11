namespace Broooms.Catalog.Infrastructure.Data.EntityFramework;

using System.Threading.Tasks;
using Broooms.Catalog.Core.Data;
using Broooms.Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;

public class EFGenericRepository<T, TId> : IRepository<T, TId>
    where T : Entity<TId>
    where TId : struct
{
    protected CatalogDataContext Context { get; private set; }
    protected DbSet<T> DbSet { get; private set; }

    public IUnitOfWork UnitOfWork { get; private set; }

    public EFGenericRepository(CatalogDataContext context)
    {
        this.Context = context;
        this.DbSet = this.Context.Set<T>();
        this.UnitOfWork = context;
    }

    public Task<T> AddAsync(T entity)
    {
        this.DbSet.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<T> FindByIdAsync(TId id) => this.DbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));

    public Task RemoveAsync(TId id)
    {
        var entity = this.DbSet.Find(id);
        if (entity != null)
        {
            this.DbSet.Remove(entity);
        }
        return Task.CompletedTask;
    }

    public Task<T> UpdateAsync(T entity)
    {
        this.DbSet.Update(entity);
        return Task.FromResult(entity);
    }
}
