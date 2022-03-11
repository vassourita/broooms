namespace Broooms.Catalog.Core.Data;

public interface IRepository<T, TId>
{
    Task<T> FindByIdAsync(TId id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task RemoveAsync(TId id);

    IUnitOfWork UnitOfWork { get; }
}
