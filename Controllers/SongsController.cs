using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlaylistApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaylistApp.Controllers
{
    [Route("api/[controller]")]
    public class SongsController : Controller
    {

        private SongsContext Db { get; set; }

        public SongsController(SongsContext temp)
        {
            Db = temp;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get(string title = "", string artist = "", string album = "", string genre = "", int page = 1, int pageSize = 10)
        {

            var results = await Db.Songs
                .Where(x => x.Title.ToLower().Contains(title.ToLower())
                && x.Artist.ToLower().Contains(artist.ToLower())
                && x.Album.ToLower().Contains(album.ToLower())
                && x.Genre.ToLower().Contains(genre.ToLower())
                    )
                .ToListAsync();

            var songList = results
                .Skip((page - 1) * pageSize)
                .Take(pageSize);


            var data = new
            {
                results = results.Count(),

                pageLength = pageSize.ToString(),

                nextPage = results.Count() > pageSize ? (page + 1).ToString() : "NaN",

                previousPage = page >= 2 ? (page - 1).ToString() : "NaN",

                songs = songList
            };


            return new OkObjectResult(data);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var song = await Db.Songs.FirstOrDefaultAsync(x => x.SongId == id);

            return new OkObjectResult(song);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

