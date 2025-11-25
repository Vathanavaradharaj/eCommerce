using Microsoft.EntityFrameworkCore;
using eCommerce.Data;
using eCommerce.Models;
using eCommerce.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(EcomDbContext context) : ControllerBase
    {


        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await context.Products.ToListAsync();
            return Ok(products);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] ProductDto dto)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.ItemName = dto.ItemName;
            product.ItemPrice = dto.ItemPrice;
            product.UserId = dto.UserId;
            // ItemImage update is omitted for simplicity

            await context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return NoContent();
        }

 [HttpPost("add")]
[Consumes("multipart/form-data")]
public async Task<IActionResult> AddProduct(
    IFormFile? image,
    [FromForm] string name,
    [FromForm] decimal price,
    [FromForm] string UserId
)
{
    if (image == null || image.Length == 0)
        return BadRequest("Image is required.");

    using var ms = new MemoryStream();
    await image.CopyToAsync(ms);
    var imageBytes = ms.ToArray();

    var product = new Product
    {
        Id = Guid.NewGuid().ToString(),
        ItemName = name,
        ItemPrice = price,
        UserId = UserId,
        ItemImage = imageBytes
    };

    context.Products.Add(product);
    await context.SaveChangesAsync();

    return Ok(new { message = "Product saved!", productId = product.Id });
}

    }
}