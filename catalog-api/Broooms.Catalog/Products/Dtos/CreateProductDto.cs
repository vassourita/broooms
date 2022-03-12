namespace Broooms.Catalog.Products.Dtos;

using System.ComponentModel.DataAnnotations;

public class CreateProductDto
{
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
}
