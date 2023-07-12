using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderlyticsAuth.Model;

namespace OrderlyticsAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        AppDbContext _db;
        public UsersController(AppDbContext db) 
        {
            _db = db;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            try
            {
                var userObj = _db.Users.
                    Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                if (userObj == null)
                {
                    return NotFound("User not found");
                }
                return Ok(userObj);
            }
            catch(Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
            
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            try
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                return Ok(user);
            }
            catch(Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
            
        }
    }
}
