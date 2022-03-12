namespace Broooms.Catalog.Categories;

using System.ComponentModel.DataAnnotations;
using Broooms.Catalog.Products;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string Name { get; set; }

    [Required]
    [MaxLength(200)]
    public string Description { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
