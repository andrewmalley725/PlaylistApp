using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaylistApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaylistApp.Controllers
{
    [Route("api/[controller]")]
    public class PlaylistController : Controller
    {

        private SongsContext Db { get; set; }

        public PlaylistController(SongsContext temp)
        {
            Db = temp;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get(int userid)
        //{
        //    var data = await Db.Playlists.Include(x => x.User).ToListAsync();

        //    return new OkObjectResult(data);
        //}
        //GET: api/values
       [HttpGet]
        public async Task<IActionResult> Get(int userid)
        {
            var playlist = await Db.Playlists.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userid);

            var data = new
            {
                user = playlist.User.Username,
                songs = await Db.PlaylistSongs.Include(x => x.Song).Where(x => x.playListId == playlist.PlaylistId).ToListAsync()
            };

            return new OkObjectResult(data);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post(int userid, int songId)
        {
            var user = await Db.Users.FirstOrDefaultAsync(x => x.UserId == userid);

            var playlist = await Db.Playlists.FirstOrDefaultAsync(x => x.UserId == userid);

            var instance = new PlaylistSongs
            {
                SongId = songId,
                playListId = playlist.PlaylistId
            };

            Db.PlaylistSongs.Add(instance);

            await Db.SaveChangesAsync();

            return Ok("Added to " + user.Username);
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

