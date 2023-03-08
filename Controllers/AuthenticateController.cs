using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaylistApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaylistApp.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {

        private SongsContext Db { get; set; }

        public AuthenticateController(SongsContext temp)
        {
            Db = temp;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Login login)
        {
            var user = await Db.Users.FirstOrDefaultAsync(x => x.Username == login.username);

            SHA256 sha256 = SHA256.Create();
            string pw = user.Password;
            byte[] inputBytes = Encoding.UTF8.GetBytes(login.password);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            // Convert the byte array to a hexadecimal string
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            string hashString = sb.ToString();

            if (hashString == user.Password)
            {
                return Ok("Authenticated!");
            }

            else
            {
                return BadRequest("Invalid Password!");
            }
        }
    }
}

