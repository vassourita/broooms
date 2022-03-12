namespace Broooms.Catalog.Data;

public interface IPaginated
{
    int Page { get; set; }
    int PageSize { get; set; }
}
