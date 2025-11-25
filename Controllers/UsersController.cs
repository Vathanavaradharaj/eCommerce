using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.Data;
using eCommerce.DTOs;
using eCommerce.Models;


namespace eCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        
        private readonly EcomDbContext _context;

        public UsersController(EcomDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
  [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var response=new UserDto
            {
                Id=user.Id,
                Username=user.Username,
                Password=user.Password,
                Role=user.Role
            };
            return Ok(response);
        }
        [HttpPost]

        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
         if(userDto == null)
         {
            return BadRequest("User data is null.");
         }

         var user= new User
         {
             Id=Guid.NewGuid().ToString(),
             Username=userDto.Username,
             Password=userDto.Password,
                Role=userDto.Role
         };
         _context.Users.Add(user);
         await _context.SaveChangesAsync();
         var response=new UserDto
         {
            Id=user.Id,
            Username=user.Username,
            Password=user.Password,
            Role=user.Role
         };
         return Ok(response);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateUser(string id,[FromBody] UserDto userDto)
    {
       if(userDto==null)
            {
                return BadRequest("User data is null.");
            }

            var user=await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            user.Username=userDto.Username;
            user.Password=userDto.Password;
            user.Role=userDto.Role;
            await _context.SaveChangesAsync();
            var response=new UserDto
            {
                Id=user.Id,
                Username=user.Username,
                Password=user.Password,
                Role=user.Role
            };

            return Ok(response);

    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteUser(string id)
        {
            var user=await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok($"User with ID {id} deleted Successfully");
        }

        
}

}
