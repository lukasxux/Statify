using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using StatifyProject.Application.Model;
using StatifyProject.Application.Dto;
using StatifyProject.Application.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace StatifyProject.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly StatifyContext _context;
        private readonly IConfiguration _config;


        public UserController(ILogger<UserController> logger, StatifyContext context, IConfiguration config)
        {
            _logger = logger;
            _context = context;
            _config = config;
        }

        /*
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
        */

        [HttpGet("{Guid}")]
        public IActionResult GetUserByGuid(Guid Guid)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Guid == Guid);

                if (user == null)
                {
                    return NotFound();
                }

                var users = _context.Users.Select(u => new
                {
                    u.Id,
                    u.Guid,
                    u.Username,
                    u.Created_at,
                    u.FavoriteSong,
                    u.FavoriteArtist,
                    u.Bio,
                    u.Role
                }).ToList();

                if (users == null || users.Count == 0)
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user by ID.");
                return StatusCode(500, "Internal server error.");
            }
        }


[HttpGet]
public IActionResult GetUsers()
{
    try
    {
        var users = _context.Users.Select(u => new
        {
            u.Id,
            u.Guid,
            u.Username,
            u.Created_at,
            u.FavoriteSong,
            u.FavoriteArtist,
            u.Bio,
            u.Role
        }).ToList();

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

                var user = new User(userDto.Username, userDto.Email, userDto.AccessToken, userDto.RefreshToken, userDto.Password, DateTime.UtcNow, userDto.Bio, userDto.Role, userDto.FavoriteSong, userDto.FavoriteArtist);

                _context.Users.Add(user);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetUserByGuid), new { Guid = user.Guid }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user.");
                return StatusCode(500, "Internal server error.");
            }
        }

            [HttpPost("login")]
            public IActionResult Login([FromBody] LoginDto loginDto)
            {

                var secret = Convert.FromBase64String(_config["Secret"]);
                var lifetime = TimeSpan.FromHours(3);
                // Find the user in the database based on the provided email
                var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);

                // If the user is not found or the password doesn't match, return an unauthorized response
                if (user == null || !user.CheckPassword(loginDto.Password))
                {
                    return Unauthorized("Invalid email or password.");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    // Payload for our JWT.
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                // Write username to the claim (the "data zone" of the JWT).
                new Claim(ClaimTypes.Name, user.Username.ToString()),
                // Write the role to the claim (optional)
                    }),
                    Expires = DateTime.UtcNow + lifetime,
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(secret),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                // Return the token so the client can save this to send a bearer token in the
                // subsequent requests.
                return Ok(new
                {
                    user.Username,
                    user.AccessToken,
                    user.RefreshToken,
                    UserGuid = user.Guid,
                    Token = tokenHandler.WriteToken(token)
                });
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

