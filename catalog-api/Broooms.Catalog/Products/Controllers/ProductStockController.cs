#pragma warning disable CA1304
namespace Broooms.Catalog.Products.Controllers;

using Broooms.Catalog.Data;
using Broooms.Catalog.Data.Validation;
using Broooms.Catalog.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/v1/products")]
[ApiController]
public class ProductStockController : ControllerBase
{
    private readonly DataContext _dataContext;

    public ProductStockController(DataContext dataContext) => _dataContext = dataContext;

    /// <summary>
    /// Updates the stock of the product with the given id.
    /// </summary>
    /// <param name="id">The product id.</param>
    /// <param name="dto">How much items should be added or removed from stock.</param>
    /// <returns>A 200 response with the found product or a 404 empty response if it was not found.</returns>
    [HttpPut("{id}/stock")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Index(
        [FromRoute] Guid id,
        [FromBody] UpdateProductStockDto dto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Errors = ModelState.ToErrorList() });
        }

        var product = await _dataContext.Products.SingleOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        product.Quantity += dto.Quantity;
        if (product.Quantity < 0)
        {
            return BadRequest(
                new
                {
                    Errors = new ErrorList(
                        "Quantity",
                        "The product quantity after update cannot be negative."
                    )
                }
            );
        }

        _dataContext.Update(product);
        await _dataContext.SaveChangesAsync();

        return Ok(product);
    }
}
