namespace Broooms.Catalog.Categories.Dtos;

using System.ComponentModel.DataAnnotations;

public class CreateCategoryDto
{
    [Required]
    [MaxLength(40)]
    public string Name { get; set; }

    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
}
