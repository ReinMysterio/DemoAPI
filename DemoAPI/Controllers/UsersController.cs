using DemoAPI.Attributes;
using DemoAPI.Models;
using DemoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [RoleCheck("Accountant")]
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetAllUsers();

            return Ok(users);
        }

        [RoleCheck("Accountant")]
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

        [RoleCheck("SuperAdmin")]
        [HttpPost]
        public IActionResult AddUser([FromBody] UserResquest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userService.AddUser(user);
            return Ok(user);
        }

        [RoleCheck("Admin")]
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

        [RoleCheck("SuperAdmin")]
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

        [RoleCheck("Admin")]
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
