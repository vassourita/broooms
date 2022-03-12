#pragma warning disable CA1304
namespace Broooms.Catalog.Products.Controllers;

using Broooms.Catalog.Data;
using Broooms.Catalog.Data.Validation;
using Broooms.Catalog.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/v1/products")]
[ApiController]
public class ProductImageController : ControllerBase
{
    private readonly DataContext _dataContext;
    private readonly IFileManager _fileManager;

    private readonly string[] _validContentTypes = new[] { "image/jpeg", "image/png" };

    public ProductImageController(DataContext dataContext, IFileManager fileManager)
    {
        _dataContext = dataContext;
        _fileManager = fileManager;
    }

    /// <summary>
    /// Updates the Image of the product with the given id.
    /// </summary>
    /// <param name="id">The product id.</param>
    /// <param name="imageFile">The uploaded image.</param>
    /// <returns>A 200 response with the updated product, a 400 response with the validation errors, or a 404 response if the product is not found.</returns>
    [HttpPut("{id:guid}/image")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Upload([FromRoute] Guid id, [FromForm] IFormFile imageFile)
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

        if (!_validContentTypes.Contains(imageFile.ContentType))
        {
            return BadRequest(
                new { Errors = new ErrorList("ImageFile", "Image must be a jpeg or png.") }
            );
        }

        if (product.ImageUrl != null)
        {
            await _fileManager.DeleteFileAsync(product.ImageUrl);
        }

        var newImageUrl = await _fileManager.UploadFileAsync(
            $"{Guid.NewGuid()}__{imageFile.FileName}",
            ReadStream(imageFile.OpenReadStream()),
            imageFile.ContentType
        );
        product.ImageUrl = newImageUrl.AbsolutePath;

        _dataContext.Update(product);
        await _dataContext.SaveChangesAsync();

        return Ok(product);
    }

    public static byte[] ReadStream(Stream input)
    {
        var buffer = new byte[16 * 1024];
        using var ms = new MemoryStream();
        int read;
        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            ms.Write(buffer, 0, read);
        }
        return ms.ToArray();
    }
}
