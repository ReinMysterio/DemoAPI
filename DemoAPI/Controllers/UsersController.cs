using DemoAPI.DataAccess;
using DemoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("GetUser/{email}")]
        public User GetUser(string email)
        {
            return new User
            {
                Name = "John Doe",
                Email = email,
                Password = "password",
            };
        }

        [HttpPost("AddUser")]
        public User AddUser([FromBody] User user)
        {
            return new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
        }

        [HttpGet]
        public string TestDBCall()
        {
            var db = new DBContext();
            return db.DbCall();
        }
    }
}
