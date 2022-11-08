using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Data;
using EmployeeAPI.Models;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<userModel>>> Getusers()
        {
          if (_context.users == null)
          {
              return NotFound();
          }
            return await _context.users.ToListAsync();
        }

        // GET: api/User/5
        //[HttpGet("getuserbyid")]
        //public async Task<ActionResult<userModel>> Getuser(int id)
        //{
        //    var dbuser = await _context.users.FindAsync(id);
        //    if (dbuser == null)
        //        return NotFound();
        //    return Ok();
        //}

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("signup")]
        public async Task<ActionResult<userModel>> signup([FromBody] userModel user)
        {
            if (user == null)
                return BadRequest();

            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "signup Successfully"
            });
        }

        [HttpPost("login")]
        public IActionResult Login(userModel userObj)
        {
            if (userObj == null)
                return BadRequest();

            else
            {
                var user = _context.users.Where(a =>
                a.EmailId == userObj.EmailId
                && a.Password == userObj.Password).FirstOrDefault();
                if (user!=null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Logged in Successfully",
                        UserData = userObj.FullName
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "User Not Found"
                    });
                }
            }
            
                
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("update")]
        //public async Task<IActionResult> Putuser(int id, userModel user)
        //{
        //    var dbuser = await _context.users.FindAsync(id);

        //    if(dbuser == null)
        //        return BadRequest("User not found.");

        //    dbuser.FullName = user.FullName;
        //    dbuser.UserName = user.UserName;
        //    dbuser.Password = user.Password;
        //    dbuser.Mobile= user.Mobile;
        //    dbuser.UserType = user.UserType;

        //    _context.SaveChanges();
        //    return Ok();
        //}


        //DELETE: api/User/5
        [HttpDelete("delete")]
        public async Task<IActionResult> Deleteuser(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool userExists(int id)
        {
            return (_context.users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
