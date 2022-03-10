namespace Broooms.Catalog.Core.Entities;

public class Category
{
    public Category(int id, string name, string description)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.CreatedAt = DateTime.UtcNow;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public ICollection<Product> Products { get; private set; } = new List<Product>();
}
