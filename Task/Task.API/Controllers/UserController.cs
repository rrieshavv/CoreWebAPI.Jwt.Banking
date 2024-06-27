using Microsoft.AspNetCore.Mvc;
using Task.API.Modals;
using Task.Services.Modals.User;
using Task.Services.Services;

namespace Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserManagement _user;

        public UserController(IUserManagement user)
        {
            _user = user;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistration register)
        {
            var response = await _user.CreateUserAsync(register);

            if (response.IsSuccess)
            {
                await _user.AssignRoleToUserAsync(register.Roles!, response.Res!.User);
                return StatusCode(StatusCodes.Status200OK, new Response { Message = "User register successfully!", IsSuccess = true });

            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = response.Message, IsSuccess = false, Errors = response.Errors });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogin login)
        {
            var response = await _user.LoginUserAsync(login);

            if (response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }

            return StatusCode(StatusCodes.Status401Unauthorized, response); 
        }
    }
}
