namespace Broooms.Catalog.Core.Data;

public interface IUnitOfWork
{
    Task CommitAsync();
    Task RollbackAsync();
}
