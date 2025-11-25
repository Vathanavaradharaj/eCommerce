using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using eCommerce.Data;
using eCommerce.Models;
using eCommerce.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace eCommercec.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterUserController : ControllerBase
    {
        private readonly EcomDbContext _context;

        public RegisterUserController(EcomDbContext context)
        {
            _context = context;
        }


        // GET: api/RegisterUser
        [HttpGet]
        public async Task<IActionResult> GetRegisterUsers()
        {
            var users = await _context.RegisterUsers.ToListAsync();
            return Ok(users);
        }

        // GET: api/RegisterUser/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegisterUser(string id)
        {
            var user = await _context.RegisterUsers.FindAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                return BadRequest("User data is null.");
            }

            var registerUser = new RegisterUser
            {
                Id = Guid.NewGuid().ToString(),
                Username = registerUserDto.Username,
                Email = registerUserDto.Email,
                Password = registerUserDto.Password,
                ProfileImage = registerUserDto.ProfileImage,
                Role = registerUserDto.Role,
                Country = registerUserDto.Country,
                State = registerUserDto.State,
                City = registerUserDto.City,
                TermsAccepted = registerUserDto.TermsAccepted
            };

            _context.RegisterUsers.Add(registerUser);
            await _context.SaveChangesAsync();

            var response = new RegisterUserDto
            {
                Id = registerUser.Id,
                Username = registerUser.Username,
                Email = registerUser.Email,
                Password = registerUser.Password,
                ProfileImage = registerUser.ProfileImage,
                Role = registerUser.Role,
                Country = registerUser.Country,
                State = registerUser.State,
                City = registerUser.City,
                TermsAccepted = registerUser.TermsAccepted
            };

            return Ok(response);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateRegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                return BadRequest("User data is null.");
            }

            var registerUser = new RegisterUser
            {
                Id = Guid.NewGuid().ToString(),
                Username = registerUserDto.Username,
                Email = registerUserDto.Email,
                Password = registerUserDto.Password,
                ProfileImage = registerUserDto.ProfileImage,
                Role = registerUserDto.Role,
                Country = registerUserDto.Country,
                State = registerUserDto.State,
                City = registerUserDto.City,
                TermsAccepted = registerUserDto.TermsAccepted
            };

            _context.RegisterUsers.Add(registerUser);
            await _context.SaveChangesAsync();

            var response = new RegisterUserDto
            {
                Id = registerUser.Id,
                Username = registerUser.Username,
                Email = registerUser.Email,
                Password = registerUser.Password,
                ProfileImage = registerUser.ProfileImage,
                Role = registerUser.Role,
                Country = registerUser.Country,
                State = registerUser.State,
                City = registerUser.City,
                TermsAccepted = registerUser.TermsAccepted
            };

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegisterUser(string id, [FromBody] RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                return BadRequest("User data is null.");
            }

            var registerUser = await _context.RegisterUsers.FindAsync(id);
            if (registerUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            registerUser.Username = registerUserDto.Username;
            registerUser.Email = registerUserDto.Email;
            registerUser.Password = registerUserDto.Password;
            registerUser.ProfileImage = registerUserDto.ProfileImage;
            registerUser.Role = registerUserDto.Role;
            registerUser.Country = registerUserDto.Country;
            registerUser.State = registerUserDto.State;
            registerUser.City = registerUserDto.City;
            registerUser.TermsAccepted = registerUserDto.TermsAccepted;

            await _context.SaveChangesAsync();

            var response = new RegisterUserDto
            {
                Id = registerUser.Id,
                Username = registerUser.Username,
                Email = registerUser.Email,
                Password = registerUser.Password,
                ProfileImage = registerUser.ProfileImage,
                Role = registerUser.Role,
                Country = registerUser.Country,
                State = registerUser.State,
                City = registerUser.City,
                TermsAccepted = registerUser.TermsAccepted
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegisterUser(string id)
        {
            var registerUser = await _context.RegisterUsers.FindAsync(id);
            if (registerUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            _context.RegisterUsers.Remove(registerUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}