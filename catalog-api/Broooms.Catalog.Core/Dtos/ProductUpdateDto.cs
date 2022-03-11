namespace Broooms.Catalog.Core.Dtos;

using System.ComponentModel.DataAnnotations;

public class ProductUpdateDto
{
    public Guid Id { get; set; }

    [MaxLength(255)]
    public string Name { get; set; }

    [MaxLength(1024)]
    public string Description { get; set; }

    public uint PriceInCents { get; set; }

    public int Quantity { get; set; }

    public string ImageUrl { get; set; }
}
