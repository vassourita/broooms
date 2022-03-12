namespace Broooms.Catalog.Products.Dtos;

using System.ComponentModel.DataAnnotations;

public class UpdateProductDto
{
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    public decimal Price { get; set; } = 0;
}
