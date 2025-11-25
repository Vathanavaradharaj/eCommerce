using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.Data;
using eCommerce.DTOs;
using eCommerce.Models;

namespace eCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly EcomDbContext _context;

        public CartController(EcomDbContext context)
        {
            _context = context;
        }

        // -------------------------------------------------------
        // GET: api/cart
        // Get all cart items
        // -------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            var carts = await _context.Carts.ToListAsync();
            return Ok(carts);
        }

        // -------------------------------------------------------
        // GET: api/cart/user/{userId}
        // Get cart by username / userId
        // -------------------------------------------------------
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCartByUser(string userId)
        {
            var items = await _context.Carts
                .Where(c => c.UserId.ToLower() == userId.ToLower())
                .ToListAsync();

            return Ok(items);
        }

        // -------------------------------------------------------
        // GET: api/cart/{id}
        // Get single cart item
        // -------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItem(string id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null) return NotFound();

            return Ok(cart);
        }

        // -------------------------------------------------------
        // POST: api/cart
        // Add a new cart item
        // -------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartDto cartDto)
        {
            if (cartDto == null) return BadRequest("Cart data is null");

               var product = await _context.Products
        .FirstOrDefaultAsync(p => p.Id == cartDto.ProductId);
         if (product == null)
        return BadRequest("Invalid ProductId. Product does not exist.");

           var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Id == cartDto.UserId);

    if (user == null)
        return BadRequest($"Invalid UserId '{cartDto.UserId}'. User does not exist.");
          
          var cart = new Cart
{
    Id = Guid.NewGuid().ToString(),
    UserId = cartDto.UserId,
    ProductId = cartDto.ProductId,
    ProductName = cartDto.ProductName,
    Price = cartDto.Price ?? 0,
    Quantity = cartDto.Quantity,
    Total = (cartDto.Price ?? 0) * cartDto.Quantity,
    AddedDate = DateTime.Now,

};


            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return Ok(cart);
            
        }

        // -------------------------------------------------------
        // PUT: api/cart/{id}
        // Update quantity or cart details
        // -------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(string id, [FromBody] CartDto cartDto)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
                return NotFound($"Cart item with ID {id} not found.");

            cart.ProductName = cartDto.ProductName;
            cart.Price = cartDto.Price ?? cart.Price;
            cart.Quantity = cartDto.Quantity;
            cart.Total = (cart.Price ?? 0) * cartDto.Quantity;

            await _context.SaveChangesAsync();

            return Ok(cart);
        }

        // -------------------------------------------------------
        // DELETE: api/cart/{id}
        // Remove item from cart
        // -------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(string id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
                return NotFound($"Cart item with ID {id} not found.");

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // -------------------------------------------------------
        // DELETE: api/cart/user/{userId}
        // Remove all cart items for user (Used when placing order)
        // -------------------------------------------------------
        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            var items = await _context.Carts
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!items.Any())
                return NotFound("No items found to delete.");

            _context.Carts.RemoveRange(items);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
