namespace Broooms.Catalog.Api.Controllers;

using Broooms.Catalog.Core.Dtos;
using Broooms.Catalog.Core.Entities;
using Broooms.Catalog.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService) =>
        this._productService = productService;

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
    [ProducesResponseType(400, Type = typeof(IEnumerable<KeyValuePair<string, ModelStateEntry>>))]
    public async Task<IActionResult> Index([FromBody] ProductSearchDto filters)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState.SelectMany(x => x.Value?.Errors));
        }

        var products = await this._productService.FindByFiltersAsync(filters);
        return this.Ok(products);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(Product))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Show([FromRoute] Guid id)
    {
        var product = await this._productService.FindByIdAsync(id);
        if (product == null)
        {
            return this.NotFound();
        }

        return this.Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Product))]
    [ProducesResponseType(400, Type = typeof(IEnumerable<KeyValuePair<string, ModelStateEntry>>))]
    public async Task<IActionResult> Store([FromBody] ProductCreateDto dto)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState.SelectMany(x => x.Value?.Errors));
        }

        var product = await this._productService.CreateAsync(dto);
        return this.CreatedAtAction(nameof(this.Show), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200, Type = typeof(Product))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductUpdateDto dto)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState.SelectMany(x => x.Value?.Errors));
        }

        dto.Id = id;
        var product = await this._productService.UpdateAsync(dto);
        if (product == null)
        {
            return this.NotFound();
        }

        return this.Ok(product);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Destroy([FromRoute] Guid id)
    {
        var product = await this._productService.FindByIdAsync(id);
        if (product == null)
        {
            return this.NotFound();
        }

        await this._productService.RemoveAsync(id);
        return this.NoContent();
    }
}
