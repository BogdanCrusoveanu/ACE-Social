using Licenta.API.Data;
using Licenta.API.Services;
using Licenta.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userToCreate = _authService.MapRegisterInformations(userForRegisterDto);

            var result = await _authService.RegisterUser(userToCreate, userForRegisterDto.Password);

            var userToReturn = _authService.MapRegisteredUser(userToCreate);

            if (result.Succeeded)
            {
                if (userToReturn.Year != 0)
                {
                    _authService.AddUserToDivisions(userForRegisterDto, userToCreate, userToReturn);
                }
                return CreatedAtRoute("GetUser", new { controller = "Users", id = userToCreate.Id }, userToReturn);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = _authService.FindUser(userForLoginDto.Username);

            var result = await _authService.SignInUser(user, userForLoginDto.Password);

            if (result.Succeeded)
            {
                var appUser = _authService.MapUserForLogin(user);

                return Ok(new
                {
                    token = await _authService.GenerateJwtToken(user),
                    user = appUser
                });
            }
            return Unauthorized();
        }
    }
}
