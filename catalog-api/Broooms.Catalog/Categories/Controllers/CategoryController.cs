#pragma warning disable CA1304

namespace Broooms.Catalog.Categories.Controllers;

using Broooms.Catalog.Data;
using Broooms.Catalog.Data.Validation;
using Broooms.Catalog.Categories.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/v1/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly DataContext _dataContext;

    public CategoryController(DataContext dataContext) => _dataContext = dataContext;

    /// <summary>
    /// Gets all categories within the given page, page size and query.
    /// </summary>
    /// <param name="search">The search options.</param>
    /// <returns>A 200 response with the found categories.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Category>))]
    public async Task<IActionResult> Index()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Errors = ModelState.SelectMany(x => x.Value.Errors) });
        }

        var items = await _dataContext.Categories.ToListAsync();

        Response.Headers.Add(
            "X-Total-Count",
            items.Count.ToString(System.Globalization.CultureInfo.InvariantCulture)
        );
        return Ok(items);
    }

    /// <summary>
    /// Gets the category with the given id.
    /// </summary>
    /// <param name="id">The category id.</param>
    /// <returns>A 200 response with the found category or a 404 empty response if it was not found.</returns>
    [HttpGet("{id:int}", Name = "GetCategoryById")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Show([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Errors = ModelState.ToErrorList() });
        }

        var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="dto">The payload to create the category.</param>
    /// <returns>A 201 response with the created category.</returns>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Category))]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Errors = ModelState.ToErrorList() });
        }

        var newCategory = new Category { Name = dto.Name, Description = dto.Description, };
        await _dataContext.Categories.AddAsync(newCategory);
        await _dataContext.SaveChangesAsync();
        return Created(Url.Link("GetCategoryById", new { id = newCategory.Id }), newCategory);
    }

    /// <summary>
    /// Updates the category with the given id.
    /// </summary>
    /// <param name="id">The category id.</param>
    /// <param name="dto">What should be updated in the category.</param>
    /// <returns>A 200 response with the found category or a 404 empty response if it was not found.</returns>
    [HttpPut("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Errors = ModelState.ToErrorList() });
        }

        var categoryToUpdate = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (categoryToUpdate == null)
        {
            return NotFound();
        }
        categoryToUpdate.Name = dto.Name;
        categoryToUpdate.Description = dto.Description;
        _dataContext.Update(categoryToUpdate);
        await _dataContext.SaveChangesAsync();
        return Ok(categoryToUpdate);
    }

    /// <summary>
    /// Deletes the category with the given id.
    /// </summary>
    /// <param name="id">The category id.</param>
    /// <returns>A 204 response if the delete succeeded or a 404 empty response if the category was not found.</returns>
    [HttpDelete("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Destroy([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Errors = ModelState.ToErrorList() });
        }

        var categoryToDelete = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (categoryToDelete == null)
        {
            return NotFound();
        }
        _dataContext.Categories.Remove(categoryToDelete);
        await _dataContext.SaveChangesAsync();
        return NoContent();
    }
}
