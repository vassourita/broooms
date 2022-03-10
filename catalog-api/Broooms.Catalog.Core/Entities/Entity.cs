namespace Broooms.Catalog.Core.Entities;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; }

    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
}
