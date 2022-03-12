namespace Broooms.Catalog.Products.Dtos;

public class SearchProductDto
{
    public string Query { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
