using DemoAPI.Models;
using DemoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{v:apiversion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = new UserEmployeeResponse[]
            {
                    new UserEmployeeResponse
                    {
                        Id = "1",
                        UserName = "user1",
                        Name = "User 1",
                        Email = ""
                    },
                    new UserEmployeeResponse
                    {
                        Id = "2",
                        UserName = "user2",
                        Name = "User 2",
                        Email = ""
                    },
                     new UserEmployeeResponse
                     {
                        Id = "3",
                        UserName = "user3",
                        Name = "User 3",
                        Email = ""
                    }
            };

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {            
            var user = _userService.GetUserById(id);

            if (user == null) 
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserResquest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _userService.AddUser(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(string id, [FromBody] UserResquest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToUpdate = _userService.GetUserById(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            return Ok(_userService.UpdateUser(id, user));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            var userToDelete = _userService.GetUserById(id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            return Ok(_userService.DeleteUser(id));
        }

        [HttpPatch("{id}/{email}")]
        public IActionResult PartialUpdateUser(string id, string email)
        {
            var userToUpdate = _userService.GetUserById(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            return Ok(_userService.UpdateEmail(id, email));
        }
    }
}
