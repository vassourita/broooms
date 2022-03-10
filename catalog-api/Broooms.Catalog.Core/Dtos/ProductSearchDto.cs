namespace Broooms.Catalog.Core.Dtos;

using System.ComponentModel.DataAnnotations;

public class ProductSearchDto
{
    [Required]
    public string Query { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    [Required]
    [Range(1, 100)]
    public int PageSize { get; set; } = 20;
}
