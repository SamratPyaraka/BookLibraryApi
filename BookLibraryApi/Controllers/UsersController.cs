using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Data;
using BookLibraryApi.Models;
using NuGet.Protocol;
using Newtonsoft.Json;

namespace BookLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly BookLibraryApiContext _context;

        public UsersController(BookLibraryApiContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        [HttpGet()]
        [Route("GetUserFromEmail")]
        public async Task<ActionResult<APIResponse>> GetUserFromEmail(string email)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (userExists == null)
            {
                var notExists = new APIResponse
                {
                    Response = false,
                    Status = 200,
                    ResponseMessage = "Users does not exists.",
                    Data = userExists
                };
                return CreatedAtAction("GetUserFromEmail", notExists);
            }
            else
            {
                var response = new APIResponse
                {
                    Response = true,
                    Status = 200,
                    ResponseMessage = "Users exists.",
                    Data = userExists
                };

                return CreatedAtAction("GetUserFromEmail", response);
            }
        }



        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            var userExists = _context.Users.Any(x => x.Email == users.Email);
            if (!userExists)
            {
                users.CreatedBy = "CoreAPI";
                users.LastUpdatedBy = "CoreAPI";
                users.Created = DateTime.Now;
                users.LastUpdatedOn = DateTime.Now;
                users.Status = 0;
                users.UserName = users.UserName != null ? users.UserName : "";
                _context.Users.Add(users);
                await _context.SaveChangesAsync();

                var response = new APIResponse
                {
                    Response = true,
                    Status = 200,
                    ResponseMessage = "You have registered successfully.",
                    Data = null
                };
                return CreatedAtAction("GetUsers", new { id = users.UserID }, response);
            }
            else
            {
                var response = new APIResponse
                {
                    Response = false,
                    Status = 200,
                    ResponseMessage = "Email already exists",
                    Data = null
                };
                return CreatedAtAction("GetUsers", response);
            }
        }

        // DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUsers(int id)
        //{
        //    var users = await _context.Users.FindAsync(id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(users);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
