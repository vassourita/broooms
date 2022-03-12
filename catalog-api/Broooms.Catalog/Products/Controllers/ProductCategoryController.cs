#pragma warning disable CA1304
namespace Broooms.Catalog.Products.Controllers;
using Broooms.Catalog.Data;
using Broooms.Catalog.Data.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/v1/products")]
[ApiController]
public class ProductCategoryController : ControllerBase
{
    private readonly DataContext _dataContext;

    public ProductCategoryController(DataContext dataContext) => _dataContext = dataContext;

    /// <summary>
    /// Adds a category to a product.
    /// </summary>
    /// <param name="id">The product id.</param>
    /// <param name="dto">The category id to be added.</param>
    /// <returns>A 201 response with the newly created product category or a 404 response if the product or category does not exist.</returns>
    [HttpPost("{id:guid}/categories/{categoryId:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Index([FromRoute] Guid id, [FromRoute] int categoryId)
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

        var category = await _dataContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
        if (category == null)
        {
            return NotFound();
        }

        product.Categories.Add(category);

        _dataContext.Update(product);
        await _dataContext.SaveChangesAsync();

        return Ok(product);
    }

    /// <summary>
    /// Removes a category from a product.
    /// </summary>
    /// <param name="id">The product id.</param>
    /// <param name="categoryId">The category id to be removed.</param>
    /// <returns>A 200 response with the updated product or a 404 response if the product or category does not exist.</returns>
    [HttpDelete("{id:guid}/categories/{categoryId:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, [FromRoute] int categoryId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Errors = ModelState.ToErrorList() });
        }

        var product = await _dataContext.Products
            .Include(p => p.Categories)
            .SingleOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        var category = await _dataContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
        if (category == null)
        {
            return NotFound();
        }

        var beforeRemoveCount = product.Categories.Count;
        product.Categories.RemoveAll(c => c.Id == categoryId);
        var afterRemoveCount = product.Categories.Count;
        var categoryExcluded = beforeRemoveCount != afterRemoveCount;
        if (!categoryExcluded)
        {
            return BadRequest(
                new
                {
                    Errors = new ErrorList(
                        "CategoryId",
                        "This product has no category with the provided category id"
                    )
                }
            );
        }

        _dataContext.Update(product);
        await _dataContext.SaveChangesAsync();

        return NoContent();
    }
}
