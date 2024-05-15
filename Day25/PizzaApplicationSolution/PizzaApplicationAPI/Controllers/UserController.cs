using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models;
using PizzaApplicationAPI.Models.DTOs;

namespace PizzaApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserLoginDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserLoginDTO), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserLoginDTO>> Login(UserLoginDTO userLoginDTO)
        {
            try
            {
                var result = await _userService.Login(userLoginDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { StatusCode = StatusCodes.Status401Unauthorized, 
                                            Message = "Please Use Correct Credentials"});
            }
        }
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserRegisterDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserRegisterDTO), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserRegisterDTO>> Register(UserRegisterDTO userDTO)
        {
            try
            {
                UserRegisterDTO result = await _userService.Register(userDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong! {ex.Message}"
                });
            }
        }
    }
}
