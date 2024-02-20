using DemoAPI.Models;
using DemoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
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
            var users = _userService.GetAllUsers();

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
        public IActionResult AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userService.AddUser(user);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(string id, [FromBody] User user)
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
    }
}
