using DataAccess.Enums;
using DemoAPI.Attributes;
using DemoAPI.Models;
using DemoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [ApiVersion("1.0")]
    //[Route("api/[controller]")]
    [Route("api/v{v:apiversion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [RoleCheck(RoleType.Accountant, RoleType.SuperAdmin)]
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetAllUsers();

            return Ok(users);
        }

        [RoleCheck(RoleType.Accountant)]
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

        [RoleCheck(RoleType.SuperAdmin)]
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

        [RoleCheck(RoleType.Admin)]
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

        [RoleCheck(RoleType.SuperAdmin)]
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

        [RoleCheck(RoleType.Admin)]
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
