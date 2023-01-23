using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatifyProject.Application.Infrastructure;
using System.Linq;

namespace StatifyProject.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly StatifyContext _db;

        public ArtistController(StatifyContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllArtists()
        {
            var username = HttpContext?.User.Identity?.Name;
            var artists = _db.Artists.Select(a => new
            {
                a.Guid,
                a.Name,
                SongCount = a.Songs.Count(),
                Songs = a.Songs.Select(s => new { s.Guid, s.Title, Length = s.Length.ToString(@"mm\:ss"), s.Link }),
                IsFavorite = a.FavoriteUsers.Any(u=>u.Username == username)
            })
            .OrderByDescending(a => a.IsFavorite).ThenBy(a => a.Name)
            .ToList();
            return Ok(artists);
        }
    }
}
