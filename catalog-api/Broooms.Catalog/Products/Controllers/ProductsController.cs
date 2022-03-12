namespace Broooms.Catalog.Products.Controllers;

using Broooms.Catalog.Data;
using Broooms.Catalog.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class NameController : ControllerBase
{
    private readonly DataContext _dataContext;

    public NameController(DataContext dataContext) => _dataContext = dataContext;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _dataContext.Products.ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}", Name = "GetProductById")]
    public async Task<IActionResult> Show([FromRoute] Guid id)
    {
        var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto product)
    {
        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
        };
        await _dataContext.Products.AddAsync(newProduct);
        await _dataContext.SaveChangesAsync();
        return Created(Url.Link("GetProductById", new { id = newProduct.Id }), newProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateProductDto product
    )
    {
        var productToUpdate = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (productToUpdate == null)
        {
            return NotFound();
        }
        productToUpdate.Name = product.Name;
        productToUpdate.Description = product.Description;
        productToUpdate.Price = product.Price;
        _dataContext.Update(productToUpdate);
        await _dataContext.SaveChangesAsync();
        return Ok(productToUpdate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Destroy([FromRoute] Guid id)
    {
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
