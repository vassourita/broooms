namespace Broooms.Catalog.Core.Entities;

public class Product : Entity<Guid>
{
    protected Product() { }

    public Product(
        Guid id,
        string name,
        string description,
        uint priceInCents,
        int quantity,
        string imageUrl,
        ICollection<Category> categories = null
    )
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.PriceInCents = priceInCents;
        this.Quantity = quantity;
        this.ImageUrl = imageUrl;
        this.CreatedAt = DateTime.UtcNow;
        this.UpdatedAt = DateTime.UtcNow;
        this.Categories = categories ?? new List<Category>();
    }

    public Product AddCategory(Category category)
    {
        if (this.Categories.Contains(category))
        {
            return this;
        }

        this.Categories.Add(category);
        return this;
    }

    public Product RemoveCategory(Category category)
    {
        this.Categories.Remove(category);
        return this;
    }

    public void AddStock(int quantity) => this.Quantity += quantity;

    public void RemoveStock(int quantity = 1) => this.Quantity -= quantity;

    public void Update(
        string name = null,
        string description = null,
        uint priceInCents = 0,
        string imageUrl = null
    )
    {
        this.Name = name ?? this.Name;
        this.Description = description ?? this.Description;
        this.PriceInCents = priceInCents > 0 ? priceInCents : this.PriceInCents;
        this.ImageUrl = imageUrl ?? this.ImageUrl;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public uint PriceInCents { get; private set; }
    public int Quantity { get; private set; }
    public string ImageUrl { get; private set; }

    public ICollection<Category> Categories { get; private set; }
}
