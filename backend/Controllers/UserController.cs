using backend.DTO;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // https:localhost:port/api/v1/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                User? user = await _userService.Register(registerDTO);

                if (user == null)
                {
                    return NotFound();
                }

                var userDTO = new UserDTO
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    FullName = user.FullName,
                    Phone = user.Phone,
                    Sex = user.Sex,
                    DateOfBirth = user.DateOfBirth,
                    CardIdentification = user.CardIdentification
                };

                return Ok(userDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByID([FromRoute] int id)
        {
            try
            {
                User? user = await _userService.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }

                var userDTO = new UserDTO
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    FullName = user.FullName,
                    Phone = user.Phone,
                    Sex = user.Sex,
                    DateOfBirth = user.DateOfBirth,
                    CardIdentification = user.CardIdentification
                };

                return Ok(userDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("resetpassword")]
        //public async Task<IActionResult> 

        //[HttpPatch("{id}")]
        //public async Task<IActionResult> UpdateUserById([FromRoute] int id, [FromBody] UserDTO userDTO)
        //{
        //    try
        //    {
        //        User? user = await _userService.GetUserById(id);

        //        if (user == null)
        //        {
        //            return NotFound();
        //        }

        //        //User? user = await _userService.UpdateUserById(userDTO);

        //        return Ok(user);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
