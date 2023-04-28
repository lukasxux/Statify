using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using StatifyProject.Application.Model;
using StatifyProject.Application.Dto;
using StatifyProject.Application.Infrastructure;

namespace StatifyProject.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly StatifyContext _context;

        public UserController(ILogger<UserController> logger, StatifyContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return NotFound();
                }
                var userDto = new UserDto(
                    user.Guid,
                    user.Username,
                    user.Email,
                    user.PasswordHash,
                    user.Bio ?? string.Empty,
                    user.FavoriteSong?.ToString() ?? string.Empty,
                    user.FavoriteArtist?.ToString() ?? string.Empty,
                    user.Role
                );
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user by ID.");
                return StatusCode(500, "Internal server error.");
            }
        }

        /*
        [HttpGet("{username}")]
        public IActionResult GetUserByUsername(String Username)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == Username);

                if (user == null)
                {
                    return NotFound();
                }

                var userDto = new UserDto(
                    user.Guid,
                    user.Username,
                    user.Email,
                    user.PasswordHash,
                    user.FavoriteSong?.ToString() ?? string.Empty,
                    user.FavoriteArtist?.ToString() ?? string.Empty,
                    user.Bio ?? string.Empty,
                    user.Role
                );
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user by ID.");
                return StatusCode(500, "Internal server error.");
            }
        }
        */

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _context.Users.ToList();

                if (users == null || users.Count == 0)
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting users.");
                return StatusCode(500, "Internal server error.");
            }
        }


        
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new User(userDto.Username, userDto.Email, userDto.Password, DateTime.UtcNow, userDto.Bio, userDto.Role, userDto.FavoriteSong, userDto.FavoriteArtist);

                _context.Users.Add(user);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user.");
                return StatusCode(500, "Internal server error.");
            }
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = _context.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return NotFound();  //string username, string email, string initialPassword, DateTime created_at, string bio, UserRole role, string? favoriteSong = null, string? favoriteArtist = null
                }
                user.Username = userDto.Username;
                user.Email = userDto.Email;
                user.SetPassword(userDto.Password);
                user.Bio = userDto.Bio;
                user.FavoriteSong = userDto.FavoriteSong;
                user.FavoriteArtist = userDto.FavoriteArtist;

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user.");
                return StatusCode(500, "Internal server error.");
            }
        }
        

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(user);
                _context.SaveChanges();

                return Ok("ok");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
