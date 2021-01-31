using Licenta.API.Services;
using Licenta.Dtos;
using Licenta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<User> _signInManager;

        public AuthController(IAuthService authService, SignInManager<User> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
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

            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                var appUser = _authService.MapUserForLogin(user);

                return Ok(new
                {
                    token = await _authService.GenerateJWT(user),
                    user = appUser
                });
            }
            return Unauthorized();
        }
    }
}
