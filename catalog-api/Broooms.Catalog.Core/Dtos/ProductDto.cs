namespace Broooms.Catalog.Core.Dtos;

using System.ComponentModel.DataAnnotations;

public class ProductDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Name { get; private set; }

    [Required]
    [MinLength(1)]
    [MaxLength(1024)]
    public string Description { get; private set; }

    [Required]
    public uint PriceInCents { get; private set; }

    [Required]
    public int Quantity { get; private set; }

    [Required]
    public string ImageUrl { get; private set; }
}
