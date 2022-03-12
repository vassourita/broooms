namespace Broooms.Catalog.Products;

using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; protected set; }

    public string ImageUrl { get; set; }
}