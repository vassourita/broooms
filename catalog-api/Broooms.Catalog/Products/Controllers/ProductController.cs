#pragma warning disable CA1304

namespace Broooms.Catalog.Products.Controllers;

using Broooms.Catalog.Data;
using Broooms.Catalog.Data.Validation;
using Broooms.Catalog.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/v1/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly DataContext _dataContext;

    public ProductController(DataContext dataContext) => _dataContext = dataContext;

    /// <summary>
    /// Gets all products within the given page, page size and query.
    /// </summary>
    /// <param name="search">The search options.</param>
    /// <returns>A 200 response with the found products.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
    public async Task<IActionResult> Index([FromQuery] SearchProductDto search)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Errors = ModelState.SelectMany(x => x.Value.Errors) });
        }

        var query = _dataContext.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search.Query))
        {
            query = query.Where(
                x =>
                    x.Name.ToLower().Contains(search.Query.ToLower())
                    || x.Description.ToLower().Contains(search.Query.ToLower())
            );
        }

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((search.Page - 1) * search.PageSize)
            .Take(search.PageSize)
            .ToListAsync();

        Response.Headers.Add(
            "X-Total-Count",
            totalItems.ToString(System.Globalization.CultureInfo.InvariantCulture)
        );
        return Ok(items);
    }

    /// <summary>
    /// Gets the product with the given id.
    /// </summary>
    /// <param name="id">The product id.</param>
    /// <returns>A 200 response with the found product or a 404 empty response if it was not found.</returns>
    [HttpGet("{id}", Name = "GetProductById")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Show([FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Errors = ModelState.ToErrorList() });
        }

        var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    /// <summary>
    /// Creater a new product.
    /// </summary>
    /// <param name="dto">The payload to create the product.</param>
    /// <returns>A 201 response with the created product.</returns>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Errors = ModelState.ToErrorList() });
        }

        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Quantity = dto.Quantity,
        };
        await _dataContext.Products.AddAsync(newProduct);
        await _dataContext.SaveChangesAsync();
        return Created(Url.Link("GetProductById", new { id = newProduct.Id }), newProduct);
    }

    /// <summary>
    /// Updates the product with the given id.
    /// </summary>
    /// <param name="id">The product id.</param>
    /// <param name="dto">What should be updated in the product.</param>
    /// <returns>A 200 response with the found product or a 404 empty response if it was not found.</returns>
    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Errors = ModelState.ToErrorList() });
        }

        var productToUpdate = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (productToUpdate == null)
        {
            return NotFound();
        }
        productToUpdate.Name = dto.Name;
        productToUpdate.Description = dto.Description;
        productToUpdate.Price = dto.Price;
        _dataContext.Update(productToUpdate);
        await _dataContext.SaveChangesAsync();
        return Ok(productToUpdate);
    }

    /// <summary>
    /// Deletes the product with the given id.
    /// </summary>
    /// <param name="id">The product id.</param>
    /// <returns>A 204 response if the delete succeeded or a 404 empty response if the product was not found.</returns>
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Destroy([FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Errors = ModelState.ToErrorList() });
        }

        var productToDelete = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (productToDelete == null)
        {
            return NotFound();
        }
        _dataContext.Products.Remove(productToDelete);
        await _dataContext.SaveChangesAsync();
        return NoContent();
    }
}
