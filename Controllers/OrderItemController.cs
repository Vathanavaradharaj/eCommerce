using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.Data;
using eCommerce.Models;
using eCommerce.DTOs;
using System.Threading.Tasks;

namespace eCommerce.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrderItemController : ControllerBase
	{
		private readonly EcomDbContext _context;

		public OrderItemController(EcomDbContext context)
		{
			_context = context;
		}

		// GET: api/OrderItem
		[HttpGet]
		public async Task<IActionResult> GetOrderItems()
		{
			var items = await _context.Set<OrderItem>().ToListAsync();
			return Ok(items);
		}

		// GET: api/OrderItem/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderItem(int id)
		{
			var item = await _context.Set<OrderItem>().FindAsync(id);
			if (item == null)
				return NotFound();
			return Ok(item);
		}

		// POST: api/OrderItem
	[HttpPost]
public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemDto dto)
{
    if (dto.Price == null)
        return BadRequest("Price is required.");

    var item = new OrderItem
    {
        UserId = dto.UserId ?? string.Empty,
        ProductId = dto.ProductId ?? string.Empty,
        Quantity = dto.Quantity ?? 1, // Default quantity = 1
        Price = dto.Price.Value,
        Total = dto.Price.Value * (dto.Quantity ?? 1),
        OrderDate = dto.OrderDate ?? DateTime.Now,
        ExpectedDeliveryDate = dto.ExpectedDeliveryDate ?? DateTime.Now.AddDays(7)
    };

    _context.Set<OrderItem>().Add(item);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetOrderItem), new { id = item.Id }, item);
}



		// PUT: api/OrderItem/{id}
	[HttpPut("{id}")]
public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItemDto dto)
{
    var item = await _context.Set<OrderItem>().FindAsync(id);
    if (item == null)
        return NotFound();

    if (!string.IsNullOrEmpty(dto.UserId))
        item.UserId = dto.UserId;

    if (!string.IsNullOrEmpty(dto.ProductId))
        item.ProductId = dto.ProductId;

    if (dto.Price.HasValue)
        item.Price = dto.Price.Value;

    if (dto.Quantity.HasValue)
    {
        item.Quantity = dto.Quantity.Value;
        item.Total = item.Price * item.Quantity;
    }

    if (dto.OrderDate.HasValue)
        item.OrderDate = dto.OrderDate.Value;

    if (dto.ExpectedDeliveryDate.HasValue)
        item.ExpectedDeliveryDate = dto.ExpectedDeliveryDate.Value;

    await _context.SaveChangesAsync();
    return NoContent();
}




		// DELETE: api/OrderItem/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrderItem(int id)
		{
			var item = await _context.Set<OrderItem>().FindAsync(id);
			if (item == null)
				return NotFound();

			_context.Set<OrderItem>().Remove(item);
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}
