namespace Broooms.Catalog.Products.Dtos;

using System.ComponentModel.DataAnnotations;

public class UpdateProductStockDto
{
    [Required]
    public int Quantity { get; set; }
}
