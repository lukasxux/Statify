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
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using StatifyProject.Application.Model.StatifyProject.Application.Model;

namespace StatifyProject.Controllers
{
    [ApiController]
    [Route("api/spotify")]
    public class SpotifyController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly StatifyContext _context;
        private readonly IConfiguration _config;


        public SpotifyController(ILogger<UserController> logger, StatifyContext context, IConfiguration config)
        {
            _logger = logger;
            _context = context;
            _config = config;
        }

        [HttpGet("topTracks/{guid}")]
        public async Task<IActionResult> topTracksOfUser(Guid guid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Guid == guid);
            if (user == null)
            {
                return NotFound(); 
            }
            string access_token = user.AccessToken;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            var result = await httpClient.GetAsync("https://api.spotify.com/v1/me/top/tracks?time_range=short_term&limit=50");

            if (result.IsSuccessStatusCode)
            {
                var responseContent = await result.Content.ReadAsStringAsync();
                var topTracks = JsonConvert.DeserializeObject<TopTracksResponseWrapper>(responseContent);
                
                return Ok(topTracks);
            }
            else
            { 
                return StatusCode((int)result.StatusCode);
            }
        }

        [HttpGet("topArtists/{guid}")]
        public async Task<IActionResult> topArtistsOfUser(Guid guid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Guid == guid);
            if (user == null)
            {
                return NotFound();
            }
            string access_token = user.AccessToken;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            var result = await httpClient.GetAsync("https://api.spotify.com/v1/me/top/artists?time_range=short_term&limit=20");

            if (result.IsSuccessStatusCode)
            {
                var responseContent = await result.Content.ReadAsStringAsync();
                var topArtists = JsonConvert.DeserializeObject<TopArtistsResponseWrapper>(responseContent);

                return Ok(topArtists);
            }
            else
            {
                return StatusCode((int)result.StatusCode);
            }
        }

        [HttpGet("spotifyProfile/{guid}")]
        public async Task<IActionResult> SpotifyProfileOfUser(Guid guid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Guid == guid);
            if (user == null)
            {
                return NotFound();
            }
            string access_token = user.AccessToken;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            var result = await httpClient.GetAsync("https://api.spotify.com/v1/me");

            if (result.IsSuccessStatusCode)
            {
                var responseContent = await result.Content.ReadAsStringAsync();
                var profile = JsonConvert.DeserializeObject<SpotifyProfileResponse>(responseContent);

                return Ok(profile);
            }
            else
            {
                return StatusCode((int)result.StatusCode);
            }
        }

    }
}

