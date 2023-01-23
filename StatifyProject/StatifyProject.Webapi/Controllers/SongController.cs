using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatifyProject.Application.Infrastructure;
using System;
using System.Linq;

namespace StatifyProject.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly StatifyContext _db;

        public SongController(StatifyContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllSongs()
        {
            var username = HttpContext?.User.Identity?.Name;
            var songs = _db.Songs.Select(s => new
            {
                s.Guid,
                s.Title,
                s.Created_at,
                Length = s.Length.ToString(@"mm\:ss"),
                s.ImageUrl,
                s.Link,
                LikedByCount = s.FavoriteUsers.Count(),
                IsFavorite = s.FavoriteUsers.Any(u => u.Username == username)
            })
            .OrderByDescending(a => a.IsFavorite).ThenBy(a => a.Title)
            .ToList();
            return Ok(songs);
        }
    }
}
