namespace Broooms.Catalog.Products;

using System.ComponentModel.DataAnnotations;
using Broooms.Catalog.Categories;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }

    [MaxLength(100)]
    public string ImageName { get; set; }

    [MaxLength(300)]
    public string ImageUrl { get; set; }

    public List<Category> Categories { get; set; } = new List<Category>();
}
