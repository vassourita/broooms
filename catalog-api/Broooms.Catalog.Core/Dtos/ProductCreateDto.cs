namespace Broooms.Catalog.Core.Dtos;

using System.ComponentModel.DataAnnotations;

public class ProductCreateDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(1024)]
    public string Description { get; set; }

    [Required]
    public uint PriceInCents { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public string ImageUrl { get; set; }
}
