using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlaylistApp.Models;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaylistApp.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private SongsContext Db { get; set; }

        public UsersController(SongsContext temp)
        {
            Db = temp;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await Db.Users.ToListAsync();
            return new OkObjectResult(data);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]User user)
        {
            SHA256 sha256 = SHA256.Create();
            string pw = user.Password;
            byte[] inputBytes = Encoding.UTF8.GetBytes(pw);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            // Convert the byte array to a hexadecimal string
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            string hashString = sb.ToString();

            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = hashString,
                Email = user.Email
            };

            Db.Users.Add(newUser);

            await Db.SaveChangesAsync();

            return Ok("Added!");
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

